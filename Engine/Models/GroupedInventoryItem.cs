using System.ComponentModel;

namespace Engine.Models
{
    public class GroupedInventoryItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public GameItem Item { get; set; }
        public int Quantity { get; set; }

        public GroupedInventoryItem(GameItem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}