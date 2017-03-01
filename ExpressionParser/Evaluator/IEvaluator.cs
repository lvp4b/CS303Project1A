namespace ExpressionParser.Evaluator
{
    /// <summary>
    ///     Evaluates an infix expression
    /// </summary>
    public interface IEvaluator
    {
        /// <summary>
        ///     Evaluates the specified infix expression
        /// </summary>
        /// <param name="expression">The infix expression to evaluate</param>
        /// <returns>The result of the expression</returns>
        /// <exception cref="EvaluationException">If the expression is invalid</exception>
        int Evaluate(string expression);
    }
}
