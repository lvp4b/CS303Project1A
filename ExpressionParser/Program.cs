using System;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ExpressionParser.Evaluator.Tokens;

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

                var tokenizer = container.Resolve<ITokenizer>();

                Console.WriteLine($"[{string.Join(", ", tokenizer.GetTokens())}]");
                Console.ReadLine();

                container.Release(tokenizer);
            }
        }
    }
}
