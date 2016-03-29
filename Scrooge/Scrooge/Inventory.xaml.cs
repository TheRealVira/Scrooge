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
    ///     Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory
    {
        public static ObservableCollection<InventoryViewModel> _data;
        private readonly List<string> _nameHistory;

        public Inventory()
        {
            this.InitializeComponent();

            _data = new ObservableCollection<InventoryViewModel>();
            this._nameHistory = new List<string>();
            this.InventoryGrid.ItemsSource = _data;

            Singleton<ServiceController>.Instance.Get<IApplicationEventService>()
                .RegisterApplicationCloseRequestHandler(this.CloseRequestHandler);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_data.Count != 0) return;

            foreach (var retrieveInventoryViewModel in MainWindow.StorageService.RetrieveInventoryViewModels())
            {
                _data.Add(retrieveInventoryViewModel);
                if (!this._nameHistory.Contains(retrieveInventoryViewModel.Name))
                {
                    this._nameHistory.Add(retrieveInventoryViewModel.Name);
                }
            }
        }

        private async void AddEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //#Model-View-Viewmodel xD
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddInventoryItem(this._nameHistory)
            {
                DataContext = new InventoryViewModel
                {
                    Duration = 1
                }
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", view.DialogHost_OnDialogClosing);
            var outp = (InventoryViewModel) view.DataContext;
            if (!(bool) result || !view.AllSet || view.DataContext == null) return;
            outp.DateOfAcquisition = view.MyDate.SelectedDate.Value;

            //outp.ID = _data.Count != 0 ? _data.Max(x => x.ID) + 1 : 0;
            _data.Add(outp);
            if (!this._nameHistory.Contains(outp.Name))
            {
                this._nameHistory.Add(outp.Name);
            }
        }

        private void DeleteEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            for (var i = _data.Count - 1; i > -1; i--)
            {
                if (!_data[i].IsSelected) continue;

                this._nameHistory.Remove(_data[i].Name);
                _data.Remove(_data[i]);
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var items = new InventoryViewModel[this.InventoryGrid.Items.Count];
            this.InventoryGrid.Items.CopyTo(items, 0);
            MainWindow.StorageService.UpdateInventory(new List<InventoryViewModel>(items));
        }

        private void InventoryGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this._nameHistory.Remove(_data[this.InventoryGrid.SelectedIndex].Name);
            //#Model-View-Viewmodel xD
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddInventoryItem(this._nameHistory, ActionwindowType.Edit)
            {
                DataContext = _data[this.InventoryGrid.SelectedIndex]
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", view.DialogHost_OnDialogClosing);
            var outp = (InventoryViewModel) view.DataContext;
            if (!(bool) result || !view.AllSet || outp == null) return;

            _data[this.InventoryGrid.SelectedIndex] = outp;
            this._nameHistory.Add(outp.Name);
        }

        private async void AppreciateBtn_Click(object sender, RoutedEventArgs e)
        {
            this._nameHistory.Remove(_data[this.InventoryGrid.SelectedIndex].Name);
            //#Model-View-Viewmodel xD
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AppreciateAppreciation(_data[this.InventoryGrid.SelectedIndex])
            {
                DataContext = new Appreciation()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", view.DialogHost_OnDialogClosing);
            var outp = (Appreciation) view.DataContext;
            outp.DateTime = view.MySelectedDate;
            if (!(bool) result || !view.AllSet || outp == null) return;

            _data[this.InventoryGrid.SelectedIndex].AppreciationList.Add(outp);
        }

        private async Task<bool> CloseRequestHandler()
        {
            var unsaved =
                !_data.SequenceEqual(MainWindow.StorageService.RetrieveInventoryViewModels());

            if (!unsaved) return true;

            YesNoMessageBox dialog = null;
            await this.Dispatcher.Invoke(async () =>
            {
                dialog =
                    new YesNoMessageBox(
                        "You have unsaved data in the Inventory tab. Do you want to proceed with exiting and ignore your changes?");
                await DialogHost.Show(dialog, "RootDialog");
            });

            return dialog?.ResultIsYes.GetValueOrDefault(false) ?? false;
        }
    }
}