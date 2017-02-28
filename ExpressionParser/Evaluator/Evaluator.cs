using System.Collections.Generic;
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
            var tokens = new Stack<Token>();

            foreach (var token in _converter.Convert(_tokenizer.GetTokens(expression)))
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
