using GameShopModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Repository.Repositories.Interfaces
{
    public interface IRepositoryRecommendedGameProduct
    {
        public Task<List<RecommendedGameProduct>> GetAllRecommendedGameProductsAsync();

        public Task<RecommendedGameProduct> GetRecommendedGameProductAsync(int id);

        public Task AddRecommendedGameProductAsync(RecommendedGameProduct recommendedgameProduct);

        Task RemoveRecommendedGameProductAsync(int id);
        public Task EditGameProductAsync(int id, RecommendedGameProduct recommendedgameProduct);
    }
}
