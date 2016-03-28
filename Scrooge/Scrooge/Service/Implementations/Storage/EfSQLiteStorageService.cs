using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public void ApplicationInitialized()
        {
            this.loggingService.WriteLine("EfSQLiteStorageService loading...");

            this.context = new StorageContext();

            this.loggingService.WriteLine("Ensuring database creation...");
            if (this.context.Database.EnsureCreated())
            {
                this.loggingService.WriteLine("Created new database.");
            }
            else
            {
                this.loggingService.WriteLine("Database existed. No action taken.");
            }

            /*if (this.context.Database)
            {
                this.loggingService.WriteLine($"ERROR: Database connection could not be guaranteed (Current state: {this.context.Database.GetDbConnection().State})! Aborting launch!");
                Singleton<ErrorHandler>.Instance.Panic(
                    new ApplicationException("Database connection could not be guaranteed!"));
            }*/

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

        public IStorageService UpdateInventory(IEnumerable<InventoryViewModel> items)
        {
            this.loggingService.WriteLine("Updating inventory items...");
            EfSQLiteStorageService.AddRemoveUpdateList(this.context.Inventory, items.ToList());
            this.context.SaveChangesAsync();
            return this;
        }

        public IList<InventoryViewModel> RetrieveInventoryViewModels()
        {
            this.loggingService.WriteLine("Retrieving inventory items...");
            return this.context.Set<InventoryViewModel>().Include(x => x.Acquisitions).ToList();
        }

        public IStorageService UpdateGroupedPurchaseAndSales(IEnumerable<GroupedPurchaseAndSalesViewModel> items)
        {
            this.loggingService.WriteLine("Updating grouped purchase and sales items...");
            EfSQLiteStorageService.AddRemoveUpdateList(this.context.GroupedPurchasesAndSales, items.ToList());
            this.context.SaveChangesAsync();
            return this;
        }

        public IList<GroupedPurchaseAndSalesViewModel> RetrieveGroupedPurchaseAndSalesViewModels()
        {
            this.loggingService.WriteLine("Retrieving grouped purchase and sales items...");
            return this.context.Set<GroupedPurchaseAndSalesViewModel>().Include(x => x.PurchaseAndSales).ToList();
        }

        public IStorageService UpdateKilometerEntry(IEnumerable<KilometerEntryViewModel> items)
        {
            this.loggingService.WriteLine("Updating kilometer entries...");
            EfSQLiteStorageService.AddRemoveUpdateList(this.context.KilometerEntries, items.ToList());
            this.context.SaveChangesAsync();
            return this;
        }

        public IList<KilometerEntryViewModel> RetrieveKilometerEntryViewModels()
        {
            this.loggingService.WriteLine("Retrieving kilometer entries...");
            return this.context.Set<KilometerEntryViewModel>().ToList();
        }

        private static void AddRemoveUpdateList<T>(DbSet<T> list, IEnumerable<T> newList) where T : class
        {
            IEnumerable<T> stagedRemovals = list.Where(t => !newList.Contains(t));

            foreach (var stagedRemoval in stagedRemovals)
            {
                list.Remove(stagedRemoval);
            }

            foreach (var t in newList.Where(t => !list.Contains(t)))
            {
                list.Add(t);
            }
        }
    }
}