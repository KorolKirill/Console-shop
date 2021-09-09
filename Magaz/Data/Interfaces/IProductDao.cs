using System.Collections.Generic;

namespace Magaz
{
    public interface IProductDao
    {
        public List<ProductData> GetAllData();
        
        public ProductData FindByInformation(ProductInformation productInformation);

        public void Take(ProductData productData, int amount);
        
        public void Put(ProductData productData, int amount);
        
        public void Take(ProductInformation productInformation, int amount);

        public void Put(ProductData data);

    }
}