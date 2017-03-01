﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace ExpressionParser.Evaluator
{
    /// <summary>
    ///     Installs the expression evaluator into the application
    /// </summary>
    public class EvaluatorInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IEvaluator>().ImplementedBy<Evaluator>().LifestyleTransient());
        }
    }
}
