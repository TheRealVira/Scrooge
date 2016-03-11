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
            foreach (var retrieveInventoryViewModel in MainWindow.StorageService.RetrieveInventoryViewModels())
            {
                this.InventoryGrid.Items.Add(retrieveInventoryViewModel);
            }
        }

        private void InventoryGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
