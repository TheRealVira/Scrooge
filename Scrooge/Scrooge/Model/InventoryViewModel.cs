using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Scrooge.Model
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        private decimal assetValue;
        private DateTime dateOfAcquisition;
        private decimal disposal;
        private decimal duration;
        private bool isSelected;
        private string name;
        private decimal acquisitionValue;

        [Key]
        public uint ID { get; set; }

        // Explicit constructor needed for serialization, do not remove!
        public InventoryViewModel()
        {
            if (this.Acquisitions == null)
            {
                this.Acquisitions = new List<Aquisition>();
            }
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

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name == value) return;
                this.name = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime DateOfAcquisition
        {
            get { return this.dateOfAcquisition; }
            set
            {
                if (this.dateOfAcquisition == value) return;
                this.dateOfAcquisition = value;
                this.OnPropertyChanged();
            }
        }

        public decimal AcquisitionValue
        {
            get { return this.acquisitionValue; }
            set
            {
                if (this.acquisitionValue == value) return;
                this.acquisitionValue = value;
                this.OnPropertyChanged();
            }
        }

        public decimal BalanceValue => 0; // TODO

        public List<Aquisition> Acquisitions;

        ////public decimal Acquisition => 0; // TODO

        public decimal Duration
        {
            get { return this.duration; }
            set
            {
                if (this.duration == value) return;
                this.duration = value == 0 ? 1 : value;
                this.OnPropertyChanged();
            }
        }

        public decimal Percentage => 100m / this.duration;

        public decimal Deprecation => this.AcquisitionValue / this.Duration;

        public decimal Disposal
        {
            get { return this.disposal; }
            set
            {
                if (this.disposal == value) return;
                this.disposal = value;
                this.OnPropertyChanged();
            }
        }

        public decimal AssetValue
        {
            get { return this.assetValue; }
            set
            {
                if (this.assetValue == value) return;
                this.assetValue = value;
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