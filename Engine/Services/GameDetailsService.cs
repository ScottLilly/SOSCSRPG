using System.IO;
using Engine.Models;
using Engine.Shared;
using Newtonsoft.Json.Linq;
 
namespace Engine.Services
{
    public static class GameDetailsService
    {
        public static GameDetails ReadGameDetails()
        {
            JObject gameDetailsJson = 
                JObject.Parse(File.ReadAllText(".\\GameData\\GameDetails.json"));
 
            GameDetails gameDetails = 
                new GameDetails(gameDetailsJson.StringValueOf("Title"), 
                                gameDetailsJson.StringValueOf("SubTitle"),
                                gameDetailsJson.StringValueOf("Version"));
 
            foreach(JToken token in gameDetailsJson["PlayerAttributes"])
            {
                gameDetails.PlayerAttributes.Add(new PlayerAttribute(token.StringValueOf("Key"),
                                                                     token.StringValueOf("DisplayName"),
                                                                     token.StringValueOf("DiceNotation")));
            }
 
            if(gameDetailsJson["Races"] != null)
            {
                foreach(JToken token in gameDetailsJson["Races"])
                {
                    Race race = new Race
                                {
                                    Key = token.StringValueOf("Key"),
                                    DisplayName = token.StringValueOf("DisplayName")
                                };
 
                    if(token["PlayerAttributeModifiers"] != null)
                    {
                        foreach(JToken childToken in token["PlayerAttributeModifiers"])
                        {
                            race.PlayerAttributeModifiers.Add(new PlayerAttributeModifier
                                                              {
                                                                  AttributeKey = childToken.StringValueOf("Key"),
                                                                  Modifier = childToken.IntValueOf("Modifier")
                                                              });
                        }
                    }
 
                    gameDetails.Races.Add(race);
                }
            }
 
            return gameDetails;
        }
    }
}