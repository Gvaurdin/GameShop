using GameShopModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Repositories.Interfaces
{
    public interface IImagesUrlRepository
    {
        public Task<List<ImageUrl>> GetAllImageUrlsAsync();

        public Task<ImageUrl> GetImageUrlAsync(int id);

        public Task AddImageUrlAsync(ImageUrl imageUrl);

        Task RemoveImageUrlAsync(int id);
        public Task EditImageUrlAsync(int id, ImageUrl imageUrl);
    }
}
