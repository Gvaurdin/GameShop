using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShop.ViewModel
{
    public class RecommendedGameProductVM
    {
        public string ExpertName { get; set; }
        public string ExpertSurname { get; set; }

        public string SelectedGameProduct { get; set; }
        public string? SearchGameProduct { get; set; }
        public SelectList? GameProducts { get; set; }
    }
}
