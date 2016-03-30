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
        public AddInventoryItem(List<string> nameHistory, ActionwindowType type = ActionwindowType.Add)
        {
            InitializeComponent();

            this.MyName.ItemsSource = nameHistory;
            this.MyDate.Text = DateTime.Now.ToString();

            if (type == ActionwindowType.Edit)
            {
                this.Title.Text = "Edit Inventory Item";
                this.AcceptBtn.Content = "Edit";
            }

            // TODO: Focuz MyName ftw
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            decimal test = -1;
            string t = ((TextBox) sender).Text + e.Text + (e.Text == "." ? "0" : "");
            e.Handled = !(decimal.TryParse(t, out test) && test > -1);
        }

        public void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
        }

        public bool AllSet
            =>
                this.MyDate != null && this.MyAcquisitionValue.Text != string.Empty &&
                this.MyDuration.Text != string.Empty && this.MyName.Text != string.Empty;
    }
}