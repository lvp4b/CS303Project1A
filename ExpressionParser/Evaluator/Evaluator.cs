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
            var tokens = _tokenizer.GetTokens(expression);
            var postfixTokens = _converter.Convert(tokens);
            var stack = new Stack<Token>();
            foreach (var token in postfixTokens)
            {
                if (token is NumericToken)
                {
                    stack.Push(token);
                }
                else if (token is OperatorToken)
                {
                    stack.Push(((OperatorToken) token).Value.Evaluate(stack));
                }
            }
            return (NumericToken) stack.Pop();
        }
    }
}
