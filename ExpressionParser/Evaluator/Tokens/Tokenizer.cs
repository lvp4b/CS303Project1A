using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionParser.UserIO.Input;

namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Tokenizes the input expression
    /// </summary>
    internal class Tokenizer : ITokenizer
    {
        private readonly IInput _input;
        private readonly IList<Token.IProvider> _providers;

        /// <summary>
        ///     Instantiates a new tokenizer using the specified input
        /// </summary>
        /// <param name="input">The input expression provider</param>
        /// <param name="providers">The token providers</param>
        public Tokenizer(IInput input, IList<Token.IProvider> providers)
        {
            _input = input;
            _providers = providers;
        }

        /// <summary>
        ///     Gets the tokens of the input expression
        /// </summary>
        /// <returns>A new enumerable containing the tokens of the input expression</returns>
        public IEnumerable<Token> GetTokens()
        {
            var tokens = new Stack<Token>();
            var input = _input.Get();
            var index = 0;

            while (index < input.Length)
            {
                var length = 1;
                Token token = null;
                while (index + length <= input.Length)
                {
                    var value = input.Substring(index, length);
                    var matchingProviders = _providers.Where(provider => provider.Matches(value)).ToList();
                    if (matchingProviders.Count == 1)
                    {
                        token = matchingProviders.Single().CreateToken(value);
                    }
                    else if (matchingProviders.Count == 0 && token != null)
                    {
                        break;
                    }
                    else if(matchingProviders.Count > 1)
                    {
                        throw new InvalidOperationException($"Input subexpression '{value}' is ambiguous");
                    }

                    length++;
                }

                if (token == null)
                {
                    throw new InvalidOperationException($"Syntax error: '{input.Substring(index)}' is unknown");
                }
                
                tokens.Push(token);
                index += token.Value.Length;
            }

            return tokens;
        }
    }
}