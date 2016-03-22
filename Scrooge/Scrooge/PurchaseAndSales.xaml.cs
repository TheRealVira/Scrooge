﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Scrooge.Model;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for PurchaseAndSales.xaml
    /// </summary>
    partial class PurchaseAndSales
    {
        public PurchaseAndSales()
        {
            InitializeComponent();

            this._data =new ObservableCollection<GroupedSaleOrPurchase>();

            this.GroupedItems.ItemsSource = this._data;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (
                GroupedPurchaseAndSalesViewModel viewModel in
                    MainWindow.StorageService.RetrieveGroupedPurchaseAndSalesViewModels())
            {
                this._data.Add(new GroupedSaleOrPurchase(viewModel));
            }

            //////DEBUG
            for (int i = 0; i < 5; i++)
            {
                this._data.Add(new GroupedSaleOrPurchase(new GroupedPurchaseAndSalesViewModel(PurchaseOrSale.Sale)
                {
                    PurchaseAndSales = new List<PurchaseAndSalesViewModel>()
                    {
                        new PurchaseAndSalesViewModel(10),
                        new PurchaseAndSalesViewModel(20),
                        new PurchaseAndSalesViewModel(10),
                        new PurchaseAndSalesViewModel(20),
                        new PurchaseAndSalesViewModel(10),
                    },
                    GroupName = "Test"
                }));
            }
            //////DEBUG

            this.Plus.Text = this._data.Where(x => x.Type == PurchaseOrSale.Sale).Sum(x => x.Data.Sum(y => y.Value)) +
                             "";
            this.Minus.Text =
                this._data.Where(x => x.Type == PurchaseOrSale.Purchase).Sum(x => x.Data.Sum(y => y.Value)) + "";
            this.Summ.Text = (decimal.Parse(this.Plus.Text) - decimal.Parse(this.Minus.Text)) + "";
        }

        private async void AddEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //#Model-View-Viewmodel xD
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddPurchaseOrSaleEntry(this._data.Select(x => x.GroupeName.Text).ToList())
            {
                DataContext = new AddPurchaseOrSaleViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", view.DialogHost_OnDialogClosing);

            if (!(bool)result || !view.AllSet || view.Output == null) return;

            view.Output.ID = _data.Count != 0 ? _data.Max(x => x.Data.Max(y=>y.ID)) + 1 : 0;

            if (_data.Any(x => x.GroupeName.Text == view.Output.GroupName && x.Type == view.Output.Type))
            {
                var current=_data.First(x => x.GroupeName.Text==view.Output.GroupName && x.Type == view.Output.Type);
                current.Data.Add(view.Output.PurchaseAndSales[0]);
                current.MySum.Text = current.Data.Sum(x => x.Value)+"";
                UpdateCalculations(current.Type);
            }
            else
            {
                _data.Add(new GroupedSaleOrPurchase(view.Output));
                UpdateCalculations(view.Output.Type);
            }
        }

        private void UpdateCalculations(PurchaseOrSale whichSideToUpdate)
        {
            if (whichSideToUpdate == PurchaseOrSale.Sale)
            {
                this.Plus.Text = this._data.Where(x => x.Type == PurchaseOrSale.Sale).Sum(x => x.Data.Sum(y => y.Value)) + "";
                this.Summ.Text = (decimal.Parse(this.Plus.Text) - decimal.Parse(this.Minus.Text)) + "";
            }
            else
            {
                this.Minus.Text = this._data.Where(x => x.Type == PurchaseOrSale.Purchase).Sum(x => x.Data.Sum(y => y.Value)) + "";
                this.Summ.Text = (decimal.Parse(this.Plus.Text) - decimal.Parse(this.Minus.Text)) + "";
            }
        }

        private void DeleteEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = this._data.Count-1; i > -1; i--)
            {
                var isChecked = ((GroupedSaleOrPurchase) this._data[i]).ImSelected.IsChecked;
                if (isChecked != null && (bool)isChecked)
                {
                    this._data.Remove(this._data[i]);
                    continue;
                }

                for (int i2 = this._data[i].Data.Count - 1; i2 > -1; i2--)
                {
                    if (this._data[i].Data[i2].IsSelected)
                    {
                        this._data[i].Data.Remove(this._data[i].Data[i2]);
                    }
                }
            }

            this.Plus.Text = this._data.Where(x => x.Type == PurchaseOrSale.Sale).Sum(x => x.Data.Sum(y => y.Value)) +
                             "";
            this.Minus.Text =
                this._data.Where(x => x.Type == PurchaseOrSale.Purchase).Sum(x => x.Data.Sum(y => y.Value)) + "";
            this.Summ.Text = (decimal.Parse(this.Plus.Text) - decimal.Parse(this.Minus.Text)) + "";
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void InventoryGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private ObservableCollection<GroupedSaleOrPurchase> _data;
    }
}
