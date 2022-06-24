using System.Collections.Generic;

namespace GroceryPalWebApi.Model
{
    public class Receipe
    {
        public int Id { get; set; }
        public string ReceipeName { get; set; }
        public string Description { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
