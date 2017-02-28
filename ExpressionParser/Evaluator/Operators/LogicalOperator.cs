using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Evaluator.Operators
{
    internal abstract class LogicalOperator : Operator
    {
        internal class NotOperator : LogicalOperator
        {
            public override string Symbol => "!";
            public override int Precedence => 8;
        }

        internal class AndOperator : LogicalOperator
        {
            public override string Symbol => "&&";
            public override int Precedence => 2;
        }

        internal class OrOperator : LogicalOperator
        {
            public override string Symbol => "||";
            public override int Precedence => 1;
        }
    }
}
