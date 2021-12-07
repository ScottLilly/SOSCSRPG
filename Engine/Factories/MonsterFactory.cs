using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Engine.Models;
using Engine.Services;
using Engine.Shared;
using SOSCSRPG.Core;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Monsters.xml";

        private static readonly GameDetails s_gameDetails;
        private static readonly List<Monster> s_baseMonsters = new List<Monster>();

        static MonsterFactory()
        {
            if(File.Exists(GAME_DATA_FILENAME))
            {
                s_gameDetails = GameDetailsService.ReadGameDetails();
                
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                string rootImagePath =
                    data.SelectSingleNode("/Monsters")
                        .AttributeAsString("RootImagePath");

                LoadMonstersFromNodes(data.SelectNodes("/Monsters/Monster"), rootImagePath);
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        public static Monster GetMonsterFromLocation(Location location)
        {
            if (!location.MonstersHere.Any())
            {
                return null;
            }

            // Total the percentages of all monsters at this location.
            int totalChances = location.MonstersHere.Sum(m => m.ChanceOfEncountering);

            // Select a random number between 1 and the total (in case the total chances is not 100).
            int randomNumber = DiceService.Instance.Roll(totalChances, 1).Value;

            // Loop through the monster list, 
            // adding the monster's percentage chance of appearing to the runningTotal variable.
            // When the random number is lower than the runningTotal,
            // that is the monster to return.
            int runningTotal = 0;

            foreach (MonsterEncounter monsterEncounter in location.MonstersHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;

                if (randomNumber <= runningTotal)
                {
                    return GetMonster(monsterEncounter.MonsterID);
                }
            }

            // If there was a problem, return the last monster in the list.
            return GetMonster(location.MonstersHere.Last().MonsterID);
        }

        private static void LoadMonstersFromNodes(XmlNodeList nodes, string rootImagePath)
        {
            if(nodes == null)
            {
                return;
            }

            foreach(XmlNode node in nodes)
            {
                var attributes = s_gameDetails.PlayerAttributes;

                attributes.First(a => a.Key.Equals("DEX")).BaseValue =
                    Convert.ToInt32(node.SelectSingleNode("./Dexterity").InnerText);
                attributes.First(a => a.Key.Equals("DEX")).ModifiedValue =
                    Convert.ToInt32(node.SelectSingleNode("./Dexterity").InnerText);
                
                Monster monster =
                    new Monster(node.AttributeAsInt("ID"),
                                node.AttributeAsString("Name"),
                                $".{rootImagePath}{node.AttributeAsString("ImageName")}",
                                node.AttributeAsInt("MaximumHitPoints"),
                                attributes,
                                ItemFactory.CreateGameItem(node.AttributeAsInt("WeaponID")),
                                node.AttributeAsInt("RewardXP"),
                                node.AttributeAsInt("Gold"));

                XmlNodeList lootItemNodes = node.SelectNodes("./LootItems/LootItem");
                
                if(lootItemNodes != null)
                {
                    foreach(XmlNode lootItemNode in lootItemNodes)
                    {
                        monster.AddItemToLootTable(lootItemNode.AttributeAsInt("ID"),
                                                   lootItemNode.AttributeAsInt("Percentage"));
                    }
                }

                s_baseMonsters.Add(monster);
            }
        }

        private static Monster GetMonster(int id)
        {
            Monster newMonster = s_baseMonsters.FirstOrDefault(m => m.ID == id).Clone();

            foreach (ItemPercentage itemPercentage in newMonster.LootTable)
            {
                // Populate the new monster's inventory, using the loot table
                if (DiceService.Instance.Roll(100).Value <= itemPercentage.Percentage)
                {
                    newMonster.AddItemToInventory(ItemFactory.CreateGameItem(itemPercentage.ID));
                }
            }

            return newMonster;
        }
    }
}