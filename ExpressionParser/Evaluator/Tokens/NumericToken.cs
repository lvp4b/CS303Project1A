namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Represent a numeric token
    /// </summary>
    internal class NumericToken : Token
    {
        /// <summary>
        ///     Instantiates a numeric token using the specified value
        /// </summary>
        /// <param name="value">The value of the numeric token</param>
        public NumericToken(string value) : base(value)
        {
        }

        /// <summary>
        ///     Validates the token
        /// </summary>
        /// <returns>The reason the token is invalid, null if token is valid</returns>
        internal override string Validate()
        {
            int value;
            if (!int.TryParse(Value, out value))
            {
                return $"{this} is out of range";
            }
            return null;
        }

        /// <summary>
        ///     Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"NumericToken[{Value}]";

        /// <summary>
        ///     Provides numeric tokens
        /// </summary>
        internal class Provider : Provider<NumericToken>
        {
            /// <summary>
            ///     Gets the regular expression of the input consumed by this provider 
            /// </summary>
            protected override string TokenPattern => "^[0-9]+$";

            /// <summary>
            ///     Creates a token for the specified value
            /// </summary>
            /// <param name="value">The value to create a token for</param>
            /// <returns>A new token for the specified value</returns>
            protected override NumericToken CreateToken(string value) => new NumericToken(value);
        }
    }
}