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
        public GameProduct GameProduct { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

    }
}
