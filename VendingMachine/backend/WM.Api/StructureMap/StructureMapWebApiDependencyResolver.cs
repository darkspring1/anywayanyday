using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace VM.Api.StructureMap
{
    public class StructureMapWebApiDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapWebApiDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public StructureMapWebApiDependencyResolver(Registry registry)
        {
            _container = new Container(registry);
            Debug.WriteLine(_container.WhatDoIHave());
        }

        #region IDependencyResolver members

        public object GetService(Type serviceType)
        {
            Debug.WriteLine("IDependencyResolver. Request for service of type: {0}", serviceType);
            return serviceType.IsAbstract || serviceType.IsInterface
                ? _container.TryGetInstance(serviceType)
                : _container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            Debug.WriteLine("IDependencyResolver. Request for all services of type: {0}", serviceType);
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public IDependencyScope BeginScope()
        {
            var nestedContainer = _container.GetNestedContainer();

            Debug.WriteLine("Begin new scope. Name:{0}", (object)nestedContainer.Name);
            var resolver = new StructureMapWebApiDependencyResolver(nestedContainer);

            return resolver;
        }

        public void Dispose()
        {
            Debug.WriteLine("Dispose scope. Name: {0}", (object)_container.Name);
            _container.Dispose();
        }

        #endregion

    }
}
