﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Scrooge.Model
{
    public class InventoryViewModel : INotifyPropertyChanged, IEquatable<InventoryViewModel>
    {
        private decimal acquisitionValue;
        private decimal appreciation;
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
                if (years < this.Duration)
                {
                    var firstDepreciation = this.DateOfAcquisition.Month > 6 ? this.Deprecation/2 : this.Deprecation;
                    return this.AcquisitionValue + this.Appreciation - firstDepreciation - this.Deprecation*(years - 1);
                }
                return 0;
            }
        }
        
        public List<Acquisition> Acquisitions { get; set; }

        /// <summary>
        ///     Gets or sets the appreciation of the <see cref="BalanceValue" />.
        /// </summary>
        public decimal Appreciation
        {
            get { return this.appreciation; }

            set
            {
                if (this.appreciation == value)
                {
                    return;
                }

                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        "InventoryViewModel.Appreciation needs to be greater than or equal 0.");
                }

                this.appreciation = value;
                this.OnPropertyChanged();
            }
        }

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

        public bool Equals(InventoryViewModel other)
        {
            return this.ID == other.ID;
        }
    }
}