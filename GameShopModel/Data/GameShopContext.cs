using GameShopModel.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Data
{
    public class GameShopContext(DbContextOptions<GameShopContext> options) : IdentityDbContext(options)
    {
        // new - перекрытия свойства identity
        public new DbSet<User>? Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<RecommendedGameProduct> RecommendedGameProducts { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<GameProduct> GameProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameProduct>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Cart>()
                .Property(sum => sum.Sum)
                .HasColumnType("decimal(18,2)");
        }
        public DbSet<Genre> Genres {  get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<MinimumSystemRequirement> MinimumSystemRequirements {  get; set; }

        public DbSet<RecommendedSystemRequirement> RecommendedSystemRequirements { get; set; }

        public DbSet<Video> Videos { get; set; }



    }
}

