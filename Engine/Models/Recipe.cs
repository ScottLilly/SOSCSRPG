using System.Collections.Generic;
using System.Linq;

namespace Engine.Models
{
    public class Recipe
    {
        public int ID { get; }
        public string Name { get; }
        public List<ItemQuantity> Ingredients { get; } = new List<ItemQuantity>();
        public List<ItemQuantity> OutputItems { get; } = new List<ItemQuantity>();

        public Recipe(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public void AddIngredient(int itemID, int quantity)
        {
            if(!Ingredients.Any(x => x.ItemID == itemID))
            {
                Ingredients.Add(new ItemQuantity(itemID, quantity));
            }
        }

        public void AddOutputItem(int itemID, int quantity)
        {
            if(!OutputItems.Any(x => x.ItemID == itemID))
            {
                OutputItems.Add(new ItemQuantity(itemID, quantity));
            }
        }
    }
}