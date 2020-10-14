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

            return gameDetails;
        }
    }
}