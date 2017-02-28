namespace ExpressionParser.Evaluator.Operators
{
    internal abstract class PostfixOperator : Operator
    {
        internal class OpenParenthesesOperator : PostfixOperator
        {
            public override string Symbol => "(";
            public override int Precedence => 0;
        }

        internal class CloseParenthesesOperator : PostfixOperator
        {
            public override string Symbol => ")";
            public override int Precedence => 0;
        }

        internal class OpenBracketOperator : PostfixOperator
        {
            public override string Symbol => "{";
            public override int Precedence => 0;
        }
        
        internal class CloseBracketOperator : PostfixOperator
        {
            public override string Symbol => "}";
            public override int Precedence => 0;
        }
    }
}
