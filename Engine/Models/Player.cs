using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Properties

        private string _characterClass;
        private int _experiencePoints;
        private int _level;

        public string CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; set; }

        #endregion

        public Player()
        {
            Quests = new ObservableCollection<QuestStatus>();
        }

        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity)
                {
                    return false;
                }
            }

            return true;
        }
    }
}