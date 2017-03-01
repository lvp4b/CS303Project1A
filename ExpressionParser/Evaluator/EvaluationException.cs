using System;

namespace ExpressionParser.Evaluator
{
    /// <summary>
    ///     Represents an error during the evaluation of an expression
    /// </summary>
    public class EvaluationException : Exception
    {
        /// <summary>
        ///     Instantiates a new evaluation exception with the specified message
        /// </summary>
        /// <param name="message">The exception message</param>
        internal EvaluationException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Instantiates a new evaluation exception with the specified message
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The cause of the excepton</param>
        internal EvaluationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}