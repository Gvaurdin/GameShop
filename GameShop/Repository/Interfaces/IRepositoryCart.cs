using GameShopModel.Entities;

namespace GameShop.Repository.Interfaces
{
    public interface IRepositoryCart
    {
        decimal GetSum { get; }
        public IEnumerable<GameProduct> GetProducts();

        void Add(GameProduct product);
        void Delete(int id);
        void Clear();
    }
}
