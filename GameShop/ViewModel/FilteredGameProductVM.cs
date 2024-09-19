using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShop.ViewModel
{
    public class FilteredGameProductVM(SelectList genres,string selectedGenreGameProduct, string selectedTitleGameProduct)
    {
        public SelectList Genres { get; set; } = genres;
        public string SelectGenreGameProduct { get; set; } = selectedGenreGameProduct;

        public string SelectTitleGameProduct { get; set; } = selectedTitleGameProduct;
    }
}
