using System.Collections.Generic;
using Scrooge.Model;
using Scrooge.Service.Definitions;

namespace Scrooge.Service.Implementations.Storage
{
    public class MockupStorageService : IStorageService
    {
        public IStorageService UpdateInventory(IEnumerable<InventoryViewModel> items)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<InventoryViewModel> RetrieveInventoryViewModels()
        {
            throw new System.NotImplementedException();
        }
    }
}
