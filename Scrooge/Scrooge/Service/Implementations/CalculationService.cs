namespace Scrooge.Service.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Scrooge.Model;
    using Scrooge.Service.Definitions;

    /// <summary>
    /// Provides accounting calculations.
    /// </summary>
    public class CalculationService : ICalculationService
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
        public decimal CalculateTaxPayable(IEnumerable<GroupedPurchaseAndSalesViewModel> purchasesAndSales, int year)
        {
            return this.GenerateTaxReport(purchasesAndSales, year).TaxPayable;
        }

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
        public FinancialReport GenerateFinancialReport(
            IEnumerable<GroupedPurchaseAndSalesViewModel> purchasesAndSales,
            int year)
        {
            return new FinancialReport(purchasesAndSales, year);
        }

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
        public TaxReport GenerateTaxReport(
            IEnumerable<GroupedPurchaseAndSalesViewModel> purchasesAndSales,
            int year)
        {
            return new TaxReport(purchasesAndSales, year);
        }

        /// <summary>
        /// Gets all inventory items that changed their value within given year (also new acquistions and dispositions).
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
        public IEnumerable<InventoryViewModel> GetChangedInventoryItems(
            IEnumerable<InventoryViewModel> inventoryViewModels,
            int year)
        {
            return inventoryViewModels.Where(inventoryViewModel => year - inventoryViewModel.DateOfAcquisition.Year <= inventoryViewModel.Duration);
        }
    }
}
