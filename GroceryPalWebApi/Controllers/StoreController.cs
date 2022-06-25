using AutoMapper;
using GroceryPalWebApi.DTO;
using GroceryPalWebApi.Model;
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
    public class StoreController : ControllerBase
    {
        private readonly GroceryPalContext _context;
        private readonly IMapper _mapper;

        public StoreController(GroceryPalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [SwaggerOperation(Summary = "Get all store")]
        [HttpGet]
        public async Task<ActionResult<List<StoreDTO>>> GetAllStoresAsync()
        {
            var stores = await _context.Stores
                .Select(s => _mapper.Map<StoreDTO>(s))
                .ToListAsync();
            return Ok(stores);
        }

        [SwaggerOperation(Summary = "Get store by ID")]
        [HttpGet("{storeId}")]
        public async Task<ActionResult<StoreDTO>> GetStoreByIdAsync([FromRoute] int storeId)
        {
            var store = await _context.Stores
                .Where(s => s.Id == storeId)
                .Select(s => _mapper.Map<StoreDTO>(s))
                .FirstOrDefaultAsync();

            if (store == null)
                return NotFound("Invalid storeId");

            return Ok(store);
        }

        [SwaggerOperation(Summary = "Set store as deafult for shopping")]
        [HttpPost("SetAsDefault/{storeId}")]
        public async Task<ActionResult> SetStoreAsDefaultAsync()
        {
            var store = await _context.Stores.FirstOrDefaultAsync();
            if (store == null)
                return NotFound("Invalid storeId");

            var shoppingList = await _context.ShoppingLists.FirstOrDefaultAsync();

            shoppingList.Store = store;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
