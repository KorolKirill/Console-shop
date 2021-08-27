using System.Collections.Generic;

namespace Magaz
{
    public interface IProductDao
    {
        public List<ProductData> GetAllData();
        
        public ProductData FindByInformation(ProductInformation productInformation);
        
        public void Take(int code, int amount);
        public void Take(string name, int amount);

        public List<string> GetHistory();

    }
}