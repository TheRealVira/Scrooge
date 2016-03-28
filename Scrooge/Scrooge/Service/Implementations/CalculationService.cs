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
        /// 
        /// </param>
        /// <param name="year">
        /// 
        /// </param>
        /// <returns></returns>
        public FinancialReport GenerateFinancialReport(
            IEnumerable<GroupedPurchaseAndSalesViewModel> purchasesAndSales,
            int year)
        {
            return new FinancialReport(purchasesAndSales, year);
        }

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
