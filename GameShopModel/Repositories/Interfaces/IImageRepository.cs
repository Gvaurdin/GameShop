using GameShopModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Repositories.Interfaces
{
    public interface IImageRepository
    {
        public Task<List<Image>> GetAllImageUrlsAsync();

        public Task<Image> GetImageUrlAsync(int id);

        public Task AddImageUrlAsync(Image imageUrl);

        Task RemoveImageUrlAsync(int id);
        public Task EditImageUrlAsync(int id, Image imageUrl);
    }
}
