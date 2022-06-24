using AutoMapper;
using GroceryPalWebApi.DTO;
using GroceryPalWebApi.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
