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
    public class ProductController : ControllerBase
    {
        private readonly GroceryPalContext _context;
        private readonly IMapper _mapper;

        public ProductController(GroceryPalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [SwaggerOperation(Summary = "Get all products")]
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProductesAsync()
        {
            var products = await _context.Products
                .Include(p=> p.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(p => p.Tag)
                .Select(c => _mapper.Map<ProductDTO>(c)).ToListAsync();
            return Ok(products);
        }

        [SwaggerOperation(Summary = "Get product by ID")]
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDTO>> GetProducteByIdAsync([FromRoute] int productId)
        {
            var product = await _context.Products.Where(p => p.Id == productId).Select(c => _mapper.Map<ProductDTO>(c)).FirstOrDefaultAsync();
            if (product == null)
                return BadRequest("Invalid productId");
            return Ok(product);
        }
    }
}
