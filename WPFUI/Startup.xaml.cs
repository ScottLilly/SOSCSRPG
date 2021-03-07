using System;
using System.Windows;
using Engine.Services;
using Engine.ViewModels;
using Microsoft.Win32;

namespace WPFUI
{
    public partial class Startup : Window
    {
        private const string SAVE_GAME_FILE_EXTENSION = "soscsrpg";

        public Startup()
        {
            InitializeComponent();

            DataContext = GameDetailsService.ReadGameDetails();
        }
 
        private void StartNewGame_OnClick(object sender, RoutedEventArgs e)
        {
            CharacterCreation characterCreationWindow = new CharacterCreation();
            characterCreationWindow.Show();
            Close();
        }

        private void LoadSavedGame_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog =
                new OpenFileDialog
                {
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    Filter = $"Saved games (*.{SAVE_GAME_FILE_EXTENSION})|*.{SAVE_GAME_FILE_EXTENSION}"
                };

            if (openFileDialog.ShowDialog() == true)
            {
                GameSession gameSession = 
                    SaveGameService.LoadLastSaveOrCreateNew(openFileDialog.FileName);
                
                MainWindow mainWindow = 
                    new MainWindow(gameSession.CurrentPlayer,
                                   gameSession.CurrentLocation.XCoordinate,
                                   gameSession.CurrentLocation.YCoordinate);
                
                mainWindow.Show();
                Close();
            }
        }
 
        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}