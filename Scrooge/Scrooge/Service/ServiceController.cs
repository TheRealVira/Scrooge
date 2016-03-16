using System;
using System.Collections.Generic;
using Scrooge.Service.Definitions;
using Scrooge.Service.Implementations;
using Scrooge.Service.Implementations.Storage;

namespace Scrooge.Service
{
    public class ServiceController
    {
        private readonly IDictionary<Type, object> services = new Dictionary<Type, object>();

        public ServiceController Register<T>(T service) where T : class
        {
            var type = typeof (T);
            this.services.Add(type, service);
            this.Get<ILoggingService>()?.WriteLine($"Service of type '{type.Name}' registered");
            return this;
        }

        public T Get<T>() where T : class
        {
            var type = typeof (T);
            if (this.services.ContainsKey(type))
            {
                return this.services[type] as T;
            }
            this.Get<ILoggingService>()?.WriteLine($"Service of type '{type.Name}' not found");
            return null;
        }

        public void InitializeServices()
        {
            this.Register<ILoggingService>(Singleton<DebugLoggingService>.Instance);
            this.Register<IApplicationEventService>(Singleton<GUIApplicationEventService>.Instance);
            //this.Register<IStorageService>(Singleton<MockupStorageService>.Instance);
            this.Register<IStorageService>(Singleton<EfSQLiteStorageService>.Instance);
            this.Get<ILoggingService>()?.WriteLine("Services registered");
        }
    }
}