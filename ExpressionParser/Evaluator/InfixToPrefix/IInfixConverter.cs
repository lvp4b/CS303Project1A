using System.Collections.Generic;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.InfixToPrefix
{
    /// <summary>
    ///     Converts infix to prefix notation
    /// </summary>
    public interface IInfixConverter
    {
        /// <summary>
        ///     Converts from infix notation to prefix notation
        /// </summary>
        /// <param name="infixTokens"></param>
        /// <returns></returns>
        IEnumerable<Token> Convert(IEnumerable<Token> infixTokens);
    }
}
