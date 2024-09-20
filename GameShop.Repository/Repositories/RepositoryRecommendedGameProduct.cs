using GameShop.Repository.Repositories.Interfaces;
using GameShopModel.Data;
using GameShopModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Repository.Repositories
{
    public class RepositoryRecommendedGameProduct(GameShopContext gameShopContext) : IRepositoryRecommendedGameProduct
    {
        public async Task AddRecommendedGameProductAsync(RecommendedGameProduct recommendedgameProduct)
        {
            await gameShopContext.RecommendedGameProducts.AddAsync(recommendedgameProduct);
            await gameShopContext.SaveChangesAsync();
        }


        public async Task EditGameProductAsync(int id, RecommendedGameProduct recommendedGameProduct)
        {
            await gameShopContext.RecommendedGameProducts
                .Where(editGameProduct => editGameProduct.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(editingGameProduct => editingGameProduct.ExpertName, _ => recommendedGameProduct.ExpertName)
                    .SetProperty(editingGameProduct => editingGameProduct.ExpertSurname, _ => recommendedGameProduct.ExpertSurname)
                    .SetProperty(editingGameProduct => editingGameProduct.GameProductId, _ => recommendedGameProduct.GameProductId)); // Обновляем по ID
        }

        public async Task<List<RecommendedGameProduct>> GetAllRecommendedGameProductsAsync()
        {
          return await gameShopContext.RecommendedGameProducts
                .Include(rgame => rgame.GameProduct)
                .ToListAsync();
        }

        public async Task<RecommendedGameProduct> GetRecommendedGameProductAsync(int id) =>
            await gameShopContext.RecommendedGameProducts
            .Include (rgame => rgame.GameProduct)
            .SingleAsync(rgame => rgame.Id == id);

        public async Task RemoveRecommendedGameProductAsync(int id) =>
            await gameShopContext.RecommendedGameProducts
            .Where(gameProduct => gameProduct.Id == id)
            .ExecuteDeleteAsync();
    }
}
