﻿using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.Operators
{
    /// <summary>
    ///     Represents a basic logical operation
    /// </summary>
    internal abstract class LogicalOperator : Operator
    {
        public override int Operands => 2;

        protected abstract NumericToken Evaluate(NumericToken l, NumericToken r);

        internal override NumericToken Evaluate(NumericToken[] operands)
        {
            return Evaluate(operands[1], operands[0]);
        }

        /// <summary>
        ///     Represents the not operation
        /// </summary>
        internal class NotOperator : Operator
        {
            public override string Symbol => "!";

            public override int Precedence => 8;

            public override int Operands => 1;

            internal override NumericToken Evaluate(NumericToken[] operands) => !operands[0];
        }

        /// <summary>
        ///     Represents the and operation
        /// </summary>
        internal class AndOperator : LogicalOperator
        {
            public override string Symbol => "&&";

            public override int Precedence => 2;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r) => l && r;
        }

        /// <summary>
        ///     Represents the or operation
        /// </summary>
        internal class OrOperator : LogicalOperator
        {
            public override string Symbol => "||";

            public override int Precedence => 1;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r) => l || r;
        }
    }
}
