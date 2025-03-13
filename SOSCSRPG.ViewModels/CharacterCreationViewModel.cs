using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SOSCSRPG.Services.Factories;
using SOSCSRPG.Models;
using SOSCSRPG.Services;

namespace SOSCSRPG.ViewModels
{
    public class CharacterCreationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public GameDetails GameDetails { get; }
        public Race SelectedRace { get; init; }
        public string Name { get; init; }
        public ObservableCollection<PlayerAttribute> PlayerAttributes { get; } =
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
            Player player = new Player(Name, 0, 10, 10, PlayerAttributes, 10);

            // Give player default inventory items, weapons, recipes, etc.
            foreach (GameItem item in ItemFactory.StarterItems())
            {
                player.AddItemToInventory(ItemFactory.ItemById(item.ItemTypeID));
            }

            foreach (Recipe recipe in RecipeFactory.StarterRecipes())
            {
                player.LearnRecipe(RecipeFactory.RecipeByID(recipe.ID));
            }

            return player;
        }
    }
}