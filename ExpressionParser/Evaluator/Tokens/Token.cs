using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

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
        /// <param name="length">The length of the token, in characters</param>
        /// <param name="index">The 0-based index in the expression of the first character of the token</param>
        internal Token(int length, int? index)
        {
            Index = index;
            Length = length;
        }

        /// <summary>
        ///     Gets the 0-based character index of the start of the token in the expression
        /// </summary>
        [CanBeNull]
        public int? Index { get; }

        /// <summary>
        ///     Gets the length, in characters of the token
        /// </summary>
        public int Length { get; }

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
            /// <param name="index">The 0-based index in the expression of the first character of the token</param>
            /// <returns>A new token for the specified value</returns>
            Token CreateToken(string value, int index);
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
            /// <param name="index">The 0-based index in the expression of the first character of the token</param>
            /// <returns>A new token for the specified value</returns>
            protected abstract TToken CreateToken(string value, int index);

            /// <summary>
            ///     Creates a token for the specified value
            /// </summary>
            /// <param name="value">The value to create a token for</param>
            /// <param name="index">The 0-based index in the expression of the first character of the token</param>
            /// <returns>A new token for the specified value</returns>
            Token IProvider.CreateToken(string value, int index) => CreateToken(value, index);
        }
    }
}