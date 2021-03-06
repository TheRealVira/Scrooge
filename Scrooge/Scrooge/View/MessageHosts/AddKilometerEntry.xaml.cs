﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Scrooge.Model;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for AddKilometerEntry.xaml
    /// </summary>
    public partial class AddKilometerEntry
    {
        public AddKilometerEntry(List<string> drivenHistory, List<string> purposeHistory)
        {
            InitializeComponent();

            this.MyPurpose.ItemsSource = purposeHistory;
            this.MyDrivenRoute.ItemsSource = drivenHistory;
            this.MyDate.Text = DateTime.Now.ToString();

            // TODO: Focuz MyDrivenRoute ftw
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            e.Handled = regex.IsMatch(((TextBox) sender).Text + e.Text);
        }

        public void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
        }

        public bool AllSet
            =>
                this.MyDate != null && this.MyDrivenRoute.Text != string.Empty &&
                this.MyNewKilometerCount.Text != string.Empty && this.MyPurpose.Text != string.Empty &&
                this.MyStartedKilometerCount.Text != string.Empty;
    }
}