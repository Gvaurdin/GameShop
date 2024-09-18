using GameShop.Repository.Interfaces;
using GameShop.ViewModel;
using GameShopModel.Data;
using GameShopModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Repository
{
    public class GameStatusService(GameShopContext gameShopContext, IRepositoryCart repositoryCart) : IGameStatusService
    {
        public async Task<Dictionary<int, GameStatusViewModel>> GetGameStatusesAsync(List<GameProduct> gameProducts, string userId)
        {
            var cartProducts = repositoryCart.GetProducts();
            var libraryGames = await gameShopContext.Carts
                .AsNoTracking()
                .Include(cart => cart.GameProducts)
                .Where(cart => cart.User.Id == userId)
                .SelectMany(cart => cart.GameProducts)
                .ToListAsync();
            return gameProducts.ToDictionary(game => game.Id, game => new GameStatusViewModel
            {
                IsInCart = cartProducts.Any(p => p.Id == game.Id),
                IsInLibrary = libraryGames.Any(p => p.Id == game.Id)
            });

        }
    }
}
