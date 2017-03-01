using System;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ExpressionParser.Evaluator;

namespace ExpressionParser
{
    /// <summary>
    ///     Read, evaluate, print, loop (REPL) for infix expressions
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            using (var container = new WindsorContainer())
            {
                container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
                container.Install(FromAssembly.This());

                var evaluator = container.Resolve<IEvaluator>();
                ShowSamples(evaluator, "2+2^2*3", "(1+2)*3", "1+3 > 2");

                // Check out the unit tests in ExpressionParser.Test
                // Run them using Test > Run > All Tests
                while (true)
                {
                    try
                    {
                        Console.Write(">> ");
                        var input = Console.ReadLine();
                        if (input == "")
                        {
                            break;
                        }

                        Console.WriteLine(evaluator.Evaluate(input));
                    }
                    catch (EvaluationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                
                container.Release(evaluator);
            }
        }

        private static void ShowSamples(IEvaluator evaluator, params string[] expressions)
        {
            Console.WriteLine("Sample expressions:");
            foreach (var expression in expressions)
            {
                try
                {
                    Console.WriteLine($"{expression}: {evaluator.Evaluate(expression)}");
                }
                catch (EvaluationException e)
                {
                    Console.WriteLine($"{expression}: {e.Message}");
                }
            }
            Console.WriteLine("More examples are in the ExpressionParser.Test project");
            Console.WriteLine();
        }
    }
}
