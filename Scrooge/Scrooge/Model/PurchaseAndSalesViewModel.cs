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
        public readonly decimal St;
        private bool isSelected;

        [Key]
        public uint ID { get; set; }

        public PurchaseAndSalesViewModel(decimal steuer)
        {
            this.St = steuer;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
