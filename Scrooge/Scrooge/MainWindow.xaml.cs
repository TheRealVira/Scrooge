using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using Scrooge.Service;
using Scrooge.Service.Definitions;

namespace Scrooge
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static IStorageService StorageService;
        private readonly IApplicationEventService service;

        public MainWindow()
        {
            this.InitializeComponent();
            this.service = Singleton<ServiceController>.Instance.Get<IApplicationEventService>();
            MainWindow.StorageService = Singleton<ServiceController>.Instance.Get<IStorageService>();
        }

        private void Help_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private async void Warning_OnClick(object sender, RoutedEventArgs e)
        {
            await
                DialogHost.Show(
                    new WarningView(),
                    "RootDialog");
        }

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            this.service.TriggerApplicationClosing();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.service.TriggerApplicationInitialized();

            if (Singleton<ServiceController>.Instance.Get<IWarningService>().ShouldShowNewWarningsDialog)
            {
                this.Dispatcher.InvokeAsync(async () =>
                {
                    await
                        DialogHost.Show(
                            new TextMessageBox(
                                "You have unread warnings! Visit the warnings tab on the left to display them."),
                            "RootDialog");
                });
            }
        }
    }
}