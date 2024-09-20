using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Entities
{
    public class RecommendedGameProduct
    {
        public int Id { get; set; }
        public required string ExpertName { get; set; }
        public required string ExpertSurname { get; set; }
        public int GameProductId { get; set; }
        public GameProduct? GameProduct { get; set; }
    }
}
