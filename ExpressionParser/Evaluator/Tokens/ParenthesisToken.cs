namespace ExpressionParser.Evaluator.Tokens
{
    internal class ParenthesisToken : Token
    {
        public ParenthesisToken(string value) : base(value)
        {
            Value = value == "(" ? Direction.Opening : Direction.Closing;
        }

        public Direction Value { get; }

        public enum Direction
        {
            Opening,
            Closing
        }

        /// <summary>
        ///     Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"ParenthesisToken[{Value}]";

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
            /// <returns>A new token for the specified value</returns>
            protected override ParenthesisToken CreateToken(string value) => new ParenthesisToken(value);
        }
    }
}