using GameShopModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.ViewModel
{
    public class GameProductDetailsViewModel
    {
        public GameProduct GameProduct { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
