using System.Collections.Generic;

namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public int ID { get; }
        public string ImageName { get; }
        public int RewardExperiencePoints { get; }
        public List<ItemPercentage> LootTable { get; } = 
            new List<ItemPercentage>();

        public Monster(int id, string name, string imageName,
                       int maximumHitPoints, IEnumerable<PlayerAttribute> attributes,
                       GameItem currentWeapon,
                       int rewardExperiencePoints, int gold) :
            base(name, maximumHitPoints, maximumHitPoints, attributes, gold)
        {
            ID = id;
            ImageName = imageName;
            CurrentWeapon = currentWeapon;
            RewardExperiencePoints = rewardExperiencePoints;
        }

        public void AddItemToLootTable(int id, int percentage)
        {
            // Remove the entry from the loot table,
            // if it already contains an entry with this ID
            LootTable.RemoveAll(ip => ip.ID == id);

            LootTable.Add(new ItemPercentage(id, percentage));
        }

        public Monster Clone()
        {
            Monster newMonster =
                new Monster(ID, Name, ImageName, MaximumHitPoints, Attributes,
                    CurrentWeapon, RewardExperiencePoints, Gold);

            newMonster.LootTable.AddRange(LootTable);

            return newMonster;
        }
    }
}