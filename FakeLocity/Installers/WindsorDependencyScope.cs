namespace FakeLocity.Installers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using Castle.MicroKernel.Lifestyle;
    using Castle.Windsor;

    internal sealed class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer container;
        private readonly IDisposable scope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
            scope = container.BeginScope();
        }

        public object GetService(Type type)
        {
            return container.Kernel.HasComponent(type) ? container.Resolve(type) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return container.ResolveAll(t).Cast<object>().ToArray();
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}