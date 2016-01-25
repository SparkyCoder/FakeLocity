namespace FakeLocity.Installers
{
    using System.Web.Http;
    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    public class IocInstaller
    {
        private static IWindsorContainer Create()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());

            return container;
        }

        public static IWindsorContainer ConfigureWindsor(HttpConfiguration configuration)
        {
            var container = Create();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            var dependencyResolver = new WindsorDependencyResolver(container);
            configuration.DependencyResolver = dependencyResolver;

            return container;
        }  
    }
}