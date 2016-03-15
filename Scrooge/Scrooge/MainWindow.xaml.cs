using System;
using System.Net;
using System.Windows;
using Scrooge.Service;
using Scrooge.Service.Definitions;
using Scrooge.Service.Implementations;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private IApplicationEventService service;
        public static IStorageService StorageService;
        public MainWindow()
        {
            InitializeComponent();
            service = Singleton<ServiceController>.Instance.Get<IApplicationEventService>();
            StorageService = Singleton<ServiceController>.Instance.Get<IStorageService>();
        }

        private void Help_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void Warning_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            service.TriggerApplicationClosing();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            service.TriggerApplicationInitialized();
        }
    }
}
