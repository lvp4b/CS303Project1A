using System;
using System.Text.RegularExpressions;

namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Represents a token
    /// </summary>
    public abstract class Token
    {
        /// <summary>
        ///     Instantiates a token using the specified value
        /// </summary>
        /// <param name="value">The value of the token</param>
        internal Token(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Gets the value of the token
        /// </summary>
        public string Value { get; }

        /// <summary>
        ///     Provides the token for a value
        /// </summary>
        internal interface IProvider
        {
            /// <summary>
            ///     Tests if the specified input subexpression is supported by this provider
            /// </summary>
            /// <param name="value">The input subexpression</param>
            /// <returns>True if this provider supports tokenizing the specified input</returns>
            bool Matches(string value);

            /// <summary>
            ///     Creates a token for the specified value
            /// </summary>
            /// <param name="value">The value to create a token for</param>
            /// <returns>A new token for the specified value</returns>
            Token CreateToken(string value);
        }

        /// <summary>
        ///     Provides the token for a value
        /// </summary>
        internal abstract class Provider<TToken> : IProvider where TToken : Token
        {
            private readonly Lazy<Regex> _matcher;

            /// <summary>
            ///     Instantiates a new token provider
            /// </summary>
            protected Provider()
            {
                _matcher = new Lazy<Regex>(() => new Regex(TokenPattern));
            }

            /// <summary>
            ///     Gets the regular expression of the input consumed by this provider 
            /// </summary>
            protected abstract string TokenPattern { get; }

            /// <summary>
            ///     Tests if the specified input subexpression is supported by this provider
            /// </summary>
            /// <param name="value">The input subexpression</param>
            /// <returns>True if this provider supports tokenizing the specified input</returns>
            public bool Matches(string value) => _matcher.Value.IsMatch(value);

            /// <summary>
            ///     Creates a token for the specified value
            /// </summary>
            /// <param name="value">The value to create a token for</param>
            /// <returns>A new token for the specified value</returns>
            protected abstract TToken CreateToken(string value);

            /// <summary>
            ///     Creates a token for the specified value
            /// </summary>
            /// <param name="value">The value to create a token for</param>
            /// <returns>A new token for the specified value</returns>
            Token IProvider.CreateToken(string value) => CreateToken(value);
        }
    }
}