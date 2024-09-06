using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Entities
{
    public class GameProduct
    {
        public int Id { get; set; }
        public required string PresentationImageURL { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public required List<ImageUrl> ImagesUrl { get; set; }
        public required List<Genre>? Genres { get; set; }
        public required MinimumSystemRequirement MinimumSystemRequirement { get; set; }
        public required RecommendedSystemRequirement? RecommendedSystemRequirement { get; set; }
    }
}
