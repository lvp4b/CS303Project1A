using System.Collections.Generic;
using ExpressionParser.Evaluator.Tokens;
using ExpressionParser.UserIO.Input;

namespace ExpressionParser.Evaluator
{
    public interface IEvaluator
    {
        int Evaluate(IEnumerable<Token> infixTokens);
    }
}
