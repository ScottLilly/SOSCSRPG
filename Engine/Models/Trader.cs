namespace Engine.Models
{
    public class Trader : LivingEntity
    {
        public int ID { get; }

        public Trader(int id, string name) : base(name, 9999, 9999, 9999)
        {
            ID = id;
        }
    }
}