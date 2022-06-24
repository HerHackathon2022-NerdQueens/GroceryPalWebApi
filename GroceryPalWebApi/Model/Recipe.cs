using System.Collections.Generic;

namespace GroceryPalWebApi.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Instructions { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
