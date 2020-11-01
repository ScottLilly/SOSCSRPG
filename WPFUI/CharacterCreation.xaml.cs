using System.Windows;
using Engine.ViewModels;

namespace WPFUI
{
    public partial class CharacterCreation : Window
    {
        private CharacterCreationViewModel VM { get; set; }

        public CharacterCreation()
        {
            InitializeComponent();

            VM = new CharacterCreationViewModel();
            DataContext = VM;
        }

        private void RandomPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void UseThisPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}