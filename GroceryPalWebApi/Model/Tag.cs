using System;
using System.Collections.Generic;

namespace GroceryPalWebApi.Model
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }

        public List<ProductTag> ProductTags { get; set; }
    }
}
