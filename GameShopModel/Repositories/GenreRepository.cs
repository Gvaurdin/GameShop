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
    public class GenreRepository(GameShopContext gameShopContext) : IGenreRepository
    {
        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await gameShopContext.Genres.ToListAsync();
        }

        public async Task<Genre> GetGenreAsync(int id)
        {
            return await gameShopContext.Genres.FirstAsync(genre => genre.Id == id);
        }

        public async Task AddGenreAsync(Genre genre)
        {
            await gameShopContext.Genres.AddAsync(genre);
            await gameShopContext.SaveChangesAsync();
        }

        public async Task EditGenreAsync(int id, Genre genre)
        {
            var editingGenre = await gameShopContext.Genres.FirstAsync(genre => genre.Id == id);
            editingGenre.Title = genre.Title;

            await gameShopContext.SaveChangesAsync();
        }

        public async Task RemoveGenreAsync(int id)
        {
            await gameShopContext.Genres
                .Where(genre => genre.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
