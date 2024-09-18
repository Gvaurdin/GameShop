using GameShopModel.Entities;

namespace GameShop.ViewModel
{
    public class GameDetailsViewModel
    {
        public GameProduct GameProduct { get; set; }
        public bool IsInCart { get; set; }
        public bool IsInLibrary { get; set; }
        public bool IsInWishlist { get; set; }
    }
}
