using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace ExpressionParser.Evaluator
{
    public class EvaluatorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IEvaluator>().ImplementedBy<Evaluator>().LifestyleTransient());
        }
    }
}
