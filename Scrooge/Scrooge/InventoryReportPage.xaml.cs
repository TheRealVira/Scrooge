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
    /// Interaction logic for InventoryReportPage.xaml
    /// </summary>
    public partial class InventoryReportPage
    {
        public InventoryReportPage()
        {
            InitializeComponent();
            ChangeVis(Visibility.Hidden);
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
            // TODO: Export
        }

        private ObservableCollection<InventoryViewModel> ReportData; 

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeVis(this.SelectedYear.Value != null ? Visibility.Visible : Visibility.Hidden);
            if (this.InventoryGrid.Visibility == Visibility.Hidden) return;

            ReportData=new ObservableCollection<InventoryViewModel>(Singleton<ServiceController>.Instance.Get<ICalculationService>().GetChangedInventoryItems(Inventory._data, (int)this.SelectedYear.Value));

            this.InventoryGrid.ItemsSource = this.ReportData;
            this.BalanceValue.Text = this.ReportData.Sum(x => x.BalanceValue)+"";
            this.AcquisitionValue.Text = this.ReportData.Sum(x => x.AcquisitionValue) + "";
            this.Deprecation.Text = this.ReportData.Sum(x => x.Deprecation)+"";
        }
    }
}
