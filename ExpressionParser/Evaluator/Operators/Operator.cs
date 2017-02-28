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
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => Symbol;
    }
}
