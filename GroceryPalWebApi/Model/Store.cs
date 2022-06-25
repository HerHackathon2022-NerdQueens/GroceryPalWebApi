using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryPalWebApi.Model
{
    public class Store
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public int ShoppingListId { get; set; }

        // TODO: Think about representing inside of the database
        [NotMapped]
        public int V { get; set; } // Number of categories
        [NotMapped]
        public int[,] StoreLayout { get; set; } // Graph -> Categories are the Nodes
        [NotMapped]
        public int[,] DistanceMatrix { get; set; } // For Traveling Salesman Problem

        public ShoppingList ShoppingList { get; set; }
    }
}
