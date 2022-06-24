namespace GroceryPalWebApi.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int ReceipeId { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }    
        public int ProductId { get; set; }

        public Receipe Receipe { get; set; }
        public Product Product { get; set; }
    }
}
