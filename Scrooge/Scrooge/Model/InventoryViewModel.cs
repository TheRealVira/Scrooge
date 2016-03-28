using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Scrooge.Model
{
    public class InventoryViewModel : INotifyPropertyChanged, IEquatable<InventoryViewModel>
    {
        private decimal acquisitionValue;
        private decimal assetValue;
        private DateTime dateOfAcquisition;
        private decimal disposal;
        private decimal duration;
        private bool isSelected;
        private string name; 

        // Explicit constructor needed for serialization, do not remove!
        public InventoryViewModel()
        {
            if (this.Acquisitions == null)
            {
                this.Acquisitions = new List<Acquisition>();
                this.AppreciationList=new ObservableCollection<Acquisition>();
            }
        }

        [Key]
        public uint ID { get; set; }

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

        public decimal BalanceValue
        {
            get
            {
                var years = DateTime.Now.Year - this.DateOfAcquisition.Year;
                if (years <= this.Duration)
                {
                    var firstDepreciation = this.DateOfAcquisition.Month > 6 ? this.Deprecation / 2 : this.Deprecation;
                    return this.AcquisitionValue + this.Appreciation - firstDepreciation - this.Deprecation * (years - 1);
                }

                return 0;
            }
        }
        
        public List<Acquisition> Acquisitions { get; set; }
        public ObservableCollection<Acquisition> AppreciationList { get; set; }

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

        public decimal Percentage => 100m/this.duration;

        public decimal Deprecation => this.AcquisitionValue/this.Duration;

        /// <summary>
        /// Gets the amount of value that wasn't removed from an already depreceated intentory item (otherwise 0).
        /// </summary>
        public decimal Disposal
        {
            get
            {
                var years = DateTime.Now.Year - this.DateOfAcquisition.Year;
                if (years == this.Duration && this.AssetValue != 0)
                {
                    return this.AssetValue;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the accounting value of the inventory item at the end of the year.
        /// </summary>
        public decimal AssetValue
        {
            get
            {
                var years = DateTime.Now.Year - this.DateOfAcquisition.Year;
                if (years < this.Duration)
                {
                    if (this.DateOfAcquisition.Month > 6)
                    {
                        return this.AcquisitionValue + this.Appreciation - this.Disposal
                               - this.Deprecation * (years - 1);
                    }

                    return this.AcquisitionValue + this.Appreciation - this.Disposal - this.Deprecation * years;
                }

                return 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Equals(InventoryViewModel other)
        {
            return this.ID == other.ID;
        }
    }
}