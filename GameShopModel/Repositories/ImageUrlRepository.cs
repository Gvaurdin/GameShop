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
    public class ImageUrlRepository(GameShopContext gameShopContext) : IImagesUrlRepository
    {
        public async Task<List<ImageUrl>> GetAllImageUrlsAsync()
        {
            return await gameShopContext.Images.ToListAsync();
        }

        public async Task<ImageUrl> GetImageUrlAsync(int id)
        {
            return await gameShopContext.Images.FirstAsync(image => image.Id == id);
        }

        public async Task AddImageUrlAsync(ImageUrl imageUrl)
        {
            await gameShopContext.Images.AddAsync(imageUrl);
            await gameShopContext.SaveChangesAsync();
        }

        public async Task EditImageUrlAsync(int id, ImageUrl imageUrl)
        {
            var editingImageUrl = await gameShopContext.Images.FirstAsync(image => image.Id == id);
            editingImageUrl.URL = imageUrl.URL;

            await gameShopContext.SaveChangesAsync();
        }

        public async Task RemoveImageUrlAsync(int id)
        {
            await gameShopContext.Images
                .Where(image => image.Id == id)
                .ExecuteDeleteAsync();
        }

    }
}
