using System;
using System.Collections.Generic;
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

            if (saleOrPurchase.PurchaseAndSales != null)
            {
                foreach (var purchaseAndSalese in saleOrPurchase.PurchaseAndSales)
                {
                    this.SaleOrPurchaseGrid.Items.Add(purchaseAndSalese);
                }
            }

            this.MySum.DataContext = saleOrPurchase;
        }

        private void Grid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
