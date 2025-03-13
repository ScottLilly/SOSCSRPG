using SOSCSRPG.Models.Actions;
using Newtonsoft.Json;

namespace SOSCSRPG.Models
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Miscellaneous,
            Weapon,
            Consumable
        }

        [JsonIgnore]
        public ItemCategory Category { get; }
        public int ItemTypeID { get; }
        [JsonIgnore]
        public string Name { get; }
        [JsonIgnore]
        public int Price { get; }
        [JsonIgnore]
        public bool IsUnique { get; }
        [JsonIgnore]
        public IAction Action { get; set; }
        [JsonIgnore]
        public int StarterQuantity { get; }

        public GameItem(ItemCategory category, int itemTypeID, string name, int price,
                        bool isUnique = false, IAction action = null, int starterQuantity = 0)
        {
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
            Action = action;
            StarterQuantity = starterQuantity;
        }

        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }

        public GameItem Clone()
        {
            return new GameItem(Category, ItemTypeID, Name, Price, IsUnique, Action, StarterQuantity);
        }
    }
}