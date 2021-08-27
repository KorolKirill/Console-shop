using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magaz
{
    public class ProductDao : IProductDao
    {
        private readonly DataBase _dataBase;
      
        public ProductDao()
        {
            _dataBase = new DataBase();;
        }

        public List<ProductData> GetAllData()
        {
            return _dataBase.ProductDatas;
            // maybe return Enumerator? 
        }

        public bool CheckIfExistsByName(string name)
        {
            return findByName(name) != null;
        }

        private ProductData findByName(string name)
        {
            return GetAllData()
                .FirstOrDefault(productData
                    => productData.Product.Name.ToLower().Equals(name.ToLower()));
        }

        public bool CheckIfExistsByCode(int code)
        {
            if (FindByCode(code)!=null)
            {
                return true;
            }
            return false;
        }

        private ProductData FindByCode(int code)
        {
            foreach (var productData in GetAllData())
            {
                if (productData.Product.Code.Equals(code))
                {
                    return productData;
                }
            }
            return null;
        }

        private void Take(ProductData productData, int amount)
        {
            if (productData.Quantity == amount)
            {
                // Этот случай если мы не хотим сохранять в списке продуктов, те продукты которые закончились.
                DeleteProduct(productData);
                AddHistory(productData, amount);
            }
            else if (productData.Quantity > amount)
            {
                productData.Quantity -= amount;
                AddHistory(productData,amount);
            }
            else
            {
                throw new NotEnoughProductException();
            }
        }
        
        public void Take(int code, int amount)
        {
            var productData = FindByCode(code);
            Take(productData,amount);
        }
        
        public void Take(string name, int amount)
        {
            var productData = findByName(name);
            Take(productData,amount);
        }
        
        private void DeleteProduct(ProductData productData)
        {
            GetAllData().Remove(productData);
        }
        
        public List<string> GetHistory()
        {
            return _dataBase.History;
        }

        private void AddHistory(ProductData productData, int amount)
        {
            string history =
                $"{_dataBase.History.Count + 1}. name: {productData.Product.Name}, code: {productData.Product.Code}," +
                $" amount: {amount}";
            
            _dataBase.History.Add(history);
        }
    }
}