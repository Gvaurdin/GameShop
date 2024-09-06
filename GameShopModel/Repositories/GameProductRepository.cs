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

        public async Task EditGameProductAsync(int id, GameProduct gameProduct) =>
        await gameShopContext.GameProducts
            .Where(editingGameProduct => editingGameProduct.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(
                    editingGameProduct => editingGameProduct.PresentationImageURL, gameProduct => gameProduct.PresentationImageURL)

                .SetProperty(
                    editingGameProduct => editingGameProduct.Title, gameProduct => gameProduct.Title)
                .SetProperty(
                    editingGameProduct => editingGameProduct.Description, gameProduct => gameProduct.Description)
                .SetProperty(
                    editingGameProduct => editingGameProduct.Price, gameProduct => gameProduct.Price)
                .SetProperty(
                    editingGameProduct => editingGameProduct.ReleaseDate, gameProduct => gameProduct.ReleaseDate)
                .SetProperty(
                    editingGameProduct => editingGameProduct.ImagesUrl, gameProduct => gameProduct.ImagesUrl)
                .SetProperty(
                editingGameProduct => editingGameProduct.MinimumSystemRequirement, gameProduct => gameProduct.MinimumSystemRequirement)
                .SetProperty(
                editingGameProduct => editingGameProduct.RecommendedSystemRequirement, gameProduct => gameProduct.RecommendedSystemRequirement)
            );

        public async Task RemoveGameProductAsync(int id)
        {
            await gameShopContext.GameProducts
                .Where(gameProduct => gameProduct.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
