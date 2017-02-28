using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Tokenizes the input expression
    /// </summary>
    internal class Tokenizer : ITokenizer
    {
        private readonly IList<Token.IProvider> _providers;

        /// <summary>
        ///     Instantiates a new tokenizer using the specified providers
        /// </summary>
        /// <param name="providers">The token providers</param>
        public Tokenizer(IList<Token.IProvider> providers)
        {
            _providers = providers;
        }

        /// <summary>
        ///     Gets the tokens of the input expression
        /// </summary>
        /// <param name="expression">The input expression</param>
        /// <returns>A new enumerable containing the tokens of the input expression</returns>
        public IEnumerable<Token> GetTokens(string expression)
        {
            var tokens = new Stack<Token>();
            var index = 0;

            while (index < expression.Length)
            {
                var token = ReadToken(expression, index);
                if (token == null)
                {
                    throw new InvalidOperationException($"Syntax error: '{expression.Substring(index)}' is unknown");
                }

                tokens.Push(token);
                index += token.Length;
            }

            return tokens;
        }

        private Token ReadToken(string expression, int index)
        {
            Token token = null;
            var length = 1;
            while (index + length <= expression.Length)
            {
                var value = expression.Substring(index, length);
                var matchingProviders = _providers.Where(provider => provider.Matches(value)).ToList();
                if (matchingProviders.Count == 1)
                {
                    token = matchingProviders.Single().CreateToken(value);
                }
                else if (matchingProviders.Count == 0 && token != null)
                {
                    break;
                }
                else if (matchingProviders.Count > 1)
                {
                    throw new InvalidOperationException($"Input subexpression '{value}' is ambiguous");
                }

                length++;
            }
            return token;
        }
    }
}