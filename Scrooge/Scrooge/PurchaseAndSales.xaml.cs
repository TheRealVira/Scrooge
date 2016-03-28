using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Scrooge.Model;
using Scrooge.Service;
using Scrooge.Service.Definitions;

namespace Scrooge
{
    /// <summary>
    ///     Interaction logic for PurchaseAndSales.xaml
    /// </summary>
    partial class PurchaseAndSales
    {
        private static ObservableCollection<GroupedSaleOrPurchase> _data;

        public static ObservableCollection<GroupedPurchaseAndSalesViewModel> GroupedData => new ObservableCollection<GroupedPurchaseAndSalesViewModel>(_data.Select(x=>x.Data));
        public PurchaseAndSales()
        {
            this.InitializeComponent();

            _data = new ObservableCollection<GroupedSaleOrPurchase>();

            this.GroupedItems.ItemsSource = _data;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_data.Count != 0) return;
            foreach (
                var viewModel in
                    MainWindow.StorageService.RetrieveGroupedPurchaseAndSalesViewModels())
            {
                _data.Add(new GroupedSaleOrPurchase(viewModel));
            }

            this.Plus.Text =
                _data.Where(x => x.Data.Type == EntryType.Sale).Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) +
                "";
            this.Minus.Text =
                _data.Where(x => x.Data.Type == EntryType.Purchase)
                    .Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) + "";

            this.UpdateSumAndTaxPayable();
        }

        private async void AddEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //#Model-View-Viewmodel xD
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddPurchaseOrSaleEntry(_data.Select(x => x.GroupeName.Text).ToList())
            {
                DataContext = new AddPurchaseOrSaleViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", view.DialogHost_OnDialogClosing);

            if (!(bool) result || !view.AllSet || view.Output == null) return;

            //view.Output.ID = _data.Count != 0 ? _data.Max(x => x.Data.PurchaseAndSales.Max(y=>y.ID)) + 1 : 0; // may come back to us one day, for now it also works without

            if (_data.Any(x => x.Data.GroupName == view.Output.GroupName && x.Data.Type == view.Output.Type))
            {
                var current =
                    _data.First(x => x.GroupeName.Text == view.Output.GroupName && x.Data.Type == view.Output.Type);
                current.Data.PurchaseAndSales.Add(view.Output.PurchaseAndSales[0]);
                current.MySum.Text = current.Data.PurchaseAndSales.Sum(x => x.Value) + "";
                this.UpdateCalculations(current.Data.Type);
            }
            else
            {
                _data.Add(new GroupedSaleOrPurchase(view.Output));
                this.UpdateCalculations(view.Output.Type);
            }
        }

        private void UpdateCalculations(EntryType whichSideToUpdate)
        {
            if (whichSideToUpdate == EntryType.Sale)
            {
                this.Plus.Text =
                    _data.Where(x => x.Data.Type == EntryType.Sale)
                        .Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) + "";
            }
            else
            {
                this.Minus.Text =
                    _data.Where(x => x.Data.Type == EntryType.Purchase)
                        .Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) + "";
            }

            this.UpdateSumAndTaxPayable();
        }

        private void DeleteEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            for (var i = _data.Count - 1; i > -1; i--)
            {
                var isChecked = _data[i].ImSelected.IsChecked;
                if (isChecked != null && (bool) isChecked)
                {
                    _data.Remove(_data[i]);
                    continue;
                }

                for (var i2 = _data[i].Data.PurchaseAndSales.Count - 1; i2 > -1; i2--)
                {
                    if (_data[i].Data.PurchaseAndSales[i2].IsSelected)
                    {
                        _data[i].Data.PurchaseAndSales.Remove(_data[i].Data.PurchaseAndSales[i2]);
                    }
                }
            }

            this.Plus.Text =
                _data.Where(x => x.Data.Type == EntryType.Sale).Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) +
                "";
            this.Minus.Text =
                _data.Where(x => x.Data.Type == EntryType.Purchase)
                    .Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) + "";
            this.UpdateSumAndTaxPayable();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var items = new GroupedPurchaseAndSalesViewModel[this.GroupedItems.Items.Count];
            this.GroupedItems.Items.OfType<GroupedSaleOrPurchase>().Select(x => x.Data).ToArray().CopyTo(items, 0);
            MainWindow.StorageService.UpdateGroupedPurchaseAndSales(new List<GroupedPurchaseAndSalesViewModel>(items));
        }

        private void UpdateSumAndTaxPayable()
        {
            this.Summ.Text = (decimal.Parse(this.Plus.Text) - decimal.Parse(this.Minus.Text)) + "";
            this.TaxPayable.Text =
                Singleton<ServiceController>.Instance.Get<ICalculationService>()
                    .CalculateTaxPayable(_data.Select(x => x.Data), DateTime.Now.Year)
                    .ToString();
        }

        private void InventoryGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }
    }
}