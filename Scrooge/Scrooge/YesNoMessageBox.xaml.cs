using System.Windows;
using MaterialDesignThemes.Wpf;

namespace Scrooge
{
    /// <summary>
    ///     Interaction logic for YesNoMessageBox.xaml
    /// </summary>
    public partial class YesNoMessageBox
    {
        public string Text { get; private set; }
        public bool? ResultIsYes;

        public YesNoMessageBox(string text)
        {
            this.Text = text;
            this.InitializeComponent();
            this.TextBlock.DataContext = this;
        }

        private void ButtonYes_OnClick(object sender, RoutedEventArgs e)
        {
            this.ResultIsYes = true;
            DialogHost.CloseDialogCommand.Execute(null, this);
        }

        private void ButtonNo_OnClick(object sender, RoutedEventArgs e)
        {
            this.ResultIsYes = false;
            DialogHost.CloseDialogCommand.Execute(null, this);
        }
    }
}