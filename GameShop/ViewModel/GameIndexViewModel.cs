using GameShopModel.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShop.ViewModel
{
    public class GameIndexViewModel
    {
        public string? TitleSearchString { get; set; }
        public string? GameGenre { get; set; }
        public SelectList? GameGenres { get; set; }
        public required List<GameProduct> Games { get; set; }
        public Dictionary<int, GameStatusViewModel> GameStatuses { get; set; }

    }
}
