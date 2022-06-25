using System.Collections.Generic;

namespace GroceryPalWebApi.DTO
{
    public class RouteItemDTO
    {
        public int OrderId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductDTO> ProductsToPick { get; set; }
    }
}
