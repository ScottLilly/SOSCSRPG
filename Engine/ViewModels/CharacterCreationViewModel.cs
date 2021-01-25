using System.Collections.ObjectModel;
using System.Linq;
using Engine.Models;
using Engine.Services;

namespace Engine.ViewModels
{
    public class CharacterCreationViewModel : BaseNotificationClass
    {
        private Race _selectedRace;

        public GameDetails GameDetails { get; }

        public Race SelectedRace
        {
            get => _selectedRace;
            set
            {
                _selectedRace = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }
        public ObservableCollection<PlayerAttribute> PlayerAttributes { get; set; } =
            new ObservableCollection<PlayerAttribute>();

        public bool HasRaces =>
            GameDetails.Races.Any();

        public bool HasRaceAttributeModifiers =>
            HasRaces && GameDetails.Races.Any(r => r.PlayerAttributeModifiers.Any());

        public CharacterCreationViewModel()
        {
            GameDetails = GameDetailsService.ReadGameDetails();

            if(HasRaces)
            {
                SelectedRace = GameDetails.Races.First();
            }
            
            RollNewCharacter();
        }

        public void RollNewCharacter()
        {
            PlayerAttributes.Clear();

            foreach(PlayerAttribute playerAttribute in GameDetails.PlayerAttributes)
            {
                playerAttribute.ReRoll();
                PlayerAttributes.Add(playerAttribute);
            }
            
            ApplyAttributeModifiers();
        }

        public void ApplyAttributeModifiers()
        {
            foreach(PlayerAttribute playerAttribute in PlayerAttributes)
            {
                var attributeRaceModifier =
                    SelectedRace.PlayerAttributeModifiers
                                .FirstOrDefault(pam => pam.AttributeKey.Equals(playerAttribute.Key));

                playerAttribute.ModifiedValue = 
                    playerAttribute.BaseValue + (attributeRaceModifier?.Modifier ?? 0);
            }
        }

        public Player GetPlayer()
        {
            return new Player(Name, "Fighter", 0, 10, 10,
                              PlayerAttributes.FirstOrDefault(pa => pa.Key.Equals("DEX"))?.ModifiedValue ?? 13, 10);
        }
    }
}