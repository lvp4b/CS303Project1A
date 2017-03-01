using System.Collections.Generic;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.InfixToPostfix
{
    /// <summary>
    ///     Converts infix to postfix notation
    /// </summary>
    public interface IInfixConverter
    {
        /// <summary>
        ///     Converts from infix notation to postfix notation
        /// </summary>
        IEnumerable<Token> Convert(IEnumerable<Token> infixTokens);
    }
}
