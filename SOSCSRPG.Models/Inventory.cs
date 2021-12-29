using System.Collections.Generic;
using System.Linq;
using SOSCSRPG.Models.Shared;
using Newtonsoft.Json;

namespace SOSCSRPG.Models
{
    public class Inventory
    {
        #region Backing variables

        private readonly List<GameItem> _backingInventory =
            new List<GameItem>();

        private readonly List<GroupedInventoryItem> _backingGroupedInventoryItems =
            new List<GroupedInventoryItem>();

        #endregion

        #region Properties

        public IReadOnlyList<GameItem> Items => _backingInventory.AsReadOnly();

        [JsonIgnore]
        public IReadOnlyList<GroupedInventoryItem> GroupedInventory =>
            _backingGroupedInventoryItems.AsReadOnly();

        [JsonIgnore]
        public IReadOnlyList<GameItem> Weapons =>
            _backingInventory.ItemsThatAre(GameItem.ItemCategory.Weapon).AsReadOnly();

        [JsonIgnore]
        public IReadOnlyList<GameItem> Consumables =>
            _backingInventory.ItemsThatAre(GameItem.ItemCategory.Consumable).AsReadOnly();

        [JsonIgnore]
        public bool HasConsumable => Consumables.Any();

        #endregion

        #region Constructors

        public Inventory(IEnumerable<GameItem> items = null)
        {
            if(items == null)
            {
                return;
            }

            foreach(GameItem item in items)
            {
                _backingInventory.Add(item);

                AddItemToGroupedInventory(item);
            }
        }

        #endregion

        #region Public functions

        public bool HasAllTheseItems(IEnumerable<ItemQuantity> items)
        {
            return items.All(item => Items.Count(i => i.ItemTypeID == item.ItemID) >= item.Quantity);
        }

        public Inventory AddItem(GameItem item)
        {
            return AddItems(new List<GameItem> { item });
        }

        public Inventory AddItems(IEnumerable<GameItem> items)
        {
            return new Inventory(Items.Concat(items));
        }

        public Inventory RemoveItem(GameItem item)
        {
            return RemoveItems(new List<GameItem> { item });
        }

        public Inventory RemoveItems(IEnumerable<GameItem> items)
        {
            // REFACTOR: Look for a cleaner solution, with fewer temporary variables.
            List<GameItem> workingInventory = Items.ToList();
            IEnumerable<GameItem> itemsToRemove = items.ToList();

            foreach (GameItem item in itemsToRemove)
            {
                workingInventory.Remove(item);
            }

            return new Inventory(workingInventory);
        }

        public Inventory RemoveItems(IEnumerable<ItemQuantity> itemQuantities)
        {
            // REFACTOR
            Inventory workingInventory = new Inventory(Items);

            foreach (ItemQuantity itemQuantity in itemQuantities)
            {
                for (int i = 0; i < itemQuantity.Quantity; i++)
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

        #endregion

        #region Private functions

        // REFACTOR: Look for a better way to do this (extension method?)
        private void AddItemToGroupedInventory(GameItem item)
        {
            if(item.IsUnique)
            {
                _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 1));
            }
            else
            {
                if(_backingGroupedInventoryItems.All(gi => gi.Item.ItemTypeID != item.ItemTypeID))
                {
                    _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 0));
                }

                _backingGroupedInventoryItems.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }
        }

        #endregion
    }
}