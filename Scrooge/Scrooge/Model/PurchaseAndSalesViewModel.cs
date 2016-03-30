using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Scrooge.Model
{
    public class PurchaseAndSalesViewModel : INotifyPropertyChanged
    {
        private DateTime rEDate;
        private DateTime entryDate;
        private string receipt;
        private string text;
        private decimal value;
        private bool isSelected;

        /// <summary>
        /// A value indicating whether this purchase or sale is a tax payment.
        /// </summary>
        private bool isTaxPayment;

        [Key]
        public uint ID { get; set; }

        public uint GroupedPurchaseAndSalesViewModelForeignKey { get; set; }

        [ForeignKey("GroupedPurchaseAndSalesViewModelForeignKey")]
        public GroupedPurchaseAndSalesViewModel GroupedPurchaseAndSalesViewModel { get; set; }

        public PurchaseAndSalesViewModel(decimal steuer)
        {
            this.St = steuer;
        }

        // Explicit constructor needed for serialization, do not remove!
        public PurchaseAndSalesViewModel()
        {
        }

        [NotMapped]
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (this.isSelected == value) return;
                this.isSelected = value;
                this.OnPropertyChanged();
            }
        }

        public decimal St { get; protected set; }

        public DateTime EntryDate
        {
            get { return this.entryDate; }
            set
            {
                if (this.entryDate == value) return;
                this.entryDate = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime REDate
        {
            get { return this.rEDate; }
            set
            {
                if (this.rEDate == value) return;
                this.rEDate = value;
                this.OnPropertyChanged();
            }
        }

        public decimal Value
        {
            get { return this.value; }
            set
            {
                if (this.value == value) return;
                this.value = value;
                this.OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return this.text; }
            set
            {
                if (this.text == value) return;
                this.text = value;
                this.OnPropertyChanged();
            }
        }

        public string Receipt
        {
            get { return this.receipt; }
            set
            {
                if (this.receipt == value) return;
                this.receipt = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this purchase or sale is a tax payment.
        /// </summary>
        public bool IsTaxPayment
        {
            get { return this.isTaxPayment; }

            set
            {
                if (this.isTaxPayment == value)
                {
                    return;
                }

                this.isTaxPayment = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}