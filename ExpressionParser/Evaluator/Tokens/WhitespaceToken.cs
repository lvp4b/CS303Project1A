using System.Text.RegularExpressions;

namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Represents a whitespace token
    /// </summary>
    internal class WhitespaceToken : Token
    {
        /// <summary>
        ///     Instantiates a whitespace token using the specified value
        /// </summary>
        /// <param name="value">The value of the whitespace token</param>
        public WhitespaceToken(string value) : base(value)
        {
        }

        /// <summary>
        ///     Validates the token
        /// </summary>
        /// <returns>The reason the token is invalid, null if token is valid</returns>
        internal override string Validate()
        {
            return Regex.IsMatch(Value, Provider.Pattern) ? null : $"{this} is not valid whitespace";
        }

        /// <summary>
        ///     Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"WhitespaceToken[{Value}]";

        /// <summary>
        ///     Provides whitespace tokens
        /// </summary>
        internal class Provider : Provider<WhitespaceToken>
        {
            internal const string Pattern = @"^\s+$";

            /// <summary>
            ///     Gets the regular expression of the input consumed by this provider 
            /// </summary>
            protected override string TokenPattern => Pattern;

            /// <summary>
            ///     Creates a token for the specified value
            /// </summary>
            /// <param name="value">The value to create a token for</param>
            /// <returns>A new token for the specified value</returns>
            protected override WhitespaceToken CreateToken(string value) => new WhitespaceToken(value);
        }
    }
}