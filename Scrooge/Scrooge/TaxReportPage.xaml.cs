using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Scrooge.Model;
using Scrooge.Service;
using Scrooge.Service.Definitions;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for TaxReportPage.xaml
    /// </summary>
    public partial class TaxReportPage
    {
        public TaxReportPage()
        {
            InitializeComponent();

            Changevis(Visibility.Hidden);
            this.SelectedYear.Value = DateTime.Now.Year;
        }

        private void Changevis(Visibility vis)
        {
            this.Equation.Visibility = vis;
            this.Grid.Visibility = vis;
            this.ExportBtn.Visibility = vis;
        }

        private void UstGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Changevis(this.SelectedYear.Value != null ? Visibility.Visible : Visibility.Hidden);
            if (this.Grid.Visibility == Visibility.Hidden) return;

            this.ReportData = new TaxReport(PurchaseAndSales.GroupedData, (int)this.SelectedYear.Value);

            this.UstGrid.ItemsSource = new ObservableCollection<GroupedPurchaseAndSalesViewModel>(this.ReportData.PurchasesAndSales.Where(x => x.Type == EntryType.Sale));
            this.VstGrid.ItemsSource = new ObservableCollection<GroupedPurchaseAndSalesViewModel>(this.ReportData.PurchasesAndSales.Where(x => x.Type == EntryType.Purchase));
            this.MyUStSum.Text = this.ReportData.PurchasesAndSales.Where(x => x.Type == EntryType.Sale)
                .Sum(x => x.Taxes)+"";
            this.MyVStSum.Text = this.ReportData.PurchasesAndSales.Where(x => x.Type == EntryType.Purchase)
                .Sum(x => x.Taxes) + "";
            this.Sales.Text = this.ReportData.Sales + "";
            this.NetSales.Text = this.ReportData.NetSales + "";
            this.MinusVorraus.Text = this.ReportData.AdvanceTaxPayements + "";
            this.MinusVSt.Text = this.MyVStSum.Text;
            this.Equals.Text = this.ReportData.OutstandingMoney + "";
        }

        private TaxReport ReportData;

        private void ExportBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (ReportData != null)
            {
                Singleton<ServiceController>.Instance.Get<IDataExportService>().ExportTaxReport(this.ReportData);
            }
        }
    }
}
