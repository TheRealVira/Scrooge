using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Scrooge.Model;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for GroupedSaleOrPurchase.xaml
    /// </summary>
    public partial class GroupedSaleOrPurchase : UserControl
    {
        public GroupedSaleOrPurchase(GroupedPurchaseAndSalesViewModel saleOrPurchase)
        {
            InitializeComponent();

            this.GroupeName.Text = saleOrPurchase.GroupName;

            this.Data = saleOrPurchase.PurchaseAndSales == null
                ? new ObservableCollection<PurchaseAndSalesViewModel>()
                : new ObservableCollection<PurchaseAndSalesViewModel>(saleOrPurchase.PurchaseAndSales);

            this.SaleOrPurchaseGrid.ItemsSource = Data;
            this.GroupeName.Text = saleOrPurchase.GroupName;
            this.MySum.Text = Data.Sum(x => x.Value) + "";

            this.ImSelected.DataContext = saleOrPurchase.IsSelected;
            this.Type = saleOrPurchase.Type;
            this.SomeCallMeType.Text = this.Type.ToString();
        }

        public ObservableCollection<PurchaseAndSalesViewModel> Data;
        public readonly EntryType Type;
    }
}
