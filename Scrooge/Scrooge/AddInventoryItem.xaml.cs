using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Scrooge.Model;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for AddInventoryItem.xaml
    /// </summary>
    public partial class AddInventoryItem
    {
        public AddInventoryItem(List<string> nameHistory)
        {
            InitializeComponent();

            this.MyName.ItemsSource = nameHistory;
            this.MyDate.Text = DateTime.Now.ToString();

            // TODO: Focuz MyName ftw
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

            var selectedDate = this.MyDate.SelectedDate;
            if (selectedDate != null)
            {
                Output = new InventoryViewModel()
                {
                    AssetValue = decimal.Parse(this.MyAssetValue.Text),
                    AcquisitionValue = decimal.Parse(this.MyAcquisitionValue.Text),
                    DateOfAcquisition = (DateTime)selectedDate,
                    Disposal = decimal.Parse(this.MyDisposal.Text),
                    Name = this.MyName.Text,
                    Duration = decimal.Parse(this.MyDuration.Text)
                };
            }
        }

        public InventoryViewModel Output;

        public bool AllSet
            =>
                this.MyDate != null && this.MyAcquisitionValue.Text != string.Empty &&
                this.MyAssetValue.Text != string.Empty && this.MyDisposal.Text != string.Empty &&
                this.MyDuration.Text != string.Empty && this.MyName.Text != string.Empty;
    }
}
