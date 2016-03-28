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
using MaterialDesignThemes.Wpf;
using Scrooge.Model;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for AppreciateAppreciation.xaml
    /// </summary>
    public partial class AppreciateAppreciation
    {
        public AppreciateAppreciation(InventoryViewModel model)
        {
            InitializeComponent();
            this.PastAppreciations.ItemsSource = model.AppreciationList;
            this.MyDate.SelectedDate=DateTime.Now;
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            decimal test = -1;
            string t = ((ComboBox)sender).Text + e.Text + (e.Text == "." ? "0" : "");
            e.Handled = !(decimal.TryParse(t, out test) && test > -1);
        }

        public void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (MyDate.SelectedDate != null) this.MySelectedDate = (DateTime) MyDate.SelectedDate;
        }

        public DateTime MySelectedDate;
        public bool AllSet
            =>
                this.MyDate != null && this.PastAppreciations.Text != string.Empty;

        private void PastAppreciations_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((ComboBox)sender).SelectedItem==null)return;

            this.MyDate.SelectedDate = ((Appreciation) ((ComboBox) sender).SelectedItem).DateTime;
        }
    }
}
