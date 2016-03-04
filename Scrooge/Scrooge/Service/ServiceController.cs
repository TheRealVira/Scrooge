using System;
using System.Collections.Generic;
using Scrooge.Service.Definitions;
using Scrooge.Service.Implementations;

namespace Scrooge.Service
{
    public class ServiceController
    {
        private readonly IDictionary<Type, object> services = new Dictionary<Type, object>();

        private ServiceController()
        {
        }

        public static ServiceController Instance { get; private set; } = new ServiceController();

        public ServiceController Register<T>(T service) where T : class
        {
            var type = typeof (T);
            this.services.Add(type, service);
            this.Get<LoggingService>()?.WriteLine($"Service of type '{type.Name}' registered");
            return this;
        }

        public T Get<T>() where T : class
        {
            var type = typeof (T);
            if (this.services.ContainsKey(type))
            {
                return this.services[type] as T;
            }
            this.Get<LoggingService>()?.WriteLine($"Service of type '{type.Name}' not found");
            return null;
        }

        public void InitializeServices()
        {
            this.Register<LoggingService>(new DebugLoggingService());
            this.Register<StorageService>(new MockupStorageService());
            this.Get<LoggingService>()?.WriteLine("Services registered");
        }
    }
}