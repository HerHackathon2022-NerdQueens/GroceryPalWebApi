using AutoMapper;
using GroceryPalWebApi.Code;
using GroceryPalWebApi.DTO;
using GroceryPalWebApi.Model;
using GroceryPalWebApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPalWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class ShoppingListController : ControllerBase
    {
        private readonly GroceryPalContext _context;
        private readonly IMapper _mapper;

        public ShoppingListController(GroceryPalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [SwaggerOperation(Summary = "Get all products from shopping list")]
        [HttpGet]
        public async Task<ActionResult<ShoppingListDTO>> GetShoppingListAsync()
        {
            var shoppingList = await _context.ShoppingLists
                .Include(l => l.ShoppingListItems)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Category)
                .Include(l => l.ShoppingListItems)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.ProductTags)
                .ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync();
            var shoppingListDTO = _mapper.Map<ShoppingListDTO>(shoppingList);
            return Ok(shoppingListDTO);
        }

        [SwaggerOperation(Summary = "Add one product to shopping list")]
        [HttpPost("{productId}")]
        public async Task<ActionResult> AddProductAsync([FromRoute] int productId)
        {
            var product = await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
            if (product == null)
                return NotFound();

            var shoppingList = await _context.ShoppingLists.Include(l => l.ShoppingListItems).FirstOrDefaultAsync();
            var shoppingListItem = shoppingList.ShoppingListItems.Where(i => i.ProductId == productId).FirstOrDefault();

            if (shoppingListItem == null)
            {
                shoppingList.ShoppingListItems.Add(new ShoppingListItem
                {
                    Product = product,
                    Amount = 1
                });
            }
            else
            {
                shoppingListItem.Amount++;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [SwaggerOperation(Summary = "Remove one product from shopping list")]
        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProductAsync([FromRoute] int productId)
        {
            var shoppingList = await _context.ShoppingLists.Include(l => l.ShoppingListItems).FirstOrDefaultAsync();

            if (!shoppingList.ShoppingListItems.Any(i => i.ProductId == productId))
                return BadRequest();

            var shoppingListItem = shoppingList.ShoppingListItems.Where(i => i.ProductId == productId).FirstOrDefault();

            if (shoppingListItem.Amount == 1)
                _context.ShoppingListItems.Remove(shoppingListItem);
            else
                shoppingListItem.Amount--;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [SwaggerOperation(Summary = "Add ingredients from recipe to shopping list")]
        [HttpPost("IngredientsFromRecipe/{recipeId}")]
        public async Task<ActionResult> AddIngredientsFromRecipeAsync([FromRoute] int recipeId)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Product)
                .Where(r => r.Id == recipeId)
                .FirstOrDefaultAsync();
            if (recipe == null)
                return BadRequest("Invalid recipeId");

            var shoppingList = await _context.ShoppingLists.Include(l => l.ShoppingListItems).FirstOrDefaultAsync();

            foreach (var ingredient in recipe.Ingredients)
            {
                var shoppingListItem = shoppingList.ShoppingListItems.Where(i => i.ProductId == ingredient.Product.Id).FirstOrDefault();
                if (shoppingListItem == null)
                {
                    var product = await _context.Products.Where(p => p.Id == ingredient.Product.Id).FirstOrDefaultAsync();

                    shoppingList.ShoppingListItems.Add(new ShoppingListItem
                    {
                        Product = product,
                        Amount = ingredient.Amount
                    });
                }
                else
                {
                    shoppingListItem.Amount += ingredient.Amount;
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [SwaggerOperation(Summary = "Finds optimal route through the store based on the shopping list")]
        [HttpPost("FindOptimalRoute")]
        public async Task<ActionResult<List<RouteItemDTO>>> FindOptimalRouteAsync()
        {
            var shoppingList = await _context.ShoppingLists
                .Include(l => l.ShoppingListItems)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Category)
                .Include(s => s.Store)
                .FirstOrDefaultAsync();

            
            if (!shoppingList.ShoppingListItems.Any())
                return Ok(new List<RouteItemDTO>());
            

            shoppingList.Store = UtilService.InitStoreLayoutAndDistanceMatrix(shoppingList.Store);

            // creating subgraph for TSP
            var categories = shoppingList.ShoppingListItems.Select(i => i.Product.Category).ToList();
            var categoriesIds = categories.Select(c => c.Id - 1).Distinct().ToList();

            var subgraph = new int[categoriesIds.Count, categoriesIds.Count];

            for (int i = 0; i < categoriesIds.Count; i++)
            {
                for (int j = 0; j < categoriesIds.Count; j++)
                {
                    subgraph[i, j] = shoppingList.Store.DistanceMatrix[categoriesIds[i], categoriesIds[j]];
                }
            }
 
            var routeOrderList = TravelingSalesmanProblem.Run(categoriesIds.Count, subgraph, 0);
            var result = new List<RouteItemDTO>();

            // returning result
            for (int i = 0; i < routeOrderList.Count; i++)
            {
                var category = categories[routeOrderList[i]];
                var routeOrder = new RouteItemDTO
                {
                    OrderId = i + 1,
                    CategoryName = category.CategoryName,
                    ProductsToPick = shoppingList.ShoppingListItems.Where(i => i.Product.CategoryId == category.Id).Select(i => _mapper.Map<ProductDTO>(i.Product)).ToList()
                };
                result.Add(routeOrder);
            }

            return Ok(result);
        }
    }
}
