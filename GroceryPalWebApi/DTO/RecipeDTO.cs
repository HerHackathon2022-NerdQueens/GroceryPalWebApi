using System.Collections.Generic;

namespace GroceryPalWebApi.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Instructions { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
    }
}
