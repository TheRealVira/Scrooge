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

            this.Grid.Visibility = Visibility.Hidden;
            this.ExportBtn.Visibility = Visibility.Hidden;
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
            this.Grid.Visibility = this.Date.SelectedDate != null ? Visibility.Visible : Visibility.Hidden;
            this.ExportBtn.Visibility = this.Grid.Visibility;
            if (this.Grid.Visibility == Visibility.Hidden) return;

            this.ReportData = new FinancialReport(PurchaseAndSales.GroupedData, this.Date.SelectedDate.Value.Year);

            this.FinancialGrid.ItemsSource = this.ReportData.PurchasesAndSales;
        }

        private void ExportBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Singleton<ServiceController>.Instance.Get<IDataExportService>().ExportFinancialReport(this.ReportData);
        }
    }
}