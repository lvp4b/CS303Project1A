using System;

namespace ExpressionParser.UserIO.Input
{
    /// <summary>
    ///     Gets input expression from the user
    /// </summary>
    internal class UserInput : IInput
    {
        /// <summary>
        ///     Gets the input expression
        /// </summary>
        /// <returns>The input expression</returns>
        public string Get() => Console.ReadLine();
    }
}