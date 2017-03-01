using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.Operators
{
    /// <summary>
    ///     Represents an operator
    /// </summary>
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
        public abstract int Operands { get; }
        
        /// <summary>
        ///     Evaluates this operator using the specified operands
        /// </summary>
        /// <param name="operands">The operands for this operator</param>
        /// <returns>The result of the operation</returns>
        internal abstract NumericToken Evaluate(NumericToken[] operands);

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => Symbol;
    }
}
