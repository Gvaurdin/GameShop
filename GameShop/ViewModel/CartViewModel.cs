using GameShopModel.Entities;

namespace GameShop.ViewModel
{
    public class CartViewModel
    {
        public required IEnumerable<GameProduct> GameProducts { get; set; }
        public required decimal Sum {  get; init; }
    }
}
