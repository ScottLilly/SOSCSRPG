using System.Collections.Generic;

namespace SOSCSRPG.Models
{
    public class Trader : LivingEntity
    {
        public int ID { get; }

        public Trader(int id, string name) : base(name, 9999, 9999, new List<PlayerAttribute>(), 9999)
        {
            ID = id;
        }
    }
}