using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrooge.Model
{
    using System.Collections.ObjectModel;

    public class TaxReport
    {
        /// <summary>
        /// The financial data (purchases and sales) which consitute this report.
        /// </summary>
        private List<GroupedPurchaseAndSalesViewModel> purchasesAndSales;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxReport"/> class.
        /// </summary>
        /// <param name="purchasesAndSales">
        /// The financial data (purchases and sales) which consitute this report.
        /// </param>
        /// <param name="year">
        /// The financial year of the data constituting this report.
        /// </param>
        public TaxReport(IEnumerable<GroupedPurchaseAndSalesViewModel> purchasesAndSales, int year)
        {
            this.purchasesAndSales = new List<GroupedPurchaseAndSalesViewModel>(purchasesAndSales);
            this.Year = year;
        }

        /// <summary>
        /// Gets the amount of sales tax already returned to the financial authority.
        /// </summary>
        public decimal AdvanceTaxPayements
        {
            get
            {
                decimal sum = 0;
                foreach (
                    var purchasesAndSale in
                        this.purchasesAndSales.Where(purchasesAndSale => purchasesAndSale.Type == EntryType.Purchase))
                {
                    // Add all expenses ('purchases') that are declared as having 100 % tax, which means, that it is the payment of sales tax.
                    purchasesAndSale.PurchaseAndSales.ToList().
                        ForEach(s => sum += s.IsTaxPayment ? s.Value : 0);
                }

                return sum;
            }
        }

        /// <summary>
        /// Returns the <see cref="FinancialReport.Sales"/> (needed for financial authority).
        /// </summary>
        public decimal Sales => new FinancialReport(this.purchasesAndSales, this.Year).Sales;

        /// <summary>
        /// Retunrs the total revenues minus the total sales tax.
        /// </summary>
        public decimal NetSales
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

                return sum - this.SalesTax;
            }
        }

        /// <summary>
        /// Gets the financial data (purchases and sales) which consitute this report.
        /// </summary>
        public Collection<GroupedPurchaseAndSalesViewModel> PurchasesAndSales
        {
            get { return new Collection<GroupedPurchaseAndSalesViewModel>(this.purchasesAndSales); }

            private set { this.purchasesAndSales = new List<GroupedPurchaseAndSalesViewModel>(value); }
        }

        /// <summary>
        /// Gets or sets the total input tax to be paid.
        /// </summary>
        public decimal InputTax
        {
            get
            {
                decimal sum = 0;
                foreach (
                    var purchasesAndSale in
                        this.purchasesAndSales.Where(purchasesAndSale => purchasesAndSale.Type == EntryType.Purchase))
                {
                    purchasesAndSale.PurchaseAndSales.ToList().
                        ForEach(p => sum += p.EntryDate.Year == this.Year && !p.IsTaxPayment ? p.Value*(p.St/100) : 0);
                }

                return sum;
            }
        }

        /// <summary>
        /// Gets the real amount of money to be paid to the financial authority.
        /// </summary>
        public decimal OutstandingMoney => this.TaxPayable - this.AdvanceTaxPayements;

        /// <summary>
        /// Gets or sets the total sales tax to be paid.
        /// </summary>
        public decimal SalesTax
        {
            get
            {
                decimal sum = 0;
                foreach (
                    var purchasesAndSale in
                        this.purchasesAndSales.Where(purchasesAndSale => purchasesAndSale.Type == EntryType.Sale))
                {
                    purchasesAndSale.PurchaseAndSales.ToList().
                        ForEach(s => sum += s.EntryDate.Year == this.Year && !s.IsTaxPayment ? s.Value*(s.St/100) : 0);
                }

                return sum;
            }
        }

        /// <summary>
        /// Gets or sets the netto amount of taxes to be paid to the finance authority (without the advanced tax payments).
        /// </summary>
        public decimal TaxPayable => this.SalesTax - this.InputTax;

        /// <summary>
        /// Gets or sets the financial year of the data constituting this report.
        /// </summary>
        public int Year { get; }
    }
}