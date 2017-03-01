namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Represents a parenthesis token
    /// </summary>
    internal class ParenthesisToken : Token
    {
        /// <summary>
        ///     Instantiates a new parenthesis token using the specified value
        /// </summary>
        /// <param name="value">The parenthesis symbol</param>
        /// <param name="index">The 0-based index in the expression of the first character of the token</param>
        public ParenthesisToken(string value, int index) : base(value.Length, index)
        {
            Value = value == "(" ? Direction.Opening : Direction.Closing;
        }

        /// <summary>
        ///     Gets the direction of the parenthesis
        /// </summary>
        public Direction Value { get; }

        /// <summary>
        ///     Indicates the direction of a parenthesis token
        /// </summary>
        public enum Direction
        {
            /// <summary>
            ///     An opening, left parenthesis
            /// </summary>
            Opening,

            /// <summary>
            ///     A closing, right parenthesis
            /// </summary>
            Closing
        }

        /// <summary>
        ///     Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"Parenthesis[{Value}]";

        /// <summary>
        ///     Provides operator tokens
        /// </summary>
        internal class Provider : Provider<ParenthesisToken>
        {
            /// <summary>
            ///     Gets the regular expression of the input consumed by this provider 
            /// </summary>
            protected override string TokenPattern => "^[()]$";

            /// <summary>
            ///     Creates a token for the specified value
            /// </summary>
            /// <param name="value">The value to create a token for</param>
            /// <param name="index">The 0-based index in the expression of the first character of the token</param>
            /// <returns>A new token for the specified value</returns>
            protected override ParenthesisToken CreateToken(string value, int index) => new ParenthesisToken(value, index);
        }
    }
}