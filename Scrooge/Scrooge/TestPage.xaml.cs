using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Scrooge
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage
    {
        public TestPage()
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
