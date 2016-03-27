using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Scrooge.Model;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory
    {
        public Inventory()
        {
            this.InitializeComponent();

            _data = new ObservableCollection<InventoryViewModel>();
            _nameHistory =new List<string>();
            this.InventoryGrid.ItemsSource = this._data;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
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
                DataContext = new InventoryViewModel()
                {
                    Duration = 1
                }
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", view.DialogHost_OnDialogClosing);

            if (!(bool)result || !view.AllSet || view.Output == null) return;

            view.Output.ID = _data.Count != 0 ? _data.Max(x => x.ID) + 1 : 0;
            _data.Add(view.Output);
            if (!this._nameHistory.Contains(view.Output.Name))
            {
                this._nameHistory.Add(view.Output.Name);
            }
        }

        private void DeleteEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = _data.Count - 1; i > -1; i--)
            {
                if (!_data[i].IsSelected) continue;

                this._nameHistory.Remove(_data[i].Name);
                _data.Remove(_data[i]);
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            InventoryViewModel[] items = new InventoryViewModel[InventoryGrid.Items.Count];
            InventoryGrid.Items.CopyTo(items, 0);
            MainWindow.StorageService.UpdateInventory(new List<InventoryViewModel>(items));
        }

        private void InventoryGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private readonly ObservableCollection<InventoryViewModel> _data;
        private readonly List<string> _nameHistory;
    }
}
