using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
        IList<InventoryViewModel> RetrieveInventoryViewModels(); // copy

        // Maybe...        
        ///// <summary>
        ///// Retrieves the inventory view models from the DB (whith a applied filter).
        ///// </summary>
        ///// <param name="filter">The filter.</param>
        ///// <returns>The retrieved inventory view models from the DB (with the applied filter).</returns>
        //IEnumerable<InventoryViewModel> RetrieveInventoryViewModels(Predicate<InventoryViewModel> filter); // copy
        #endregion

        #region Purchase and Sales Services
        //IStorageService AddGroupedPurchaseAndSalesViewModelItem(PurchaseAndSalesViewModel item);

        /// <summary>
        /// Updates the PurchaseAndSales (remove removed ones and add the added ones).
        /// </summary>
        /// <param name="items">The items which should be the PurchaseAndSales items stored in the DB.</param>
        /// <returns>The service which is used for updating the DB.</returns>
        IStorageService UpdateGroupedPurchaseAndSales(IEnumerable<GroupedPurchaseAndSalesViewModel> items);

        /// <summary>
        /// Retrieves the purchase and sales view models from the DB.
        /// </summary>
        /// <returns>The retrieved purchase and sales view models from the DB.</returns>
        IList<GroupedPurchaseAndSalesViewModel> RetrieveGroupedPurchaseAndSalesViewModels(); // copy

        // Maybe...        
        ///// <summary>
        ///// Retrieves the purchase and sales view models from the DB (whith a applied filter).
        ///// </summary>
        ///// <param name="filter">The filter.</param>
        ///// <returns>The retrieved purchase and sales view models from the DB (with the applied filter).</returns>
        //IEnumerable<GroupedPurchaseAndSalesViewModel> RetrieveGroupedPurchaseAndSalesViewModel(Predicate<GroupedPurchaseAndSalesViewModel> filter); // copy
        #endregion

        #region Kilometer Entry Services
        //IStorageService AddKilometerEntryItem(InventoryViewModel item);

        /// <summary>
        /// Updates the inventory (remove removed ones and add the added ones).
        /// </summary>
        /// <param name="items">The items which should be the inventory items stored in the DB.</param>
        /// <returns>The service which is used for updating the DB.</returns>
        IStorageService UpdateKilometerEntry(IEnumerable<KilometerEntryViewModel> items);

        /// <summary>
        /// Retrieves the kilometer entry view models from the DB.
        /// </summary>
        /// <returns>The retrieved kilometer entry view models from the DB.</returns>
        IList<KilometerEntryViewModel> RetrieveKilometerEntryViewModels(); // copy

        // Maybe...        
        ///// <summary>
        ///// Retrieves the entry view view models from the DB (whith a applied filter).
        ///// </summary>
        ///// <param name="filter">The filter.</param>
        ///// <returns>The retrieved entry view models from the DB (with the applied filter).</returns>
        //IEnumerable<KilometerEntryViewModel> RetrieveInventoryViewModels(Predicate<KilometerEntryViewModel> filter); // copy
        #endregion
    }
}
