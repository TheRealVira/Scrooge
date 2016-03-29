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
    ///     Interaction logic for InventoryReportPage.xaml
    /// </summary>
    public partial class InventoryReportPage
    {
        private ObservableCollection<InventoryViewModel> ReportData;

        public InventoryReportPage()
        {
            this.InitializeComponent();
            this.ChangeVis(Visibility.Hidden);
            this.SelectedYear.Value = DateTime.Now.Year;
        }

        private void ChangeVis(Visibility vis)
        {
            this.GridZone.Visibility = vis;
            this.ExportBtn.Visibility = vis;
            this.AcquisitionValueZone.Visibility = vis;
            this.DeprecationZone.Visibility = vis;
            this.BalanceValueZone.Visibility = vis;
        }

        private void InventoryGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void ExportBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Singleton<ServiceController>.Instance.Get<IDataExportService>()
                .ExportInventoryReport(this.ReportData, (int) this.SelectedYear.Value);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.ChangeVis(this.SelectedYear.Value != null ? Visibility.Visible : Visibility.Hidden);
            if (this.InventoryGrid.Visibility == Visibility.Hidden) return;

            this.ReportData =
                new ObservableCollection<InventoryViewModel>(
                    Singleton<ServiceController>.Instance.Get<ICalculationService>()
                        .GetChangedInventoryItems(Inventory._data, (int) this.SelectedYear.Value));

            this.InventoryGrid.ItemsSource = this.ReportData;
            this.BalanceValue.Text = Math.Round(this.ReportData.Sum(x => x.BalanceValue),2)+"";
            this.AcquisitionValue.Text = Math.Round(this.ReportData.Sum(x => x.AcquisitionValue), 2) + "";
            this.Deprecation.Text = Math.Round(this.ReportData.Sum(x => x.Deprecation),2)+"";
        }
    }
}
