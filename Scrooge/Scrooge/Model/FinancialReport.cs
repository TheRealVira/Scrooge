namespace Scrooge.Model
{
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Describes the report of the financial performance.
    /// </summary>
    public class FinancialReport
    {
        /// <summary>
        /// The financial data (purchases and sales) which consitute this report.
        /// </summary>
        private List<GroupedPurchaseAndSalesViewModel> purchasesAndSales;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialReport"/> class.
        /// </summary>
        /// <param name="purchasesAndSales">
        /// The financial data (purchases and sales) which consitute this report.
        /// </param>
        /// <param name="year">
        /// The financial year of the data constituting this report.
        /// </param>
        public FinancialReport(IEnumerable<GroupedPurchaseAndSalesViewModel> purchasesAndSales, int year)
        {
            this.purchasesAndSales = new List<GroupedPurchaseAndSalesViewModel>(purchasesAndSales);
            this.Year = year;
        }

        /// <summary>
        /// Gets or sets the expenses of the financial year.
        /// </summary>
        public decimal Purchases
        {
            get
            {
                decimal sum = 0;
                foreach (var purchasesAndSale in this.purchasesAndSales.Where(purchasesAndSale => purchasesAndSale.Type == EntryType.Purchase))
                {
                    purchasesAndSale.PurchaseAndSales.ToList().
                        ForEach(p => sum += p.EntryDate.Year == this.Year ? p.Value : 0);
                }

                return sum;
            }
        }

        /// <summary>
        /// Gets the financial data (purchases and sales) which consitute this report.
        /// </summary>
        public Collection<GroupedPurchaseAndSalesViewModel> PurchasesAndSales
        {
            get
            {
                return new Collection<GroupedPurchaseAndSalesViewModel>(this.purchasesAndSales);
            }

            private set
            {
                this.purchasesAndSales = new List<GroupedPurchaseAndSalesViewModel>(value);
            }
        }

        /// <summary>
        /// Gets or sets the profit or loss of the financial year.
        /// </summary>
        public decimal Result => this.Sales - this.Purchases;

        /// <summary>
        /// Gets or sets the revenues of the financial year.
        /// </summary>
        public decimal Sales
        {
            get
            {
                decimal sum = 0;
                foreach (
                    var purchasesAndSale in
                        this.purchasesAndSales.Where(purchasesAndSale => purchasesAndSale.Type == EntryType.Sale))
                {
                    purchasesAndSale.PurchaseAndSales.ToList().
                        ForEach(s => sum += s.EntryDate.Year == this.Year ? s.Value : 0);
                }

                return sum;
            }
        }

        /// <summary>
        /// Gets or sets the financial year of the data constituting this report.
        /// </summary>
        public int Year { get; }
    }
}
