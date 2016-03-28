using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Scrooge.Model
{
    public class GroupedPurchaseAndSalesViewModel: INotifyPropertyChanged, IEquatable<GroupedPurchaseAndSalesViewModel>
    {
        private string groupName;
        private bool isSelected;

        // Explicit constructor needed for serialization, do not remove!
        public GroupedPurchaseAndSalesViewModel()
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

        [Key]
        public uint ID { get; set; }

        public GroupedPurchaseAndSalesViewModel(EntryType type)
        {
            Type = type;
        }

        public ObservableCollection<PurchaseAndSalesViewModel> PurchaseAndSales { get; set; }

        public EntryType Type { get; protected set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Equals(GroupedPurchaseAndSalesViewModel other)
        {
            return other.ID == this.ID;
        }
    }
}
