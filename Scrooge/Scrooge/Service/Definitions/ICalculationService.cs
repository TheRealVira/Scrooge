namespace Scrooge.Service.Definitions
{
    using System.Collections.Generic;
    using Scrooge.Model;

    /// <summary>
    ///     Defines a service that provides accounting calculations.
    /// </summary>
    public interface ICalculationService
    {
        /// <summary>
        /// Calculates the tax payable of the given data.
        /// </summary>
        /// <param name="purchasesAndSales">
        /// The data to be used.
        /// </param>
        /// <param name="year">
        /// The year to be used (only data within the given year will be considered).
        /// </param>
        /// <returns>
        /// The tax payable of the given data.
        /// </returns>
        decimal CalculateTaxPayable(IEnumerable<GroupedPurchaseAndSalesViewModel> purchasesAndSales, int year);

        /// <summary>
        /// Generates a financial report for the given year and the given purchase and sale data (only data within the given year will be considered).
        /// </summary>
        /// <param name="purchasesAndSales">
        /// The data to be used.
        /// </param>
        /// <param name="year">
        /// The year of the report (only data within the given year will be considered).
        /// </param>
        /// <returns>
        /// A financial report for the given year and the given purchase and sale data (only data within the given year will be considered).
        /// </returns>
        FinancialReport GenerateFinancialReport(
            IEnumerable<GroupedPurchaseAndSalesViewModel> purchasesAndSales,
            int year);

        /// <summary>
        /// Generates a tax report for the given year and the given purchase and sale data (only data within the given year will be considered).
        /// </summary>
        /// <param name="purchasesAndSales">
        /// The data to be used.
        /// </param>
        /// <param name="year">
        /// The year of the report (only data within the given year will be considered).
        /// </param>
        /// <returns>
        /// A tax report for the given year and the given purchase and sale data (only data within the given year will be considered).
        /// </returns>
        TaxReport GenerateTaxReport(
            IEnumerable<GroupedPurchaseAndSalesViewModel> purchasesAndSales,
            int year);

        /// <summary>
        /// Gets all inventory items that changed their value in the given year (also new acquistions and dispositions).
        /// </summary>
        /// <param name="inventoryViewModels">
        /// The inventory items to be checked against.
        /// </param>
        /// <param name="year">
        /// The year to be searched for.
        /// </param>
        /// <returns>
        /// All inventory items that changed their value in the given year (also new acquistions and dispositions).
        /// </returns>
        IEnumerable<InventoryViewModel> GetChangedInventoryItems(
            IEnumerable<InventoryViewModel> inventoryViewModels,
            int year);
    }
}