using System.Collections.Generic;
using System.Linq;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.InfixToPrefix
{
    public class InfixConverter : IInfixConverter
    {
        public IEnumerable<Token> Convert(IEnumerable<Token> tokens)
        {
            var infixTokens = new Stack<Token>(tokens);
            var prefixTokens = new Stack<Token>();
            var operators = new Stack<OperatorToken>();

            while (infixTokens.Any())
            {
                var token = infixTokens.Pop();
                if (token is NumericToken)
                {
                    prefixTokens.Push(token);
                }
                else if (token is OperatorToken)
                {
                    var operatorToken = (OperatorToken)token;
                    while (operators.Any() && operatorToken.Value.Precedence <= operators.Peek().Value.Precedence)
                    {
                        prefixTokens.Push(operators.Pop());
                    }
                    operators.Push(operatorToken);
                }
                // parenthesis
            }
            while (operators.Any())
            {
                prefixTokens.Push(operators.Pop());
            }
            return prefixTokens;
        }
    }
}
