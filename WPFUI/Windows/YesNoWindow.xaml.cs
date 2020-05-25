using System.Windows;

namespace WPFUI.Windows
{
    public partial class YesNoWindow : Window
    {
        public bool ClickedYes { get; private set; }

        public YesNoWindow(string title, string message)
        {
            InitializeComponent();

            Title = title;
            Message.Content = message;
        }

        private void Yes_OnClick(object sender, RoutedEventArgs e)
        {
            ClickedYes = true;
            Close();
        }

        private void No_OnClick(object sender, RoutedEventArgs e)
        {
            ClickedYes = false;
            Close();
        }
    }
}