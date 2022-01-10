using System.ComponentModel;

namespace SOSCSRPG.Models
{
    public class PopupDetails : INotifyPropertyChanged
    {
        public bool IsVisible { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int MinHeight { get; set; }
        public int MaxHeight { get; set; }
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}