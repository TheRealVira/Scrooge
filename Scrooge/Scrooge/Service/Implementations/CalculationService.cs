namespace Scrooge.Service.Implementations
{
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
    }
}
