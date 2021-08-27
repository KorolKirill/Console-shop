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

        public ProductData FindByInformation(ProductInformation productInformation)
        {
            return GetAllData().FirstOrDefault(x => x.Product.Equals(productInformation));
        }

        public void Take(ProductData productData, int amount)
        {
            productData.Quantity -= amount;
        }

        public void Take(ProductInformation productInformation, int amount)
        {
            
            var productData = FindByInformation(productInformation);
            Take(productData,amount);
            
        }
    }
}