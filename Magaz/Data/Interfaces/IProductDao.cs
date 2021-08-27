using System.Collections.Generic;

namespace Magaz
{
    public interface IProductDao
    {
        public List<ProductData> GetAllData();
        
        public ProductData FindByInformation(ProductInformation productInformation);
        
    }
}