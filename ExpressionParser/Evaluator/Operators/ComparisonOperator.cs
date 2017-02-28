﻿using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.Operators
{
    /// <summary>
    ///     Represents a basic comparison operation
    /// </summary>
    internal abstract class ComparisonOperator : Operator
    {
        protected override int Operands => 2;

        protected abstract NumericToken Evaluate(NumericToken l, NumericToken r);

        protected override NumericToken Evaluate(NumericToken[] operands)
        {
            return Evaluate(operands[1], operands[0]);
        }

        /// <summary>
        ///     Represents the greater than operation
        /// </summary>
        internal class GreaterThanOperator : ComparisonOperator
        {
            public override string Symbol => ">";

            public override int Precedence => 4;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r) => l > r;
        }

        /// <summary>
        ///     Represents the greater than or equal operation
        /// </summary>
        internal class GreaterOrEqualOperator : ComparisonOperator
        {
            public override string Symbol => ">=";

            public override int Precedence => 4;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r) => l >= r;
        }

        /// <summary>
        ///     Represents the less than operation
        /// </summary>
        internal class LessThanOperator : ComparisonOperator
        {
            public override string Symbol => "<";

            public override int Precedence => 4;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r) => l < r;
        }

        /// <summary>
        ///     Represents the less than or equal operation
        /// </summary>
        internal class LessOrEqualOperator : ComparisonOperator
        {
            public override string Symbol => "<=";

            public override int Precedence => 4;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r) => l <= r;
        }
    }
}