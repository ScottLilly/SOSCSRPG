using System.Collections.Generic;
using System.Linq;
using Engine.Factories;
using Engine.Models;

namespace Engine.Services
{
    // This service lets us write C# code that is more "functional".
    // Instead of modifying existing objects, and their properties,
    // we always instantiate new objects that have the modified values.

    public static class InventoryService
    {
        public static Inventory AddItem(this Inventory inventory, GameItem item)
        {
            return inventory.AddItems(new List<GameItem> {item});
        }

        public static Inventory AddItemFromFactory(this Inventory inventory, int itemTypeID)
        {
            return inventory.AddItems(new List<GameItem> {ItemFactory.CreateGameItem(itemTypeID)});
        }

        public static Inventory AddItems(this Inventory inventory, IEnumerable<GameItem> items)
        {
            return new Inventory(inventory.Items.Concat(items));
        }

        public static Inventory AddItems(this Inventory inventory,
                                         IEnumerable<ItemQuantity> itemQuantities)
        {
            List<GameItem> itemsToAdd = new List<GameItem>();

            foreach(ItemQuantity itemQuantity in itemQuantities)
            {
                for(int i = 0; i < itemQuantity.Quantity; i++)
                {
                    itemsToAdd.Add(ItemFactory.CreateGameItem(itemQuantity.ItemID));
                }
            }

            return inventory.AddItems(itemsToAdd);
        }

        public static Inventory RemoveItem(this Inventory inventory, GameItem item)
        {
            return inventory.RemoveItems(new List<GameItem> {item});
        }

        public static Inventory RemoveItems(this Inventory inventory, IEnumerable<GameItem> items)
        {
            // REFACTOR: Look for a cleaner solution, with fewer temporary variables.
            List<GameItem> workingInventory = inventory.Items.ToList();
            IEnumerable<GameItem> itemsToRemove = items.ToList();

            foreach(GameItem item in itemsToRemove)
            {
                workingInventory.Remove(item);
            }

            return new Inventory(workingInventory);
        }

        public static Inventory RemoveItems(this Inventory inventory,
                                            IEnumerable<ItemQuantity> itemQuantities)
        {
            // REFACTOR
            Inventory workingInventory = inventory;

            foreach(ItemQuantity itemQuantity in itemQuantities)
            {
                for(int i = 0; i < itemQuantity.Quantity; i++)
                {
                    workingInventory =
                        workingInventory
                            .RemoveItem(workingInventory
                                        .Items
                                        .First(item => item.ItemTypeID == itemQuantity.ItemID));
                }
            }

            return workingInventory;
        }

        public static List<GameItem> ItemsThatAre(this IEnumerable<GameItem> inventory,
                                                  GameItem.ItemCategory category)
        {
            return inventory.Where(i => i.Category == category).ToList();
        }
    }
}