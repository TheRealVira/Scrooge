using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Scrooge.Model
{
    public class GroupedPurchaseAndSalesViewModel: INotifyPropertyChanged
    {
        private string groupName;
        public List<PurchaseAndSalesViewModel> PurchaseAndSales;
        public readonly EntryType Type;
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

        public string Sum => 0+""; //TODO (add all PurchaseAndSales together)

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode()==this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
