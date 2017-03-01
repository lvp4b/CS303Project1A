using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.Operators
{
    /// <summary>
    ///     Represents the negation operation
    /// </summary>
    internal class NegationOperator : Operator
    {
        public override string Symbol => "-";

        public override int Precedence => 8;

        public override int Operands => 1;

        public override string ToString() => "NEG";

        protected override NumericToken Evaluate(NumericToken[] operands) => -operands[0];
    }
}