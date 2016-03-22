using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Scrooge.Model;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for AddPurchaseOrSaleEntry.xaml
    /// </summary>
    public partial class AddPurchaseOrSaleEntry
    {
        public AddPurchaseOrSaleEntry(List<string> groupnames )
        {
            InitializeComponent();
            this.MyType.Items.Add("Sale");
            this.MyType.Items.Add("Purchase");

            this.MyGroupName.ItemsSource = groupnames;
            this.MyEntryDate.Text = DateTime.Now.ToString();
            this.MyREDate.Text = DateTime.Now.ToString();
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            e.Handled = regex.IsMatch(((TextBox)sender).Text + e.Text);
        }

        public void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("SAMPLE 1: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));

            //you can cancel the dialog close:
            //eventArgs.Cancel();

            if (!Equals(eventArgs.Parameter, true)) return;
            
            if (AllSet)
            {
                var temp = new PurchaseAndSalesViewModel(decimal.Parse(this.MySt.Text))
                {
                    EntryDate = (DateTime) this.MyEntryDate.SelectedDate,
                    Receipt = this.MyReceipt.Text,
                    REDate = (DateTime) this.MyREDate.SelectedDate,
                    Text = this.MyText.Text,
                    Value = decimal.Parse(this.MyValue.Text)
                };

                Output =
                    new GroupedPurchaseAndSalesViewModel(this.MyType.Text == "Sale"
                        ? PurchaseOrSale.Sale
                        : PurchaseOrSale.Purchase)
                    {
                        GroupName = this.MyGroupName.Text,
                        PurchaseAndSales = new List<PurchaseAndSalesViewModel>()
                        {
                            temp
                        }
                    };
            }
        }
        
        public GroupedPurchaseAndSalesViewModel Output;

        public bool AllSet
            =>
                this.MyEntryDate.SelectedDate != null && this.MyREDate.SelectedDate != null && this.MyType.Text != string.Empty &&
                this.MyGroupName.Text != string.Empty && this.MyReceipt.Text != string.Empty &&
                this.MySt.Text != string.Empty && this.MyValue.Text != string.Empty;
    }
}
