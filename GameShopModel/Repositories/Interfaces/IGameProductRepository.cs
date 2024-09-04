using GameShopModel.Data;
using GameShopModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Repositories.Interfaces
{
    public interface IGameProductRepository
    {
        public Task<List<GameProduct>> GetAllGameProductsAsync();

        public Task<GameProduct> GetGameProductAsync(int id);

        public Task AddGameProductAsync(GameProduct gameProduct);

        Task RemoveGameProductAsync(int id);
        public Task EditGameProductAsync(int id, GameProduct gameProduct);
    }
}
