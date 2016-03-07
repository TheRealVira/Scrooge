using System.Collections.Generic;
using System.Threading.Tasks;
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

        public IStorageService AddInventoryItem(InventoryViewModel item)
        {
            this.loggingService.WriteLine("Adding inventory item: " + item);
            return this;
        }

        public IStorageService UpdateInventory(IEnumerable<InventoryViewModel> items)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<InventoryViewModel> RetrieveInventoryViewModels()
        {
            this.loggingService.WriteLine("Retrieving inventory items...");
            return null;
        }

        public async Task ApplicationInitialized()
        {
            this.loggingService.WriteLine("EfSQLiteStorageService loading...");
            
            this.context = new StorageContext();

            this.loggingService.WriteLine("Ensuring database creation...");
            /*if (await this.context.Database.EnsureCreatedAsync())
            {
                
            }*/

            this.loggingService.WriteLine("EfSQLiteStorageService loaded.");
        }

        public async Task ApplicationClosing()
        {
            this.loggingService.WriteLine("EfSQLiteStorageService deinitializing...");
            this.loggingService.WriteLine("EfSQLiteStorageService deinitialized.");
        }
    }
}
