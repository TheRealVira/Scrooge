using System.Windows;
using System.Windows.Controls;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for FinancialReportPage.xaml
    /// </summary>
    public partial class FinancialReportPage
    {
        public FinancialReportPage()
        {
            InitializeComponent();

            this.Grid.Visibility = Visibility.Hidden;
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
            this.Grid.Visibility = this.Date.SelectedDate!=null?Visibility.Visible : Visibility.Hidden;
        }
    }
}
