using System.Text.RegularExpressions;

namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Represent a operator token
    /// </summary>
    internal class OperatorToken : Token
    {
        /// <summary>
        ///     Instantiates a operator token using the specified value
        /// </summary>
        /// <param name="value">The value of the operator token</param>
        public OperatorToken(string value) : base(value)
        {
        }

        /// <summary>
        ///     Validates the token
        /// </summary>
        /// <returns>The reason the token is invalid, null if token is valid</returns>
        internal override string Validate()
        {
            return Regex.IsMatch(Value, Provider.Pattern) ? null : $"{this} is not a valid operator";
        }

        /// <summary>
        ///     Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"OperatorToken[{Value}]";

        /// <summary>
        ///     Provides operator tokens
        /// </summary>
        internal class Provider : Provider<OperatorToken>
        {
            internal const string Pattern = @"^([-+*/()]|--|\+\+)$";

            /// <summary>
            ///     Gets the regular expression of the input consumed by this provider 
            /// </summary>
            protected override string TokenPattern => Pattern;

            /// <summary>
            ///     Creates a token for the specified value
            /// </summary>
            /// <param name="value">The value to create a token for</param>
            /// <returns>A new token for the specified value</returns>
            protected override OperatorToken CreateToken(string value) => new OperatorToken(value);
        }
    }
}