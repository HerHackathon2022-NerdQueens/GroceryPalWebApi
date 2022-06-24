using GroceryPalWebApi.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

                var category = new Category
                {
                    CategoryName = "Fruit"
                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
