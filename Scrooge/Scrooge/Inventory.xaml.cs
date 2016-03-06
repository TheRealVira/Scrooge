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
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory
    {
        public Inventory()
        {
            InitializeComponent();
        }

        private void AddEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void DeleteEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
