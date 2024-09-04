using GameShopModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Data
{
    public class GameShopContext(DbContextOptions<GameShopContext> options) : DbContext(options)
    {
        public DbSet<GameProduct> GameProducts {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameProduct>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Genre> Genres {  get; set; }
        public DbSet<ImageUrl> Images { get; set; }


    }
}

