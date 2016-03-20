﻿using System;
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
    /// Interaction logic for Kilometer_Entry.xaml
    /// </summary>
    public partial class KilometerEntry
    {
        public KilometerEntry()
        {
            InitializeComponent();
            
            _data = new ObservableCollection<KilometerEntryViewModel>();
            _purposeHistory=new List<string>();
            _drivenRouteHistory = new List<string>();

            this.KilometerGrid.ItemsSource = this._data;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            long sum = 0;
            // If implemented then just uncomment this section:
            //foreach (var retrieveInventoryViewModel in MainWindow.StorageService.RetrieveKilometerEntryViewModels())
            //{
            //    var retrieved = retrieveInventoryViewModel;
            //    sum += (long)retrieved.DrivenKilometers;
            //    if (!this._drivenRouteHistory.Contains(retrieved.DrivenRoute))
            //    {
            //       this._drivenRouteHistory.Add(retrieved.DrivenRoute);
            //    }
            //    if (!this._purposeHistory.Contains(retrieved.Purpose))
            //    {
            //        this._purposeHistory.Add(retrieved.Purpose);
            //    }

            //    this.SumOfKilometers.Text = (int.Parse(this.SumOfKilometers.Text) + retrieved.DrivenKilometers).ToString();
            //    this.KilometerGrid.Items.Add(retrieved);
            //}

            this.SumOfKilometers.Text = sum.ToString();
        }

        private async void AddEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //#Model-View-Viewmodel xD
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddKilometerEntry(this._drivenRouteHistory,this._purposeHistory)
            {
                DataContext = new KilometerEntryViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog",view.DialogHost_OnDialogClosing);

            if (!(bool) result || !view.AllSet || view.Output == null) return;

            view.Output.ID = _data.Count!=0?_data.Max(x => x.ID)+1:0;
            _data.Add(view.Output);
            if (!this._drivenRouteHistory.Contains(view.Output.DrivenRoute))
            {
                this._drivenRouteHistory.Add(view.Output.DrivenRoute);
            }
            if (!this._purposeHistory.Contains(view.Output.Purpose))
            {
                this._purposeHistory.Add(view.Output.Purpose);
            }

            this.SumOfKilometers.Text = (int.Parse(this.SumOfKilometers.Text)+view.Output.DrivenKilometers).ToString();
        }

        private void DeleteEntryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = _data.Count-1; i >-1 ; i--)
            {
                if (!_data[i].IsSelected)continue;
                
                this._drivenRouteHistory.Remove(_data[i].DrivenRoute);
                this._purposeHistory.Remove(_data[i].Purpose);
                this.SumOfKilometers.Text = (int.Parse(this.SumOfKilometers.Text) - _data[i].DrivenKilometers).ToString();
                _data.Remove(_data[i]);
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            KilometerEntryViewModel[]items=new KilometerEntryViewModel[KilometerGrid.Items.Count];
            KilometerGrid.Items.CopyTo(items,0);
            MainWindow.StorageService.UpdateKilometerEntry(new List<KilometerEntryViewModel>(items));
        }

        private readonly ObservableCollection<KilometerEntryViewModel> _data;
        private readonly List<string> _purposeHistory, _drivenRouteHistory;
    }
}
