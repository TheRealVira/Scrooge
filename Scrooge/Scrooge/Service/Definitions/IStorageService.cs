using System.Collections.Generic;
using Scrooge.Model;

namespace Scrooge.Service.Definitions
{
    public interface IStorageService
    {
        IStorageService AddInventoryItem(InventoryViewModel item);

        IEnumerable<InventoryViewModel> RetrieveInventoryViewModels();
    }
}
