using GameShop.Core;
using GameShopModel.Entities;

namespace GameShop.ViewModel
{
    public class GameProductsVM
    {
        public required PaginationList<GameProduct> GameProducts { get; set; }
        public required FilteredGameProductVM FilteredGameProductVM { get; set; }
        public required SortGameProductVM SortGameProductVM { get; set; }

        public Dictionary<int, GameStatusViewModel> GameStatuses { get; set; }
    }
}
