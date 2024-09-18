using GameShop.ViewModel;
using GameShopModel.Entities;

namespace GameShop.Repository.Interfaces
{
    public interface IGameStatusService
    {
        Task<Dictionary<int,GameStatusViewModel>> GetGameStatusesAsync(List<GameProduct> gameProducts, string userId);
    }
}
