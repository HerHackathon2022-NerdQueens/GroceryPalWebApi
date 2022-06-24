namespace GroceryPalWebApi.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int ReceipeId { get; set; }
        public uint Amount { get; set; }
        public int ProductId { get; set; }

        public Recipe Receipe { get; set; }
        public Product Product { get; set; }
    }
}
