using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPalWebApi.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Pack { get; set; }
        public float Price { get; set; }
        public string Eans { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public List<ProductTag> ProductTags { get; set; }
        public List<Ingredient> ReceipeIngredients { get; set; }

        public List<ShoppingListItem> ShoppingListItems { get; set; }
    }
}
