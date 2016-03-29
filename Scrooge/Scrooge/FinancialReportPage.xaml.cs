using System;
using System.Windows;
using System.Windows.Controls;
using Scrooge.Model;
using Scrooge.Service;
using Scrooge.Service.Definitions;

namespace Scrooge
{
    /// <summary>
    ///     Interaction logic for FinancialReportPage.xaml
    /// </summary>
    public partial class FinancialReportPage
    {
        private FinancialReport ReportData;

        public FinancialReportPage()
        {
            this.InitializeComponent();

            this.SelectedYear.Value = DateTime.Now.Year;

            ChangeVis(Visibility.Hidden);
        }

        private void ChangeVis(Visibility vis)
        {
            this.Grid.Visibility = vis;
            this.ExportBtn.Visibility = vis;
            this.Equation.Visibility = vis;
        }

        private void FinancialGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeVis(this.SelectedYear.Value != null ? Visibility.Visible : Visibility.Hidden);
            if (this.Grid.Visibility == Visibility.Hidden) return;

            this.ReportData = new FinancialReport(PurchaseAndSales.GroupedData, (int)this.SelectedYear.Value);

            this.FinancialGrid.ItemsSource = this.ReportData.PurchasesAndSales;
            this.Minus.Text = this.ReportData.Purchases+"";
            this.Plus.Text = this.ReportData.Sales + "";
            this.Equals.Text = this.ReportData.Result + "";
        }

        private void ExportBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.ReportData != null)
            {
                Singleton<ServiceController>.Instance.Get<IDataExportService>().ExportFinancialReport(this.ReportData);
            }
        }
    }
}