using System.Windows;
using Engine.Models;
using Engine.Services;

namespace WPFUI
{
    public partial class Startup : Window
    {
        private GameDetails _gameDetails;

        public Startup()
        {
            InitializeComponent();

            _gameDetails = GameDetailsService.ReadGameDetails();

            DataContext = _gameDetails;
        }

        private void StartNewGame_OnClick(object sender, RoutedEventArgs e)
        {
            CharacterCreation characterCreationWindow = new CharacterCreation();
            characterCreationWindow.Show();
            Close();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}