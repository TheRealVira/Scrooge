using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Documents;

namespace Scrooge.Model
{
    public class GroupedPurchaseAndSalesViewModel: INotifyPropertyChanged
    {
        private string groupName;
        public List<PurchaseAndSalesViewModel> PurchaseAndSales;

        [Key]
        public uint ID { get; set; }
        public GroupedPurchaseAndSalesViewModel()
        {
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
    }
}
