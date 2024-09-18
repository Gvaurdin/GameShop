using GameShopModel.Entities;

namespace GameShop.Repository.Interfaces
{
    public interface IRepositoryWishList
    {
        Task AddAsync(User user, GameProduct gameProduct);

        Task DeleteAsync(string userId, int gameProductId);
    }
}
