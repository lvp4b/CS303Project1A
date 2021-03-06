﻿using System.Collections.Generic;
using System.Linq;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.InfixToPostfix
{
    /// <summary>
    ///     Converts infix to postfix notation
    /// </summary>
    internal class InfixConverter : IInfixConverter
    {
        /// <summary>
        ///     Converts from infix notation to postfix notation
        /// </summary>
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
                    // If the input token is right associative, and has a 
                    // precedence less than that of the top operator,
                    // push the top operator to the output
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
                            // Push the operators in the group to the output
                            while (operators.Any() &&
                                   (operators.Peek() as ParenthesisToken)?.Value != ParenthesisToken.Direction.Opening)
                            {
                                postfixTokens.Add(operators.Pop());
                            }
                            
                            // Check for unmatched closing parenthesis
                            if (!operators.Any())
                            {
                                throw new EvaluationException($"Unmatched closing parenthesis @ char: {token.Index}");
                            }

                            operators.Pop();
                            break;
                    }
                }
            }

            // Copy remaining operators to the output
            while (operators.Any())
            {
                var token = operators.Pop();
                if (token is ParenthesisToken)
                {
                    throw new EvaluationException($"Unmatched opening parenthesis @ char: {token.Index}");
                }
                postfixTokens.Add(token);
            }
            
            return postfixTokens;
        }
    }
}
