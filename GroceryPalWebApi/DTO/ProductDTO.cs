using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPalWebApi.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Pack { get; set; }
        public float Price { get; set; }
        public string Eans { get; set; }
        public string CategoryName { get; set; }
        public List<string> ProductTags { get; set; }
    }
}
