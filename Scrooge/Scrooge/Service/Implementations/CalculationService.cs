namespace Scrooge.Service.Implementations
{
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
        /// The standard modifier for VAT of legally benefited products in Austria.
        /// </summary>
        private const decimal BenefitedVAT = (decimal)0.1;

        /// <summary>
        /// The standard modifier for VAT in Austria.
        /// </summary>
        private const decimal VATModifier = (decimal)0.2;

        /// <inheritdoc/>
        public decimal GetVAT(decimal netValue, bool benefited)
        {
            return benefited ? netValue * CalculationService.BenefitedVAT : netValue * CalculationService.VATModifier;
        }

        /// <inheritdoc/>
        public decimal GetNetValue(decimal grossValue, bool benefited)
        {
            return benefited
                       ? grossValue / (1 + CalculationService.BenefitedVAT)
                       : grossValue / (1 + CalculationService.VATModifier);
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
            int taxPayable = 0;
            foreach (var purchasesAndSale in purchasesAndSales)
            {
                if (purchasesAndSale.Type == EntryType.Purchase)
                {
                }
            }

            return taxPayable;
        }
    }
}
