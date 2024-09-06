using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Entities
{
    public class GameProductGenre
    {
        public int GameProductId { get; set; }
        public GameProduct? GameProduct { get; set; }

        public int GenreId { get; set; }
        public Genre? Genre { get; set; }

        public class GameProductGenreConfiguration : IEntityTypeConfiguration<GameProductGenre>
        {
            public void Configure(EntityTypeBuilder<GameProductGenre> builder)
            {
                builder.HasKey(gpg => new { gpg.GameProductId, gpg.GenreId });
            }
        }
    }
}
