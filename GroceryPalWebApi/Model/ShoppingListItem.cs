namespace GroceryPalWebApi.Model
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        public int ShoppingListId { get; set; }
        public float Amount { get; set; }
        public int ProductId { get; set; }

        public ShoppingList ShoppingList { get; set; }
        public Product Product { get; set; }
    }
}
