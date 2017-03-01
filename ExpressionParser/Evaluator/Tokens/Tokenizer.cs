using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionParser.Evaluator.Operators;

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
            var tokens = new List<Token>();
            var index = 0;

            // Read tokens until the end of the expression
            while (index < expression.Length)
            {
                var token = ReadToken(expression, index);
                if (token == null)
                {
                    throw new EvaluationException($"Syntax error: '{expression.Substring(index)}' is unknown @ char: {index}");
                }

                tokens.Add(token);
                index += token.Length;
            }

            return ConvertSubtractionToUnaryMinus(tokens);
        }

        /// <summary>
        ///     Reads a single token from the expression
        /// </summary>
        private Token ReadToken(string expression, int index)
        {
            Token token = null;
            var length = 1;

            // Find the largest consumable token in the input that matches a single token
            while (index + length <= expression.Length)
            {
                var value = expression.Substring(index, length);
                var matchingProviders = _providers.Where(provider => provider.Matches(value)).ToList();
                if (matchingProviders.Count == 1)
                {
                    token = matchingProviders.Single().CreateToken(value, index);
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

        /// <summary>
        ///     Convert subtraction operators to negation
        /// </summary>
        private static IEnumerable<Token> ConvertSubtractionToUnaryMinus(IEnumerable<Token> tokens)
        {
            var result = new Stack<Token>();

            var lastTokenIsNumeric = false;
            foreach (var token in tokens)
            {
                result.Push(token);

                // If the last token read is numeric, and this is a subtraction, convert to negation
                if ((token as OperatorToken)?.Value is ArithmeticOperator.SubtractOperator
                    && !lastTokenIsNumeric)
                {
                    result.Pop();
                    result.Push(new OperatorToken(new NegationOperator(), token.Index.Value));
                }
                else if (token is NumericToken)
                {
                    lastTokenIsNumeric = true;
                }
                else if (token is OperatorToken)
                {
                    lastTokenIsNumeric = false;
                }
            }

            return result;
        }
    }
}