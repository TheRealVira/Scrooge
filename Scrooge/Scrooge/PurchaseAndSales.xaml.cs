using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Scrooge.Model;

namespace Scrooge
{
    using Scrooge.Service;
    using Scrooge.Service.Definitions;
    using Scrooge.Service.Implementations;

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
            if (this._data.Count != 0) return;
            foreach (
                GroupedPurchaseAndSalesViewModel viewModel in
                    MainWindow.StorageService.RetrieveGroupedPurchaseAndSalesViewModels())
            {
                this._data.Add(new GroupedSaleOrPurchase(viewModel));
            }

            this.Plus.Text = this._data.Where(x => x.Data.Type == EntryType.Sale).Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) +
                             "";
            this.Minus.Text =
                this._data.Where(x => x.Data.Type == EntryType.Purchase).Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) + "";

            this.UpdateSumAndTaxPayable();
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

            //view.Output.ID = _data.Count != 0 ? _data.Max(x => x.Data.PurchaseAndSales.Max(y=>y.ID)) + 1 : 0; // may come back to us one day, for now it also works without

            if (_data.Any(x => x.Data.GroupName == view.Output.GroupName && x.Data.Type == view.Output.Type))
            {
                var current=_data.First(x => x.GroupeName.Text==view.Output.GroupName && x.Data.Type == view.Output.Type);
                current.Data.PurchaseAndSales.Add(view.Output.PurchaseAndSales[0]);
                current.MySum.Text = current.Data.PurchaseAndSales.Sum(x => x.Value)+"";
                UpdateCalculations(current.Data.Type);
            }
            else
            {
                _data.Add(new GroupedSaleOrPurchase(view.Output));
                UpdateCalculations(view.Output.Type);
            }
        }

        private void UpdateCalculations(EntryType whichSideToUpdate)
        {
            if (whichSideToUpdate == EntryType.Sale)
            {
                this.Plus.Text = this._data.Where(x => x.Data.Type == EntryType.Sale).Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) + "";
            }
            else
            {
                this.Minus.Text = this._data.Where(x => x.Data.Type == EntryType.Purchase).Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) + "";
            }

            this.UpdateSumAndTaxPayable();
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

                for (int i2 = this._data[i].Data.PurchaseAndSales.Count - 1; i2 > -1; i2--)
                {
                    if (this._data[i].Data.PurchaseAndSales[i2].IsSelected)
                    {
                        this._data[i].Data.PurchaseAndSales.Remove(this._data[i].Data.PurchaseAndSales[i2]);
                    }
                }
            }

            this.Plus.Text = this._data.Where(x => x.Data.Type == EntryType.Sale).Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) +
                             "";
            this.Minus.Text =
                this._data.Where(x => x.Data.Type == EntryType.Purchase).Sum(x => x.Data.PurchaseAndSales.Sum(y => y.Value)) + "";
            this.UpdateSumAndTaxPayable();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            GroupedPurchaseAndSalesViewModel[] items = new GroupedPurchaseAndSalesViewModel[GroupedItems.Items.Count];
            this.GroupedItems.Items.OfType<GroupedSaleOrPurchase>().Select(x=>x.Data).ToArray().CopyTo(items, 0);
            MainWindow.StorageService.UpdateGroupedPurchaseAndSales(new List<GroupedPurchaseAndSalesViewModel>(items));
        }

        private void UpdateSumAndTaxPayable()
        {
            this.Summ.Text = (decimal.Parse(this.Plus.Text) - decimal.Parse(this.Minus.Text)) + "";
            this.TaxPayable.Text = Singleton<ServiceController>.Instance.Get<ICalculationService>().CalculateTaxPayable(/* ToDo: pass the grouped purchases and sales view models */, DateTime.Now.Year);
        }

        private void InventoryGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private readonly ObservableCollection<GroupedSaleOrPurchase> _data;
    }
}
