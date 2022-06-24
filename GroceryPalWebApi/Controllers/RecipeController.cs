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
    public class RecipeController : ControllerBase
    {
        private readonly GroceryPalContext _context;
        private readonly IMapper _mapper;

        public RecipeController(GroceryPalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [SwaggerOperation(Summary = "Get all recipes")]
        [HttpGet]
        public async Task<ActionResult<List<RecipeDTO>>> GetRecipesAsync()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Category)
                .Select(r => _mapper.Map<RecipeDTO>(r))
                .ToListAsync();
            return Ok(recipes);
        }

        [SwaggerOperation(Summary = "Get recipe by ID")]
        [HttpGet("{recipeId}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipeByIdAsync([FromRoute] int recipeId)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Category)
                .Select(r => _mapper.Map<RecipeDTO>(r))
                .Where(r => r.Id == recipeId)
                .FirstOrDefaultAsync();
            if (recipe == null)
                return NotFound("Invalid recipeId");

            var recipeDTO = _mapper.Map<RecipeDTO>(recipe);
            return Ok(recipeDTO);
        }

        [SwaggerOperation(Summary = "Add new recipe")]
        [HttpPost]
        public async Task<ActionResult> AddRecipeAsync([FromBody] RecipeInputDTO req)
        {
            var recipe = new Recipe
            {
                RecipeName = req.RecipeName,
                Ingredients = new List<Ingredient>(),
                Instructions = req.Instructions
            };

            var reqProductIds = req.IngredientInputs.Select(i => i.ProductId).ToList();
            var products = await _context.Products.Where(p => reqProductIds.Contains(p.Id)).ToListAsync();
            if (products.Count != req.IngredientInputs.Count) // Not all listed ingredients exist in database of products
                return BadRequest("Some invalid productId in ingredients list");

            foreach (var ingredientInput in req.IngredientInputs)
            {
                var product = products.Where(p => p.Id == ingredientInput.ProductId).FirstOrDefault();
                var ingredient = new Ingredient
                {
                    Product = product,
                    Amount = ingredientInput.Amount
                };
                recipe.Ingredients.Add(ingredient);
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [SwaggerOperation(Summary = "Edit recipe")]
        [HttpPut("{recipeId}")]
        public async Task<ActionResult> EditRecipeAsync([FromRoute] int recipeId, [FromBody] RecipeInputDTO req)
        {
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId).FirstOrDefaultAsync();
            if (recipe == null)
                return NotFound();

            recipe.RecipeName = req.RecipeName;
            recipe.Instructions = req.Instructions;

            _context.Ingredients.RemoveRange(recipe.Ingredients);
            recipe.Ingredients = new List<Ingredient>();

            var reqProductIds = req.IngredientInputs.Select(i => i.ProductId).ToList();
            var products = await _context.Products.Where(p => reqProductIds.Contains(p.Id)).ToListAsync();
            if (products.Count != req.IngredientInputs.Count) // Not all listed ingredients exist in database of products
                return BadRequest("Some invalid productId in ingredients list");

            foreach (var ingredientInput in req.IngredientInputs)
            {
                var product = products.Where(p => p.Id == ingredientInput.ProductId).FirstOrDefault();
                var ingredient = new Ingredient
                {
                    Product = product,
                    Amount = ingredientInput.Amount
                };
                recipe.Ingredients.Add(ingredient);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [SwaggerOperation(Summary = "Delete recipe")]
        [HttpDelete("{recipeId}")]
        public async Task<ActionResult> DeleteRecipeAsync([FromRoute] int recipeId)
        {
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId).FirstOrDefaultAsync();
            if (recipe == null)
                return NotFound("Invalid recipeId");

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
