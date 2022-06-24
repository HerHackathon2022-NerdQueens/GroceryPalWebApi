using System.Collections.Generic;

namespace GroceryPalWebApi.Model
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ShoppingListId { get; set; }

        public Store Store { get; set; }
        public List<ShoppingListItem> ShoppingListItems { get; set; }
    }
}
