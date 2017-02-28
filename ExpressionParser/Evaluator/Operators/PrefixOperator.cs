namespace ExpressionParser.Evaluator.Operators
{
    internal abstract class PrefixOperator : Operator
    {
        internal class IncrementOperator : PrefixOperator
        {
            public override string Symbol => "++";
            public override int Precedence => 8;
        }

        internal class DecrementOperator : PrefixOperator
        {
            public override string Symbol => "--";
            public override int Precedence => 8;
        }

    }
}
