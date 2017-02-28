namespace ExpressionParser.Evaluator.Operators
{
    /// <summary>
    ///     
    /// </summary>
    internal abstract class ArithmeticOperator : Operator
    {
        /// <summary>
        ///     Set the multiply operator precedence to 8
        /// </summary>
        internal class MultiplyOperator : ArithmeticOperator
        {

            public override string Symbol => "*";

            public override int Precedence => 6;

        }

        /// <summary>
        ///     Set the divide operator precedence to 8
        /// </summary>
        internal class DivideOperator : ArithmeticOperator
        {
            public override string Symbol => "/";

            public override int Precedence => 6;
        }

        /// <summary>
        ///     Set the modulus operator precedence to 6
        /// </summary>
        internal class ModulusOperator : ArithmeticOperator
        {
            public override string Symbol => "%";

            public override int Precedence => 6;
        }

        /// <summary>
        ///     Set the add operator precedence to 5
        /// </summary>
        internal class AddOperator : ArithmeticOperator
        {
            public override string Symbol => "+";

            public override int Precedence => 5;
        }

        /// <summary>
        ///     Set the subtract operator precedence to 5
        /// </summary>
        internal class SubtractOperator : ArithmeticOperator
        {
            public override string Symbol => "-";

            public override int Precedence => 5;
        }
    }
}
