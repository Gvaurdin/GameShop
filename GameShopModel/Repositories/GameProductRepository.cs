using GameShopModel.Data;
using GameShopModel.Entities;
using GameShopModel.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Repositories
{
    public class GameProductRepository(GameShopContext gameShopContext): IGameProductRepository
    {
        public async Task<List<GameProduct>> GetAllGameProductsAsync()
        {
            return await gameShopContext.GameProducts.ToListAsync();
        }

        public async Task<GameProduct> GetGameProductAsync(int id)
        {
           return await gameShopContext.GameProducts.FirstAsync(gameProduct => gameProduct.Id == id);
        }

        public async Task AddGameProductAsync(GameProduct gameProduct)
        {
            await gameShopContext.GameProducts.AddAsync(gameProduct);
            await gameShopContext.SaveChangesAsync();
        }

        public async Task EditGameProductAsync(int id,GameProduct gameProduct)
        {
            var editingGameProduct = await gameShopContext.GameProducts.FirstAsync(gameProduct => gameProduct.Id == id);
            editingGameProduct.Title = gameProduct.Title;
            editingGameProduct.Description = gameProduct.Description;
            editingGameProduct.PresentationImageURL = gameProduct.PresentationImageURL;
            editingGameProduct.Price = gameProduct.Price;
            editingGameProduct.ReleaseDate = gameProduct.ReleaseDate;
            editingGameProduct.Genre = gameProduct.Genre;
            editingGameProduct.ImagesUrl = gameProduct.ImagesUrl;

            await gameShopContext.SaveChangesAsync();
        }

        public async Task RemoveGameProductAsync(int id)
        {
            await gameShopContext.GameProducts
                .Where(gameProduct => gameProduct.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
