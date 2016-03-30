using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Scrooge.Service;
using Scrooge.Service.Definitions;

namespace Scrooge
{
    /// <summary>
    ///     Interaction logic for WarningView.xaml
    /// </summary>
    public partial class WarningView
    {
        public WarningView()
        {
            this.InitializeComponent();

            // Init
            var warningService = Singleton<ServiceController>.Instance.Get<IWarningService>();
            var lightGrayBrush = new SolidColorBrush(Colors.DarkGray);

            foreach (var warning in warningService.GetWarnings().OrderByDescending(w => w.Issued))
            {
                var inline = new Span();
                inline.Inlines.Add(new Bold(new Run(warning.Title)));
                inline.Inlines.Add(new LineBreak());
                inline.Inlines.Add(new Run(warning.Message));
                inline.Inlines.Add(new LineBreak());
                inline.Inlines.Add(new Run(warning.Issued.ToShortDateString()) {Foreground = lightGrayBrush});
                if (warning.Read)
                {
                    foreach (var i in inline.Inlines)
                    {
                        i.Foreground = lightGrayBrush;
                    }
                }
                this.StackPanel.Children.Add(new TextBlock(inline) {Margin = new Thickness(5), LineHeight = 20});
            }

            warningService.SetAllWarningsRead();
        }
    }
}