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
        public Task<List<GameProduct>> GetAllAsync();

        public Task<GameProduct> GetByIdAsync(int id);

        public Task AddAsync(GameProduct gameProduct);

        Task RemoveAsync(int id);
        public Task EditAsync(int id, GameProduct gameProduct);

        public Task<GameProduct> GetByTitleAsync(string title);
    }
}
