using AutoMapper;
using GroceryPalWebApi.DTO;
using GroceryPalWebApi.Model;

namespace GroceryPalWebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>();
        }
    }
}
