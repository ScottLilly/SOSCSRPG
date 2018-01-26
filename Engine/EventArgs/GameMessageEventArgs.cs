namespace Engine.EventArgs
{
    public class GameMessageEventArgs : System.EventArgs
    {
        public string Message { get; private set; }

        public GameMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}