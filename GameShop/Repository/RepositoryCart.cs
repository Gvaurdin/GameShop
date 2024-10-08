﻿using GameShop.Repository.Interfaces;
using GameShopModel.Entities;

namespace GameShop.Repository
{
    public class RepositoryCart:IRepositoryCart
    {
        private readonly List<GameProduct> gameProducts = [];

        public decimal GetSum => gameProducts.Sum(gameProduct => gameProduct.Price);
        public void Add(GameProduct gameProduct)
        {
            gameProducts.Add(gameProduct);
        }

        public void Delete(int id)
        {
            var gameProduct = gameProducts.First(x => x.Id == id);
            gameProducts.Remove(gameProduct);
        }

        public IEnumerable<GameProduct> GetProducts() => 
            gameProducts;

        public void Clear() =>
            gameProducts.Clear();
    }
}
