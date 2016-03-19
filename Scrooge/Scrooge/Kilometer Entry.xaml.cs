using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Scrooge.Model;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for Kilometer_Entry.xaml
    /// </summary>
    public partial class KilometerEntry
    {
        public KilometerEntry()
        {
            InitializeComponent();

            // Well doesn't work like I would like it to...
            Data = new ObservableCollection<KilometerEntryViewModel>();
            this.KilometerGrid.ItemsSource = this.Data;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            long sum = 0;
            //foreach (var retrieveInventoryViewModel in MainWindow.StorageService.RetrieveInventoryViewModels())
            //{
            //    var retrieved = retrieveInventoryViewModel;
            //    sum+=retrieved.DrivenKilometers;
            //    this.InventoryGrid.Items.Add(retrieved);
            //}

            this.SumOfKilometers.Text = sum.ToString();
        }

        private async void AddEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ////Some kind of copie paste:
            
            //#Model-View-Viewmodel xD
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddKilometerEntry()
            {
                DataContext = new KilometerEntryViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog",view.DialogHost_OnDialogClosing);

            if ((bool) result&&view.AllSet)
            {
                //this.KilometerGrid.Items.Add(view.Output);
                Data.Add(view.Output);
            }
        }

        private void DeleteEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = Data.Count - 1; i > -1; i--)
            {
                if (Data[i].IsSelected)
                {
                    Data.Remove(Data[i]);
                }
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            KilometerEntryViewModel[]items=new KilometerEntryViewModel[KilometerGrid.Items.Count];
            KilometerGrid.Items.CopyTo(items,0);
            MainWindow.StorageService.UpdateKilometerEntry(new List<KilometerEntryViewModel>(items));
        }

        private void InventoryGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //e.Cancel=true;
        }

        private ObservableCollection<KilometerEntryViewModel> Data;
    }
}
