using GameShopModel.Entities;

namespace GameShop.ViewModel
{
    public class GameIndexViewModel
    {
        public List<GameProduct> Games { get; set; }
        public Dictionary<int, GameStatusViewModel> GameStatuses { get; set; }
    }
}
