using System.Collections.Generic;
using ExpressionParser.Evaluator.Tokens;

namespace ExpressionParser.Evaluator
{
    public interface IEvaluator
    {
        int Evaluate(IEnumerable<Token> infixTokens);
    }
}
