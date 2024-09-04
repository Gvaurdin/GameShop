using GameShopModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        public Task<List<Genre>> GetAllGenresAsync();

        public Task<Genre> GetGenreAsync(int id);

        public Task AddGenreAsync(Genre genre);

        Task RemoveGenreAsync(int id);
        public Task EditGenreAsync(int id, Genre genre);
    }
}
