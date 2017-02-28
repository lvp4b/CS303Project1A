using System;
using System.Collections.Generic;
using ExpressionParser.Evaluator.InfixToPostfix;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator
{
    internal class Evaluator : IEvaluator
    {
        private readonly IInfixConverter _converter;

        public Evaluator(IInfixConverter converter)
        {
            _converter = converter;
        }

        public int Evaluate(IEnumerable<Token> infixTokens)
        {
            var tokens = new Stack<Token>();

            foreach (var token in _converter.Convert(infixTokens))
            {
                if (token is NumericToken)
                {
                    tokens.Push(token);
                }
                else if (token is OperatorToken)
                {
                    tokens.Push(((OperatorToken) token).Value.Evaluate(tokens));
                }
            }
            return (NumericToken) tokens.Pop();
        }
    }
}
