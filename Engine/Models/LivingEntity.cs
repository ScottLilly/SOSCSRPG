using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotificationClass
    {
        private string _name;
        private int _currentHitPoints;
        private int _maximumHitPoints;
        private int _gold;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value; 
                OnPropertyChanged(nameof(Name));
            }
        }

        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            set
            {
                _currentHitPoints = value; 
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }

        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            set
            {
                _maximumHitPoints = value; 
                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }

        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value; 
                OnPropertyChanged(nameof(Gold));
            }
        }

        public ObservableCollection<GameItem> Inventory { get; set; }

        public List<GameItem> Weapons =>
            Inventory.Where(i => i is Weapon).ToList();

        protected LivingEntity()
        {
            Inventory = new ObservableCollection<GameItem>();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);

            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);

            OnPropertyChanged(nameof(Weapons));
        }
    }
}