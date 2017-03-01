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
        /// <param name="index">The 0-based index in the expression of the first character of the token</param>
        public WhitespaceToken(string value, int index) : base(value.Length, index)
        {
        }

        /// <summary>
        ///     Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => "Whitespace";

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
            /// <param name="index">The 0-based index in the expression of the first character of the token</param>
            /// <returns>A new token for the specified value</returns>
            protected override WhitespaceToken CreateToken(string value, int index) => new WhitespaceToken(value, index);
        }
    }
}