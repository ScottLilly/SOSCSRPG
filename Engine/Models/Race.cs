using System.Collections.Generic;

namespace Engine.Models
{
    public class Race
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public List<PlayerAttributeModifier> PlayerAttributeModifiers { get; } =
            new List<PlayerAttributeModifier>();
    }
}