using System.Collections.Generic;
using System.Security.AccessControl;
using MahApps.Metro;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Scrooge.Model;
using Scrooge.Service.Definitions;

namespace Scrooge.Service.Implementations.Storage
{
    public class StorageContext : DbContext
    {
        public DbSet<InventoryViewModel> Inventory { get; set; }
        public DbSet<KilometerEntryViewModel> KilometerEntries { get; set; }
        public DbSet<GroupedPurchaseAndSalesViewModel> GroupedPurchasesAndSales { get; set; }
        public DbSet<WarningViewModel> Warnings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggingService = Singleton<ServiceController>.Instance.Get<ILoggingService>();
            loggingService.WriteLine("Configuring database connection...");

            var connectionStringBuilder = new SqliteConnectionStringBuilder {DataSource = "scrooge.db"};
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            loggingService.WriteLine("Using connection string: " + connectionString);

            optionsBuilder.UseSqlite(connection);

            base.OnConfiguring(optionsBuilder);

            loggingService.WriteLine("Database configured!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var loggingService = Singleton<ServiceController>.Instance.Get<ILoggingService>();

            loggingService.WriteLine("Configuring relationships...");

            base.OnModelCreating(modelBuilder);

            loggingService.WriteLine("Configuring done");
        }
    }
}