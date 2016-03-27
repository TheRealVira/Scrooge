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
        public AddInventoryItem(List<string> nameHistory, ActionwindowType type=ActionwindowType.Add)
        {
            InitializeComponent();

            this.MyName.ItemsSource = nameHistory;
            this.MyDate.Text = DateTime.Now.ToString();

            if (type ==ActionwindowType.Edit)
            {
                this.Title.Text = "Edit Inventory Item";
                this.AcceptBtn.Content = "Edit";
            }

            // TODO: Focuz MyName ftw
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            e.Handled = regex.IsMatch(((TextBox)sender).Text + e.Text);
        }

        public void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
        }

        public bool AllSet
            =>
                this.MyDate != null && this.MyAcquisitionValue.Text != string.Empty &&
                this.MyAssetValue.Text != string.Empty && this.MyDisposal.Text != string.Empty &&
                this.MyDuration.Text != string.Empty && this.MyName.Text != string.Empty;
    }
}
