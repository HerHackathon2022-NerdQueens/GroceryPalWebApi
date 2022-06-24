using System.Collections.Generic;

namespace GroceryPalWebApi.DTO
{
    public class RecipeInputDTO
    {
        public string RecipeName { get; set; }
        public List<IngredientInputDTO> IngredientInputs { get; set; }
        public string Instructions { get; set; }
    }
}
