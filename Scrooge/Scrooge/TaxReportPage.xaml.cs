using System.Windows;
using System.Windows.Controls;

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
        }

        private void Changevis(Visibility vis)
        {
            this.Equation.Visibility = vis;
            this.Grid.Visibility = vis;
        }

        private void UstGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void ExportBtn_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Export
        }
    }
}
