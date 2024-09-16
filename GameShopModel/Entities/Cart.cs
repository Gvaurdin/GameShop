using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public required decimal Sum { get; set; }
        public required DateTime DatePurchase { get; set; }
        public required User User { get; set; }
        public required List<GameProduct> GameProducts { get; set; }
    }
}
