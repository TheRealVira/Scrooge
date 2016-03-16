using System;
using System.Collections.Generic;
using Scrooge.Model;
using Scrooge.Model.Internal;
using Scrooge.Service.Definitions;

namespace Scrooge.Service.Implementations.Storage
{
    // ReSharper disable once InconsistentNaming
    public class EfSQLiteStorageService : Singleton<EfSQLiteStorageService>, IStorageService, IApplicationEventListener
    {
        private readonly ILoggingService loggingService;
        private StorageContext context;

        public EfSQLiteStorageService()
        {
            this.loggingService = Singleton<ServiceController>.Instance
                .Get<ILoggingService>();
            Singleton<ServiceController>.Instance
                .Get<IApplicationEventService>()
                .RegisterApplicationEventListener(this);
        }

        public IStorageService UpdateInventory(IEnumerable<InventoryViewModel> items)
        {
            this.loggingService.WriteLine("Updating inventory items...");
            this.context.SaveChangesAsync();
            return this;
        }

        public IEnumerable<InventoryViewModel> RetrieveInventoryViewModels()
        {
            this.loggingService.WriteLine("Retrieving inventory items...");
            return this.context.Inventory;
        }

        public void ApplicationInitialized()
        {
            this.loggingService.WriteLine("EfSQLiteStorageService loading...");
            
            this.context = new StorageContext();

            this.loggingService.WriteLine("Ensuring database creation...");
            if (this.context.Database.EnsureCreated())
            {
                this.loggingService.WriteLine("ERROR: Database existance could not be guaranteed! Aborting launch!");
                Singleton<ErrorHandler>.Instance.Panic(new ApplicationException("Database existance could not be guaranteed!"));
            }

            this.UpdateInventory(Singleton<MockupStorageService>.Instance.RetrieveInventoryViewModels());

            this.loggingService.WriteLine("EfSQLiteStorageService loaded.");
        }

        public void ApplicationClosing()
        {
            this.loggingService.WriteLine("EfSQLiteStorageService deinitializing...");
            if (this.context != null)
            {
                this.context.SaveChanges();
                this.context.Dispose();
            }
            this.loggingService.WriteLine("EfSQLiteStorageService deinitialized.");
        }
    }
}
