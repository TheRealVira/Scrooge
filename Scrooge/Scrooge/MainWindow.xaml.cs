using System.Windows;

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GithubButton_Onclick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/TheRealVira/Scrooge");
        }

        private void StackoverflowButton_Onclick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://stackoverflow.com/l");
        }
    }
}
