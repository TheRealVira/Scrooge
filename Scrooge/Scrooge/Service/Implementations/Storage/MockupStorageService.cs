using System;
using System.Collections.Generic;
using Scrooge.Model;
using Scrooge.Service.Definitions;

namespace Scrooge.Service.Implementations.Storage
{
    public class MockupStorageService : IStorageService
    {
        private ILoggingService logger;
        private IList<InventoryViewModel> mockupInventory;

        public MockupStorageService()
        {
            this.mockupInventory = new List<InventoryViewModel>
            {
                new InventoryViewModel()
                {
                    Acquisitions = new List<Aquisition>
                    {
                        new Aquisition(new DateTime(1998, 5, 13), 70m)
                    },
                    AssetValue = 12,
                    AcquisitionValue = 15m,
                    DateOfAcquisition = new DateTime(1998, 4, 2),
                    Disposal = 0m,
                    Duration = 10m,
                    IsSelected = false,
                    Name = "Computer"
                },
                new InventoryViewModel()
                {
                    Acquisitions = new List<Aquisition>(),
                    AssetValue = 12,
                    AcquisitionValue = 15m,
                    DateOfAcquisition = new DateTime(1999, 4, 2),
                    Disposal = 0m,
                    Duration = 10m,
                    IsSelected = false,
                    Name = "Auto"
                },
                new InventoryViewModel()
                {
                    Acquisitions = new List<Aquisition>
                    {
                        new Aquisition(new DateTime(1999, 3, 18), 7m),
                        new Aquisition(new DateTime(1999, 4, 18), 7m)
                    },
                    AssetValue = 12,
                    AcquisitionValue = 15m,
                    DateOfAcquisition = new DateTime(1998, 8, 2),
                    Disposal = 0m,
                    Duration = 10m,
                    IsSelected = false,
                    Name = "Bildschirm"
                }
            };
            logger = Singleton<ServiceController>.Instance.Get<ILoggingService>();
        }

        public IStorageService UpdateInventory(IEnumerable<InventoryViewModel> items)
        {
            logger.WriteLine("Updating mockup inventory");
            
            return this;
        }

        public IEnumerable<InventoryViewModel> RetrieveInventoryViewModels()
        {
            logger.WriteLine("Retrieving mockup inventory");
            return this.mockupInventory;
        }
    }
}