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
        /// <param name="index">The 0-based index in the expression of the first character of the token</param>
        /// <param name="value">The value of the numeric token</param>
        public NumericToken(string value, int index) : base(value.Length, index)
        {
            int val;
            if (int.TryParse(value, out val))
            {
                Value = val;
            }
            else
            {
                throw new EvaluationException($"Input number '{value}' is out of range @ char: {index}");
            }
        }

        /// <summary>
        ///     Instantiates a numeric token using the specified value
        /// </summary>
        /// <param name="value">The value of the numeric token</param>
        private NumericToken(int value) : base(value.ToString().Length, null)
        {
            Value = value;
        }

        /// <summary>
        ///     Implicitly converts the specified boolean value into a numeric token
        /// </summary>
        public static implicit operator NumericToken(bool value) => new NumericToken(value ? 1 : 0);

        /// <summary>
        ///     Implicitly converts the specified numeric token into a boolean value
        /// </summary>
        public static implicit operator bool(NumericToken value) => value.Value != 0;

        /// <summary>
        ///     Implicitly converts the specified integer into a numeric token
        /// </summary>
        public static implicit operator NumericToken(int value) => new NumericToken(value);

        /// <summary>
        ///     Implicitly converts the specified numeric token into an integer
        /// </summary>
        public static implicit operator int(NumericToken token) => token.Value;

        /// <summary>
        ///     Compares two numeric tokens for equality
        /// </summary>
        public static bool operator ==(NumericToken l, NumericToken r) => Equals(l, r);

        /// <summary>
        ///     Compares two numeric tokens for inequality
        /// </summary>
        public static bool operator !=(NumericToken l, NumericToken r) => !Equals(l, r);

        /// <summary>
        ///     Gets the value of the numeric token
        /// </summary>
        public int Value { get; }

        /// <summary>
        ///     Determines whether the specified object is equal to the current object
        /// </summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false</returns>
        /// <param name="other">The object to compare with the current object</param>
        protected bool Equals(NumericToken other) => Value == other.Value;

        /// <summary>
        ///     Determines whether the specified object is equal to the current object
        /// </summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false</returns>
        /// <param name="obj">The object to compare with the current object</param>
        public override bool Equals(object obj) => Equals(obj as NumericToken);

        /// <summary>
        ///     Serves as the default hash function</summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode() => Value;

        /// <summary>
        ///     Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"Numeric[{Value}]";

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
            /// <param name="index">The 0-based index in the expression of the first character of the token</param>
            /// <returns>A new token for the specified value</returns>
            protected override NumericToken CreateToken(string value, int index) => new NumericToken(value, index);
        }
    }
}