using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Scrooge.Model
{
    // DONT ADD ME TO THE DATABASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // DONT ADD ME TO THE DATABASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // DONT ADD ME TO THE DATABASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // DONT ADD ME TO THE DATABASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // DONT ADD ME TO THE DATABASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // DONT ADD ME TO THE DATABASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // DONT ADD ME TO THE DATABASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // thx.
    class AddPurchaseOrSaleViewModel
    {
        private DateTime rEDate, entryDate;
        private string groupName, text, receipt;
        private EntryType _type;
        private decimal st, value;

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

        public string GroupName
        {
            get { return this.groupName; }
            set
            {
                if (this.groupName == value) return;
                this.groupName = value;
                this.OnPropertyChanged();
            }
        }

        public EntryType Type
        {
            get { return this._type; }
            set
            {
                if (this._type == value) return;
                this._type = value;
                this.OnPropertyChanged();
            }
        }

        public decimal St
        {
            get { return this.st; }
            set
            {
                if (this.st == value) return;
                this.st = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}