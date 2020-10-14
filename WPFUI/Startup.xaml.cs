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
    }
}