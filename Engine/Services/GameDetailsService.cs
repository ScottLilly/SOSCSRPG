using System.IO;
using Engine.Models;
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
                new GameDetails(gameDetailsJson["Title"].ToString(), 
                                gameDetailsJson["SubTitle"].ToString(),
                                gameDetailsJson["Version"].ToString());

            foreach(JToken token in gameDetailsJson["PlayerAttributes"])
            {
                gameDetails.PlayerAttributes.Add(new PlayerAttribute(token["Key"].ToString(),
                                                                           token["DisplayName"].ToString(),
                                                                           token["DiceNotation"].ToString()));
            }

            return gameDetails;
        }
    }
}