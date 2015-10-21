using System;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SIContainer = SimpleInjector.Container;

namespace Kekiri.IoC.SimpleInjector
{
    internal class SimpleInjectorContainer : Container
    {
        private Scope _scope;

        private readonly Lazy<SIContainer> Container =
            new Lazy<SIContainer>(
                () => IocConfig.BuildContainer == null
                    ? new SIContainer()
                    : IocConfig.BuildContainer());

        protected override T OnResolve<T>()
        {
            var container = Container.Value;

            if (_scope == null)
            {
                _scope = container.BeginExecutionContextScope();

                foreach (var obj in Fakes)
                {
                    var objCopy = obj;
                    container.Register(obj.GetType(), () => objCopy, new ExecutionContextScopeLifestyle());
                    foreach (var i in obj.GetType().GetInterfaces())
                        container.Register(i, () => objCopy, new ExecutionContextScopeLifestyle());
                }
            }

            return container.GetInstance<T>();
        }

        public override void Dispose()
        {
            if (_scope != null)
                _scope.Dispose();
        }
    }
}