namespace Engine.Models
{
    public class MonsterEncounter
    {
        public int MonsterID { get; set; }
        public int ChanceOfEncountering { get; set; }

        public MonsterEncounter(int monsterID, int chanceOfEncountering)
        {
            MonsterID = monsterID;
            ChanceOfEncountering = chanceOfEncountering;
        }
    }
}
