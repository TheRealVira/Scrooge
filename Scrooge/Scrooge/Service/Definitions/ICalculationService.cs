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
        ///     Calculates the VAT of a business operation having a specific net value.
        /// </summary>
        /// <param name="netValue">
        ///     The net value of the business operation.
        /// </param>
        /// <param name="benefited">
        ///     A value indicating whether the products involved in the business operation are benefited by law (leads to lower tax
        ///     rate).
        /// </param>
        /// <returns>
        ///     The VAT of a business operation having a specific net value.
        /// </returns>
        decimal GetVAT(decimal netValue, bool benefited);

        /// <summary>
        ///     Calculates the net value of a business operation having a specific gross value.
        /// </summary>
        /// <param name="grossValue">
        ///     The gross value of the business operation.
        /// </param>
        /// <param name="benefited">
        ///     A value indicating whether the products involved in the business operation are benefited by law (leads to lower tax
        ///     rate).
        /// </param>
        /// <returns>
        ///     The net value of a business operation having a specific net value.
        /// </returns>
        decimal GetNetValue(decimal grossValue, bool benefited);
    }
}