using System.Collections.Generic;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.Operators
{
    internal abstract class Operator
    {
        /// <summary>
        ///     Gets the symbol of the operator
        /// </summary>
        public abstract string Symbol { get; }

        /// <summary>
        ///     Gets the precedence of the operator
        /// </summary>
        public abstract int Precedence { get; }

        /// <summary>
        ///     Gets the number of operands used by the operator
        /// </summary>
        protected abstract int Operands { get; }

        /// <summary>
        ///     Evaluates this operator using the specified operands
        /// </summary>
        /// <param name="operands">The operands stack</param>
        /// <returns>The result of the operation</returns>
        internal NumericToken Evaluate(Stack<Token> operands)
        {
            var argument = new NumericToken[Operands];
            for (var i = 0; i < Operands; i++)
            {
                argument[i] = (NumericToken) operands.Pop();
            }
            return Evaluate(argument);
        }

        /// <summary>
        ///     Evaluates this operator using the specified operands
        /// </summary>
        /// <param name="operands">The operands for this operator</param>
        /// <returns>The result of the operation</returns>
        protected abstract NumericToken Evaluate(NumericToken[] operands);

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => Symbol;
    }
}
