using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace ExpressionParser.Evaluator.Tokens
{
    /// <summary>
    ///     Installs the tokenizer into the application
    /// </summary>
    public class TokensInstaller : IWindsorInstaller
    {
        /// <summary>
        ///   Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITokenizer>().ImplementedBy<Tokenizer>().LifestyleTransient());
            container.Register(Classes.FromThisAssembly().IncludeNonPublicTypes().BasedOn<Token.IProvider>()
                .WithServiceBase().LifestyleTransient());
        }
    }
}
