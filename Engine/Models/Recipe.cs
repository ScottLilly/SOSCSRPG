using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Engine.Models
{
    public class Recipe
    {
        public int ID { get; }
        [JsonIgnore]
        public string Name { get; }

        [JsonIgnore] public List<ItemQuantity> Ingredients { get; }
        [JsonIgnore]
        public List<ItemQuantity> OutputItems { get; }

        [JsonIgnore]
        public string ToolTipContents =>
            "Ingredients" + Environment.NewLine +
            "===========" + Environment.NewLine +
            string.Join(Environment.NewLine, Ingredients.Select(i => i.QuantityItemDescription)) +
            Environment.NewLine + Environment.NewLine +
            "Creates" + Environment.NewLine +
            "===========" + Environment.NewLine +
            string.Join(Environment.NewLine, OutputItems.Select(i => i.QuantityItemDescription));

        public Recipe(int id, string name, List<ItemQuantity> ingredients, List<ItemQuantity> outputItems)
        {
            ID = id;
            Name = name;
            Ingredients = ingredients;
            OutputItems = outputItems;
        }
    }
}