using AutoMapper;
using GroceryPalWebApi.DTO;
using GroceryPalWebApi.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPalWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class TagController : ControllerBase
    {
        private readonly GroceryPalContext _context;
        private readonly IMapper _mapper;

        public TagController(GroceryPalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TagDTO>>> GetTagsAsync()
        {
            var tags = await _context.Tags.Select(c => _mapper.Map<TagDTO>(c)).ToListAsync();
            return Ok(tags);
        }
    }
}
