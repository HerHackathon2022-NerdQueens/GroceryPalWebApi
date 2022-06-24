using AutoMapper;
using GroceryPalWebApi.DTO;
using GroceryPalWebApi.Model;
using System.Linq;

namespace GroceryPalWebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<Tag, TagDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(a => a.Category.CategoryName))
                .ForMember(dest => dest.ProductTags, opt => opt.MapFrom(a => a.ProductTags.Select(t => t.Tag).ToList()));
            CreateMap<ShoppingList, ShoppingListDTO>();
            CreateMap<ShoppingListItem, ShoppingListItemDTO>();
            CreateMap<Recipe, RecipeDTO>();
            CreateMap<Ingredient, IngredientDTO>();

        }
    }
}
