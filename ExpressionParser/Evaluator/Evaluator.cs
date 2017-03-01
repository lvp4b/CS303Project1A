using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionParser.Evaluator.InfixToPostfix;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator
{
    /// <summary>
    ///     Evaluates an infix expression
    /// </summary>
    internal class Evaluator : IEvaluator
    {
        private readonly IInfixConverter _converter;
        private readonly ITokenizer _tokenizer;

        /// <summary>
        ///     Instantiates a new expression evaluator with the specified dependencies
        /// </summary>
        public Evaluator(IInfixConverter converter, ITokenizer tokenizer)
        {
            _converter = converter;
            _tokenizer = tokenizer;
        }

        /// <summary>
        ///     Evaluates the specified infix expression
        /// </summary>
        /// <param name="expression">The infix expression to evaluate</param>
        /// <returns>The result of the expression</returns>
        /// <exception cref="EvaluationException">If the expression is invalid</exception>
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
                    operands.Push(Evaluate((OperatorToken)token, operands));
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
        private static NumericToken Evaluate(OperatorToken token, Stack<Token> operands)
        {
            try
            {
                var arguments = new NumericToken[token.Value.Operands];
                for (var i = 0; i < token.Value.Operands; i++)
                {
                    arguments[i] = (NumericToken)operands.Pop();
                }
                return token.Value.Evaluate(arguments);
            }
            catch (InvalidOperationException e) when (!operands.Any())
            {
                throw new EvaluationException(
                    $"Insufficient operands for operator '{token.Value}' @ char: {token.Index}", e);
            }
        }
    }
}
