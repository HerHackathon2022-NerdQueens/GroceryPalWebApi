using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPalWebApi.Model
{
    public class GroceryPalContext : DbContext
    {
        public GroceryPalContext(DbContextOptions<GroceryPalContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.CategoryName).IsUnique();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.TagName).IsUnique();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasIndex(x => new { x.ProductId, x.TagId }).IsUnique();

                entity.HasOne(x => x.Product)
                .WithMany(x => x.ProductTags)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Tag)
                .WithMany(x => x.ProductTags)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Receipe)
                .WithMany(x => x.Ingredients)
                .HasForeignKey(x => x.ReceipeId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Product)
                .WithMany(x => x.ReceipeIngredients)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Store)
                .WithOne(x => x.ShoppingList)
                .HasForeignKey<ShoppingList>(x => x.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ShoppingListItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.ShoppingList)
                .WithMany(x => x.ShoppingListItems)
                .HasForeignKey(x => x.ShoppingListId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Product)
                .WithMany(x => x.ShoppingListItems)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.ShoppingList)
                .WithOne(x => x.Store)
                .HasForeignKey<Store>(x => x.ShoppingListId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
