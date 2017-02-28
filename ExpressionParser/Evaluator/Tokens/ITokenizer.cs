using System.Collections.Generic;

namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Tokenizes the input expression
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        ///     Gets the tokens of the input expression
        /// </summary>
        /// <param name="expression">The input expression</param>
        /// <returns>A new enumerable containing the tokens of the input expression</returns>
        IEnumerable<Token> GetTokens(string expression);
    }
}
