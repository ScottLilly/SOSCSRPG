namespace SOSCSRPG.Models
{
    public class ItemQuantity
    {
        private readonly GameItem _gameItem;

        public int ItemID => _gameItem.ItemTypeID;
        public int Quantity { get; }

        public string QuantityItemDescription => 
            $"{Quantity} {_gameItem.Name}";

        public ItemQuantity(GameItem item, int quantity)
        {
            _gameItem = item;
            Quantity = quantity;
        }
    }
}