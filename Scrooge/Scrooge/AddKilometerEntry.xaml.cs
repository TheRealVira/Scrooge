using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Scrooge.Model;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for AddKilometerEntry.xaml
    /// </summary>
    public partial class AddKilometerEntry 
    {
        public AddKilometerEntry()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        public void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("SAMPLE 1: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));

            //you can cancel the dialog close:
            //eventArgs.Cancel();

            if (!Equals(eventArgs.Parameter, true)) return;
            
            var selectedDate = this.MyDate.SelectedDate;
            if (selectedDate != null)
            {
                Output = new KilometerEntryViewModel()
                {
                    Date = (DateTime) selectedDate,
                    DrivenRoute = this.MyDrivenRoute.Text,
                    StartedKilometerCount = decimal.Parse(this.MyStartedKilometerCount.Text),
                    NewKilometerCount = decimal.Parse(this.MyNewKilometerCount.Text),
                    Purpose = this.MyPurpose.Text
                };
            }
        }

        public KilometerEntryViewModel Output;

        public bool AllSet
            =>
                this.MyDate != null && this.MyDrivenRoute.Text != string.Empty &&
                this.MyNewKilometerCount.Text != string.Empty && this.MyPurpose.Text != string.Empty &&
                this.MyStartedKilometerCount.Text != string.Empty;
    }
}
