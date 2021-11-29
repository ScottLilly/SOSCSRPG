using System.ComponentModel;

namespace Engine.Models
{
    public class QuestStatus : INotifyPropertyChanged
    {
        private bool _isCompleted;

        public event PropertyChangedEventHandler PropertyChanged;

        public Quest PlayerQuest { get; }
        public bool IsCompleted
        {
            get { return _isCompleted;}
            set
            {
                _isCompleted = value;
            }
        }

        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}