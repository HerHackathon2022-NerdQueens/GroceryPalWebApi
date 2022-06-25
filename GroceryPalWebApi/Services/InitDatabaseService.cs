using GroceryPalWebApi.Code;
using GroceryPalWebApi.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryPalWebApi.Services
{
    public class InitDatabaseService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public InitDatabaseService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<GroceryPalContext>();

                var shoppingList = new ShoppingList { ShoppingListItems = new List<ShoppingListItem>() };
                _context.ShoppingLists.Add(shoppingList);

                #region AddCategories
                var category1 = new Category { CategoryName = "F&V" };
                var category2 = new Category { CategoryName = "H&B" };
                var category3 = new Category { CategoryName = "Drinks (DD Pallets)" };
                var category4 = new Category { CategoryName = "Drinks (Juices, Squash & Fizzy)" };
                var category5 = new Category { CategoryName = "Beers & Ciders" };
                var category6 = new Category { CategoryName = "Wines" };
                var category7 = new Category { CategoryName = "Wine Cellar" };
                var category8 = new Category { CategoryName = "Spirits" };
                var category9 = new Category { CategoryName = "Baby - Nappies" };
                var category10 = new Category { CategoryName = "Toilet and Kitchen Rolls" };
                var category11 = new Category { CategoryName = "Household" };
                var category12 = new Category { CategoryName = "Pet Food" };
                var category13 = new Category { CategoryName = "Washing Powders" };
                var category14 = new Category { CategoryName = "Tissues" };
                var category15 = new Category { CategoryName = "Medicines" };
                var category16 = new Category { CategoryName = "Chocolates & Sweets" };
                var category17 = new Category { CategoryName = "Muesli & Cereals" };
                var category18 = new Category { CategoryName = "Jams & Spreads" };
                var category19 = new Category { CategoryName = "Tea & Coffee" };
                var category20 = new Category { CategoryName = "Assorted Tinned Items - Meals, Meats, Fish & Soups" };
                var category21 = new Category { CategoryName = "Tinned Veg. & Pickles" };
                var category22 = new Category { CategoryName = "Pasta & Rice with Sauce Jars" };
                var category23 = new Category { CategoryName = "Sliced Breads" };
                var category24 = new Category { CategoryName = "Assorted Breads" };
                var category25 = new Category { CategoryName = "Bakery" };
                var category26 = new Category { CategoryName = "F&V Chiller" };
                var category27 = new Category { CategoryName = "Frozen Food" };
                var category28 = new Category { CategoryName = "Ice Creams" };
                var category29 = new Category { CategoryName = "M & P" };
                var category30 = new Category { CategoryName = "Frz. Cabs" };
                var category31 = new Category { CategoryName = "Milk Chiller" };
                var category32 = new Category { CategoryName = "Dairy Chiller" };
                var category33 = new Category { CategoryName = "Nuts" };
                var category34 = new Category { CategoryName = "Snacks" };
                var category35 = new Category { CategoryName = "Spices" };

                _context.Categories.Add(category1);
                _context.Categories.Add(category2);
                _context.Categories.Add(category3);
                _context.Categories.Add(category4);
                _context.Categories.Add(category5);
                _context.Categories.Add(category6);
                _context.Categories.Add(category7);
                _context.Categories.Add(category8);
                _context.Categories.Add(category9);
                /*
                _context.Categories.Add(category10);
                _context.Categories.Add(category11);
                _context.Categories.Add(category12);
                _context.Categories.Add(category13);
                _context.Categories.Add(category14);
                _context.Categories.Add(category15);
                _context.Categories.Add(category16);
                _context.Categories.Add(category17);
                _context.Categories.Add(category18);
                _context.Categories.Add(category19);
                _context.Categories.Add(category20);
                _context.Categories.Add(category21);
                _context.Categories.Add(category22);
                _context.Categories.Add(category23);
                _context.Categories.Add(category24);
                _context.Categories.Add(category25);
                _context.Categories.Add(category26);
                _context.Categories.Add(category27);
                _context.Categories.Add(category28);
                _context.Categories.Add(category29);
                _context.Categories.Add(category30);
                _context.Categories.Add(category31);
                _context.Categories.Add(category32);
                _context.Categories.Add(category33);
                _context.Categories.Add(category34);
                _context.Categories.Add(category35);
                */
                #endregion

                #region AddTags
                var tag1 = new Tag { TagName = "Gluten Free"};
                var tag2 = new Tag { TagName = "Lactose Free" };
                var tag3 = new Tag { TagName = "Organic" };
                var tag4 = new Tag { TagName = "Vegan" };
                var tag5 = new Tag { TagName = "High In Protein" };
                var tag6 = new Tag { TagName = "Biodegardable" };

                _context.Tags.Add(tag1);
                _context.Tags.Add(tag2);
                _context.Tags.Add(tag3);
                _context.Tags.Add(tag4);
                _context.Tags.Add(tag5);
                _context.Tags.Add(tag6);

                #endregion

                await _context.SaveChangesAsync();

                #region AddProducts
                string fileName = "products.json";
                string jsonString = File.ReadAllText(fileName);
                var products = JsonSerializer.Deserialize<List<Product>>(jsonString)!;

                var rnd = new Random();
                var categories = _context.Categories.ToList();

                foreach (var product in products)
                {
                    // Random selection of category only for MOCKING purpose 
                    // (to match with the store layout matrix)
                    var i = rnd.Next(0, categories.Count-1);
                    var category = categories[i]; 
                    product.Category = category;
                    
                    //var category = _context.Categories.Where(p => p.Id == product.CategoryId).FirstOrDefault();
                }

                _context.Products.AddRange(products);
                #endregion

                #region AddReceipes

                var recipe1 = new Recipe
                {
                    RecipeName = "Test Recipe 1",
                    Instructions = "Step 1, Step 2, Step 3, Step 4",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { ProductId = 1383, Amount = 1 }, new Ingredient { ProductId = 1483, Amount = 2}
                    }
                };
                var recipe2 = new Recipe
                {
                    RecipeName = "Test Recipe 2",
                    Instructions = "Step 1, Step 2, Step 3, Step 4",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { ProductId = 1484, Amount = 1 }, new Ingredient { ProductId = 1779, Amount = 1 }, new Ingredient { ProductId = 1858, Amount = 1 } 
                    }
                };

                _context.Add(recipe1);
                _context.Add(recipe2);

                #endregion

                #region AddStore

                var store = new Store
                {
                    StoreName = "Lidl #1",
                    ShoppingList = shoppingList

                };

                _context.Stores.Add(store);
                #endregion

                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
