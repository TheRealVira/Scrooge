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
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory
    {
        public Inventory()
        {
            this.InitializeComponent();
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.InventoryGrid.Items.Add(new InventoryViewModel()
            { Access = 1f,AssetValue = 12,BalanceValue = 13f,CostValue = 15f,DateOfAcquisition = 1998,Derecognition = 0f,Duration = 10f,IsSelected = false,Months = 10,Name = "Computer",Percentage = 50});
            this.InventoryGrid.Items.Add(new InventoryViewModel()
            { Access = 1f, AssetValue = 12, BalanceValue = 13f, CostValue = 15f, DateOfAcquisition = 3000, Derecognition = 0f, Duration = 10f, IsSelected = false, Months = 10, Name = "Auto", Percentage = 50 });
            this.InventoryGrid.Items.Add(new InventoryViewModel()
            { Access = 1f, AssetValue = 12, BalanceValue = 13f, CostValue = 15f, DateOfAcquisition = 1900, Derecognition = 0f, Duration = 10f, IsSelected = false, Months = 10, Name = "Bildschirm", Percentage = 50 });
        }
    }
}
