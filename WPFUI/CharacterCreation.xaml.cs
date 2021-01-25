using System.Windows;
using System.Windows.Controls;
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
            VM.RollNewCharacter();
        }

        private void UseThisPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(VM.GetPlayer());
            mainWindow.Show();
            Close();
        }

        private void Race_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.ApplyAttributeModifiers();
        }
    }
}