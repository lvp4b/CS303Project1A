using System;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.Operators
{
    /// <summary>
    ///     Represents a basic arithmetic operation
    /// </summary>
    internal abstract class ArithmeticOperator : Operator
    {
        public override int Operands => 2;

        protected abstract NumericToken Evaluate(NumericToken l, NumericToken r);

        internal override NumericToken Evaluate(NumericToken[] operands)
        {
            return Evaluate(operands[1], operands[0]);
        }

        /// <summary>
        ///     Represents the multiplication operation
        /// </summary>
        internal class MultiplyOperator : ArithmeticOperator
        {
            public override string Symbol => "*";

            public override int Precedence => 6;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r) => l*r;
        }

        /// <summary>
        ///     Represents the division operation
        /// </summary>
        internal class DivideOperator : ArithmeticOperator
        {
            public override string Symbol => "/";

            public override int Precedence => 6;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r)
            {
                try
                {
                    return l / r;
                }
                catch (ArithmeticException e)
                {
                    throw new EvaluationException($"Division by zero @ char {r.Index}", e);
                }
            }
        }

        /// <summary>
        ///     Represents the modulus operation
        /// </summary>
        internal class ModulusOperator : ArithmeticOperator
        {
            public override string Symbol => "%";

            public override int Precedence => 6;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r) => l % r;
        }

        /// <summary>
        ///     Represents the addition operation
        /// </summary>
        internal class AddOperator : ArithmeticOperator
        {
            public override string Symbol => "+";

            public override int Precedence => 5;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r) => l + r;
        }

        /// <summary>
        ///     Represents the subtraction operation
        /// </summary>
        internal class SubtractOperator : ArithmeticOperator
        {
            public override string Symbol => "-";

            public override int Precedence => 5;

            protected override NumericToken Evaluate(NumericToken l, NumericToken r) => l - r;
        }
    }
}
