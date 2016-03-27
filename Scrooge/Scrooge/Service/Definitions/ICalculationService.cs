namespace Scrooge.Service.Definitions
{
    /// <summary>
    ///     Defines a service that provides accounting calculations.
    /// </summary>
    public interface ICalculationService
    {
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