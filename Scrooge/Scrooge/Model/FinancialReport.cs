using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrooge.Model
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class FinancialReport : INotifyPropertyChanged
    {
        private List<GroupedPurchaseAndSalesViewModel> purchasesAndSales;
         
        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialReport"/> class.
        /// </summary>
        public FinancialReport(IEnumerable<GroupedPurchaseAndSalesViewModel> purchasesAndSales, int year)
        {
            this.purchasesAndSales = new List<GroupedPurchaseAndSalesViewModel>(purchasesAndSales);
            this.Year = year;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Collection<GroupedPurchaseAndSalesViewModel> PurchasesAndSales
        {
            get
            {
                return new Collection<GroupedPurchaseAndSalesViewModel>(this.purchasesAndSales);
            }

            set
            {
                this.purchasesAndSales = new List<GroupedPurchaseAndSalesViewModel>(value);
            }
        }

        /// <summary>
        /// Gets or sets the expenses of the financial year.
        /// </summary>
        public decimal Expenses
        {
            get
            {
                decimal sum = 0;
                foreach (var purchasesAndSale in this.purchasesAndSales.Where(purchasesAndSale => purchasesAndSale.Type == EntryType.Sale))
                {
                    purchasesAndSale.PurchaseAndSales.ForEach(p => sum += p.Value);
                }

                return sum;
            }
        }

        /// <summary>
        /// Gets or sets the profit or loss of the financial year.
        /// </summary>
        public decimal Result => this.Revenues - this.Expenses;

        /// <summary>
        /// Gets or sets the revenues of the financial year.
        /// </summary>
        public decimal Revenues
        {
            get
            {
                decimal sum = 0;
                foreach (var purchasesAndSale in this.purchasesAndSales.Where(purchasesAndSale => purchasesAndSale.Type == EntryType.Purchase))
                {
                    purchasesAndSale.PurchaseAndSales.ForEach(p => sum += p.Value);
                }

                return sum;
            }
        }

        /// <summary>
        /// Gets or sets the financial year of the data constituting this report.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Notifies that a property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the changed property (generated automatically using the <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
