namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Represents a numeric token
    /// </summary>
    internal class NumericToken : Token
    {
        /// <summary>
        ///     Instantiates a numeric token using the specified value
        /// </summary>
        /// <param name="value">The value of the numeric token</param>
        public NumericToken(string value) : base(value)
        {
            Value = int.Parse(value);
        }

        public static implicit operator NumericToken(bool value)
        {
            return new NumericToken(value ? "1" : "0");
        }

        public static implicit operator bool(NumericToken value)
        {
            return value.Value != 0;
        }

        public static implicit operator NumericToken(int value)
        {
            return new NumericToken(value.ToString());
        }
        
        public static implicit operator int(NumericToken token)
        {
            return token.Value;
        }

        /// <summary>
        ///     Gets the value of the numeric token
        /// </summary>
        public new int Value { get; }

        /// <summary>
        ///     Validates the token
        /// </summary>
        /// <returns>The reason the token is invalid, null if token is valid</returns>
        internal override string Validate()
        {
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