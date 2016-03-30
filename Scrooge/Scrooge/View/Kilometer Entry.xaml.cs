using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Scrooge.Model;
using Scrooge.Service;
using Scrooge.Service.Definitions;

namespace Scrooge
{
    /// <summary>
    ///     Interaction logic for Kilometer_Entry.xaml
    /// </summary>
    public partial class KilometerEntry
    {
        private readonly ObservableCollection<KilometerEntryViewModel> _data;
        private readonly List<string> _purposeHistory, _drivenRouteHistory;

        public KilometerEntry()
        {
            this.InitializeComponent();

            this._data = new ObservableCollection<KilometerEntryViewModel>();
            this._purposeHistory = new List<string>();
            this._drivenRouteHistory = new List<string>();

            this.KilometerGrid.ItemsSource = this._data;
            this.SumOfKilometers.Text = "0";

            Singleton<ServiceController>.Instance.Get<IApplicationEventService>()
                .RegisterApplicationCloseRequestHandler(this.CloseRequestHandler);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this._data.Count != 0) return;
            long sum = 0;

            foreach (var retrieveInventoryViewModel in MainWindow.StorageService.RetrieveKilometerEntryViewModels())
            {
                var retrieved = retrieveInventoryViewModel;
                sum += (long) retrieved.DrivenKilometers;
                if (!this._drivenRouteHistory.Contains(retrieved.DrivenRoute))
                {
                    this._drivenRouteHistory.Add(retrieved.DrivenRoute);
                }
                if (!this._purposeHistory.Contains(retrieved.Purpose))
                {
                    this._purposeHistory.Add(retrieved.Purpose);
                }

                this.SumOfKilometers.Text =
                    (int.Parse(this.SumOfKilometers.Text) + retrieved.DrivenKilometers).ToString();
                this._data.Add(retrieved);
            }

            this.SumOfKilometers.Text = sum.ToString();
        }

        private async void AddEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //#Model-View-Viewmodel xD
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddKilometerEntry(this._drivenRouteHistory, this._purposeHistory)
            {
                DataContext = new KilometerEntryViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", view.DialogHost_OnDialogClosing);

            var outp = (KilometerEntryViewModel) view.DataContext;
            if (!(bool) result || !view.AllSet || outp == null) return;

            //outp.ID = _data.Count!=0?_data.Max(x => x.ID)+1:0;
            this._data.Add(outp);
            if (!this._drivenRouteHistory.Contains(outp.DrivenRoute))
            {
                this._drivenRouteHistory.Add(outp.DrivenRoute);
            }
            if (!this._purposeHistory.Contains(outp.Purpose))
            {
                this._purposeHistory.Add(outp.Purpose);
            }

            this.SumOfKilometers.Text = (int.Parse(this.SumOfKilometers.Text) + outp.DrivenKilometers).ToString();
        }

        private void DeleteEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            for (var i = this._data.Count - 1; i > -1; i--)
            {
                if (!this._data[i].IsSelected) continue;

                this._drivenRouteHistory.Remove(this._data[i].DrivenRoute);
                this._purposeHistory.Remove(this._data[i].Purpose);
                this.SumOfKilometers.Text =
                    (int.Parse(this.SumOfKilometers.Text) - this._data[i].DrivenKilometers).ToString();
                this._data.Remove(this._data[i]);
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var items = new KilometerEntryViewModel[this.KilometerGrid.Items.Count];
            this.KilometerGrid.Items.CopyTo(items, 0);
            MainWindow.StorageService.UpdateKilometerEntry(new List<KilometerEntryViewModel>(items));
        }

        private void KilometerGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private async Task<bool> CloseRequestHandler()
        {
            var unsaved =
                !this._data.SequenceEqual(MainWindow.StorageService.RetrieveKilometerEntryViewModels());

            if (!unsaved) return true;

            YesNoMessageBox dialog = null;
            await this.Dispatcher.Invoke(async () =>
            {
                dialog =
                    new YesNoMessageBox(
                        "You have unsaved data in the Driver's log tab. Do you want to proceed with exiting and ignore your changes?");
                await DialogHost.Show(dialog, "RootDialog");
            });

            return dialog?.ResultIsYes.GetValueOrDefault(false) ?? false;
        }
    }
}