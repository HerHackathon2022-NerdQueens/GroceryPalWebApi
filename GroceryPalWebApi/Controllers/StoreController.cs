using AutoMapper;
using GroceryPalWebApi.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        [SwaggerOperation(Summary = "TODO: Get all store")]
        [HttpGet]
        public async Task<ActionResult> GetAllStoresAsync()
        {
            // TODO: 
            return Ok();
        }

        [SwaggerOperation(Summary = "TODO: Get store by ID")]
        [HttpGet("{storeId}")]
        public async Task<ActionResult> GetStoreByIdAsync()
        {
            // TODO: 
            return Ok();
        }

        [SwaggerOperation(Summary = "TODO: Set store as deafult for shopping")]
        [HttpPost("SetAsDefault/{storeId}")]
        public async Task<ActionResult> SetStoreAsDefaultAsync()
        {
            // TODO: 
            return Ok();
        }
    }
}
