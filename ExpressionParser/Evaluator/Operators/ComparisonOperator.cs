namespace ExpressionParser.Evaluator.Operators
{
    internal abstract class ComparisonOperator : Operator
    {
        internal class GreaterThanOperator : ComparisonOperator
        {
            public override string Symbol => ">";
            public override int Precedence => 4;
        }

        internal class GreaterOrEqualOperator : ComparisonOperator
        {
            public override string Symbol => ">=";
            public override int Precedence => 4;
        }

        internal class LessThanOperator : ComparisonOperator
        {
            public override string Symbol => "<";
            public override int Precedence => 4;
        }

        internal class LessOrEqualOperator : ComparisonOperator
        {
            public override string Symbol => "<=";
            public override int Precedence => 4;
        }
    }
}
