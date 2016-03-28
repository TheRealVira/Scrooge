namespace Scrooge
{
    /// <summary>
    ///     Interaction logic for TextMessageBox.xaml
    /// </summary>
    public partial class TextMessageBox
    {
        public TextMessageBox(string text)
        {
            this.Text = text;
            this.InitializeComponent();
            this.TextBlock.DataContext = this;
        }

        public string Text { get; private set; }
    }
}