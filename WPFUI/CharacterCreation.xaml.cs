using System.Windows;
using Engine.Models;
using Engine.Services;

namespace WPFUI
{
    public partial class CharacterCreation : Window
    {
        private GameDetails _gameDetails;

        public CharacterCreation()
        {
            InitializeComponent();

            _gameDetails = GameDetailsService.ReadGameDetails();

            DataContext = _gameDetails;
        }

        private void RandomPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}