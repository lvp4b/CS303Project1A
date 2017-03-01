using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionParser.Evaluator.InfixToPostfix;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator
{
    internal class Evaluator : IEvaluator
    {
        private readonly IInfixConverter _converter;
        private readonly ITokenizer _tokenizer;

        public Evaluator(IInfixConverter converter, ITokenizer tokenizer)
        {
            _converter = converter;
            _tokenizer = tokenizer;
        }

        public int Evaluate(string expression)
        {
            var tokens = _tokenizer.GetTokens(expression);
            var postfixTokens = _converter.Convert(tokens);
            var operands = new Stack<Token>();
            foreach (var token in postfixTokens)
            {
                if (token is NumericToken)
                {
                    operands.Push(token);
                }
                else if (token is OperatorToken)
                {
                    Evaluate((OperatorToken)token, operands);
                }
            }

            if (!operands.Any())
            {
                throw new EvaluationException("No expression to evaluate");
            }

            if (operands.Count > 1)
            {
                throw new EvaluationException($"Multiple operands in a row near {operands.First().Index}");
            }

            return (NumericToken) operands.Pop();
        }
        
        /// <summary>
        ///     Performs the evaluation of the specified operator token
        /// </summary>
        private static void Evaluate(OperatorToken token, Stack<Token> operands)
        {
            try
            {
                var arguments = new NumericToken[token.Value.Operands];
                for (var i = 0; i < token.Value.Operands; i++)
                {
                    arguments[i] = (NumericToken)operands.Pop();
                }
                operands.Push(token.Value.Evaluate(arguments));
            }
            catch (InvalidOperationException e) when (!operands.Any())
            {
                throw new EvaluationException(
                    $"Insufficient operands for operator '{token.Value}' @ char: {token.Index}", e);
            }
        }
    }
}
