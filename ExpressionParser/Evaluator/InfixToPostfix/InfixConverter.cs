using System;
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
            var operators = new Stack<Token>();

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
                    while (operators.Any() && operatorToken.Value.Precedence
                        < (operators.Peek() as OperatorToken)?.Value.Precedence)
                    {
                        postfixTokens.Add(operators.Pop());
                    }
                    operators.Push(operatorToken);
                }
                else if (token is ParenthesisToken)
                {
                    var parenthesis = (ParenthesisToken) token;
                    switch (parenthesis.Value)
                    {
                        case ParenthesisToken.Direction.Opening:
                            operators.Push(parenthesis);
                            break;
                        case ParenthesisToken.Direction.Closing:
                            while (operators.Any() &&
                                   (operators.Peek() as ParenthesisToken)?.Value != ParenthesisToken.Direction.Opening)
                            {
                                postfixTokens.Add(operators.Pop());
                            }
                            operators.Pop();
                            break;
                    }
                }
            }

            while (operators.Any())
            {
                var token = operators.Pop();
                if (token is ParenthesisToken)
                {
                    throw new Exception("Mismatched parenthesis");
                }
                postfixTokens.Add(token);
            }
            
            return postfixTokens;
        }
    }
}
