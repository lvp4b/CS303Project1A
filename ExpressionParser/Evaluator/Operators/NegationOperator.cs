﻿using ExpressionParser.Evaluator.Tokens;

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

        // Disambiguates between arithmetic subtraction
        public override string ToString() => "NEG";

        internal override NumericToken Evaluate(NumericToken[] operands) => -operands[0];
    }
}