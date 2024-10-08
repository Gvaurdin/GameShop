﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required List<GameProduct> GameProducts { get; set; }
    }
}
