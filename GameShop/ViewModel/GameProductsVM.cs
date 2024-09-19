using GameShopModel.Entities;

namespace GameShop.ViewModel
{
    public class GameProductsVM
    {
        public required IEnumerable<GameProduct> GameProducts { get; set; }

        public required PageViewModel PageViewModel { get; set; }
        public required FilteredGameProductVM FilteredGameProductVM { get; set; }
        public required SortGameProductVM SortGameProductVM { get; set; }

        public Dictionary<int, GameStatusViewModel> GameStatuses { get; set; }
    }
}
