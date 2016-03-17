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

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for Kilometer_Entry.xaml
    /// </summary>
    public partial class KilometerEntry
    {
        public KilometerEntry()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            long sum = 0;
            //foreach (var retrieveInventoryViewModel in MainWindow.StorageService.RetrieveInventoryViewModels())
            //{
            //    var retrieved = retrieveInventoryViewModel;
            //    sum+=retrieved.DrivenKilometers;
            //    this.InventoryGrid.Items.Add(retrieved);
            //}

            this.SumOfKilometers.Text = sum.ToString();
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
            e.Cancel=true;
        }
    }
}
