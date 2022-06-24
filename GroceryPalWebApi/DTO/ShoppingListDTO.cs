using System.Collections.Generic;

namespace GroceryPalWebApi.DTO
{
    public class ShoppingListDTO
    {
        public int Id { get; set; }
        public List<ShoppingListItemDTO> ShoppingListItems { get; set; }
    }
}
