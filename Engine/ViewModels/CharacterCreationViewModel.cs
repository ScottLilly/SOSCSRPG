using System.Collections.Generic;
using Engine.Models;
using Engine.Services;

namespace Engine.ViewModels
{
    public class CharacterCreationViewModel
    {
        public GameDetails GameDetails { get; } =
            GameDetailsService.ReadGameDetails();

        public Race SelectedRace { get; set; }
        public string Name { get; set; }
        public List<PlayerAttribute> PlayerAttributes { get; set; } =
            new List<PlayerAttribute>();
    }
}