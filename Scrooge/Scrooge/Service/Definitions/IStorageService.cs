using System.Collections.Generic;
using Scrooge.Model;

namespace Scrooge.Service.Definitions
{
    public interface IStorageService
    {
        #region Inventory Services
        //IStorageService AddInventoryItem(InventoryViewModel item);

        /// <summary>
        /// Updates the inventory (remove removed ones and add the added ones).
        /// </summary>
        /// <param name="items">The items which should be the inventory items stored in the DB.</param>
        /// <returns>The service which is used for updating the DB.</returns>
        IStorageService UpdateInventory(IEnumerable<InventoryViewModel> items);

        /// <summary>
        /// Retrieves the inventory view models from the DB.
        /// </summary>
        /// <returns>The retrieved inventory view models from the DB.</returns>
        IEnumerable<InventoryViewModel> RetrieveInventoryViewModels(); // copy

        // Maybe...        
        ///// <summary>
        ///// Retrieves the inventory view models from the DB (whith a applied filter).
        ///// </summary>
        ///// <param name="filter">The filter.</param>
        ///// <returns>The retrieved inventory view models from the DB (with the applied filter).</returns>
        //IEnumerable<InventoryViewModel> RetrieveInventoryViewModels(Predicate<InventoryViewModel> filter); // copy
        #endregion


    }
}
