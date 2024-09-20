using GameShop.ViewModel;
using GameShopModel.Entities;

namespace GameShop.Repository.Interfaces
{
    public interface IGameStatusService
    {
        Task<Dictionary<int,GameStatusViewModel>> GetGameStatusesAsync(IQueryable<GameProduct> gameProducts, string userId);
    }
}
