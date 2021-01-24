using Engine.Services;

namespace Engine.Models
{
    public class PlayerAttribute : BaseNotificationClass
    {
        private int _modifiedValue;
        public string Key { get; }
        public string DisplayName { get; }
        public string DiceNotation { get; }
        public int BaseValue { get; set; }

        public int ModifiedValue
        {
            get => _modifiedValue;
            set
            {
                _modifiedValue = value;
                OnPropertyChanged();
            }
        }

        // Constructor that will use DiceService to create a BaseValue.
        // The constructor this calls will put that same value into BaseValue and ModifiedValue
        public PlayerAttribute(string key, string displayName, string diceNotation)
            : this(key, displayName, diceNotation, DiceService.Instance.Roll(diceNotation).Value)
        {
        }

        // Constructor that takes a baseValue and also uses it for modifiedValue,
        // for when we're creating a new attribute
        public PlayerAttribute(string key, string displayName, string diceNotation,
                               int baseValue) :
            this(key, displayName, diceNotation, baseValue, baseValue)
        {
        }

        // This constructor is eventually called by the others, 
        // or used when reading a Player's attributes from a saved game file.
        public PlayerAttribute(string key, string displayName, string diceNotation,
                               int baseValue, int modifiedValue)
        {
            Key = key;
            DisplayName = displayName;
            DiceNotation = diceNotation;
            BaseValue = baseValue;
            ModifiedValue = modifiedValue;
        }

        public void ReRoll()
        {
            BaseValue = DiceService.Instance.Roll(DiceNotation).Value;
            ModifiedValue = BaseValue;
        }
    }
}