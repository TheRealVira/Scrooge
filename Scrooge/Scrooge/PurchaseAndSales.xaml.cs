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
    /// Interaction logic for PurchaseAndSales.xaml
    /// </summary>
    partial class PurchaseAndSales
    {
        public PurchaseAndSales()
        {
            InitializeComponent();

            this.Summ.Text = 1000000.ToString();
            this.Minus.Text = 1000000.ToString();
            this.Plus.Text = 1000000.ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (GroupedPurchaseAndSalesViewModel viewModel in MainWindow.StorageService.RetrieveGroupedPurchaseAndSalesViewModels())
            {
                this.GroupedItems.Items.Add(new GroupedSaleOrPurchase(viewModel));
            }

            //////DEBUG
            for (int i = 0; i < 5; i++)
            {
                this.GroupedItems.Items.Add(new GroupedSaleOrPurchase(new GroupedPurchaseAndSalesViewModel()
                {
                    PurchaseAndSales = new List<PurchaseAndSalesViewModel>()
            {
                new PurchaseAndSalesViewModel(),
                new PurchaseAndSalesViewModel(),
                new PurchaseAndSalesViewModel(),
                new PurchaseAndSalesViewModel(),
                new PurchaseAndSalesViewModel(),
            },
                    GroupName = "Test"
                }));
            }
            //////DEBUG
        }

        private void AddEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void InventoryGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
