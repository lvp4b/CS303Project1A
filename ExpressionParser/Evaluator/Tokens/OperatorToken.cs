using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExpressionParser.Evaluator.Operators;

namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Represents a operator token
    /// </summary>
    internal class OperatorToken : Token
    {
        /// <summary>
        ///     Instantiates a operator token using the specified value
        /// </summary>
        /// <param name="value">The value of the operator token</param>
        public OperatorToken(Operator value) : base(value.Symbol)
        {
            Value = value;
        }
        
        /// <summary>
        ///     Gets the value of the operator token
        /// </summary>
        public Operator Value { get; }

        /// <summary>
        ///     Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"Operator[{Value}]";

        /// <summary>
        ///     Provides operator tokens
        /// </summary>
        internal class Provider : Provider<OperatorToken>
        {
            private readonly IDictionary<string, Operator> _operators;

            /// <summary>
            ///     Instantiates the operator token provider that supports the specified operators
            /// </summary>
            /// <param name="operators">The operators to support with this provider</param>
            public Provider(IEnumerable<Operator> operators)
            {
                _operators = new Dictionary<string, Operator>();
                foreach (var @operator in operators)
                {
                    if (!_operators.ContainsKey(@operator.Symbol))
                    {
                       _operators.Add(@operator.Symbol, @operator); 
                    }
                    else if (_operators[@operator.Symbol].Precedence > @operator.Precedence)
                    {
                        _operators[@operator.Symbol] = @operator;
                    }
                }
            }

            /// <summary>
            ///     Gets the regular expression of the input consumed by this provider 
            /// </summary>
            protected override string TokenPattern => $"^({string.Join("|", _operators.Keys.Select(Regex.Escape))})$";

            /// <summary>
            ///     Creates a token for the specified value
            /// </summary>
            /// <param name="value">The value to create a token for</param>
            /// <returns>A new token for the specified value</returns>
            protected override OperatorToken CreateToken(string value) => new OperatorToken(_operators[value]);
        }
    }
}