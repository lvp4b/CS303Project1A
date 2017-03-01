using System;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ExpressionParser.Evaluator;

namespace ExpressionParser
{
    internal class Program
    {
        private static void Main()
        {
            using (var container = new WindsorContainer())
            {
                container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
                container.Install(FromAssembly.This());

                var evaluator = container.Resolve<IEvaluator>();
                
                Console.WriteLine($"[{string.Join(", ", evaluator.Evaluate(Console.ReadLine()))}]");
                Console.ReadLine();
                
                container.Release(evaluator);
            }
        }
    }
}
