using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPalWebApi.Model
{
    public class Store
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public int ShoppingListId { get; set; }
        // TODO: Store representation

        public ShoppingList ShoppingList { get; set; }
    }
}
