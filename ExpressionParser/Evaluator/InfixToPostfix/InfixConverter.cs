using System.Collections.Generic;
using System.Linq;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.InfixToPostfix
{
    public class InfixConverter : IInfixConverter
    {
        /// <summary>
        ///     Converts from infix notation to postfix notation
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public IEnumerable<Token> Convert(IEnumerable<Token> tokens)
        {
            var infixTokens = new Stack<Token>(tokens);
            var postfixTokens = new List<Token>();
            var operators = new Stack<OperatorToken>();

            while (infixTokens.Any())
            {
                var token = infixTokens.Pop();
                if (token is NumericToken)
                {
                    postfixTokens.Add(token);
                }
                else if (token is OperatorToken)
                {
                    var operatorToken = (OperatorToken)token;
                    while (operators.Any() && operatorToken.Value.Precedence <= operators.Peek().Value.Precedence)
                    {
                        postfixTokens.Add(operators.Pop());
                    }
                    operators.Push(operatorToken);
                }
                // parenthesis
            }
            while (operators.Any())
            {
                postfixTokens.Add(operators.Pop());
            }
            return postfixTokens;
        }
    }
}
