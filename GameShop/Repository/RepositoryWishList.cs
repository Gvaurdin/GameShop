using GameShop.Repository.Interfaces;
using GameShopModel.Data;
using GameShopModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Repository
{
    public class RepositoryWishList(GameShopContext gameShopContext) : IRepositoryWishList
    {
        public async Task AddAsync(User user, GameProduct gameProduct)
        {
            var newWishList = new WishList
            {
                GameProduct = gameProduct,
                User = user
            };

            await gameShopContext.WishLists.AddAsync(newWishList);
            await gameShopContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string userId, int gameProductId)
        {
            await gameShopContext.WishLists
                .Include(wishlist => wishlist.User)
                .Include(wishlist => wishlist.GameProduct)
                .Where(wishlist => wishlist.User.Id == userId && wishlist.GameProduct.Id == gameProductId)
                .ExecuteDeleteAsync();
            await gameShopContext.SaveChangesAsync();
        }
    }
}
