using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.Operators
{
    /// <summary>
    ///     Represents a prefix increment or decrement operation
    /// </summary>
    internal abstract class PrefixOperator : Operator
    {
        public override int Operands => 1;

        protected abstract NumericToken Evaluate(NumericToken r);

        protected override NumericToken Evaluate(NumericToken[] operands)
        {
            return Evaluate(operands[0]);
        }

        /// <summary>
        ///     Represents the increment operation
        /// </summary>
        internal class IncrementOperator : PrefixOperator
        {
            public override string Symbol => "++";

            public override int Precedence => 8;

            protected override NumericToken Evaluate(NumericToken r) => ++r;
        }

        /// <summary>
        ///     Represents the decrement operation
        /// </summary>
        internal class DecrementOperator : PrefixOperator
        {
            public override string Symbol => "--";

            public override int Precedence => 8;

            protected override NumericToken Evaluate(NumericToken r) => --r;
        }

    }
}
