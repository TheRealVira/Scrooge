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
        private bool initialized;

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
            if (this.initialized)
            {
                this.Get<ILoggingService>()?.WriteLine("Services already registered, nothing to be done.");
                return;
            }

            this.initialized = true;
            this.Register<ILoggingService>(Singleton<DebugLoggingService>.Instance);
            this.Register<IApplicationEventService>(Singleton<GUIApplicationEventService>.Instance);
            this.Register<IStorageService>(Singleton<EfSQLiteStorageService>.Instance);
            this.Register<ICalculationService>(Singleton<CalculationService>.Instance);
            this.Register<IDataExportService>(Singleton<DataExportService>.Instance);
            this.Register<IWarningService>(Singleton<WarningService>.Instance);
            this.Get<ILoggingService>()?.WriteLine("Services registered");
        }
    }
}