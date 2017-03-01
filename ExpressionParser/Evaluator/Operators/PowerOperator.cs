using System;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator.Operators
{
    /// <summary>
    ///     Represents the power operation
    /// </summary>
    internal class PowerOperator : Operator
    {
        public override string Symbol => "^";

        public override int Precedence => 7;

        public override int Operands => 2;
        
        protected override NumericToken Evaluate(NumericToken[] operands)
        {
            return (int) Math.Pow(operands[1], operands[0]);
        }
    }
}