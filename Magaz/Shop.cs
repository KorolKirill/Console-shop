using System.Collections.Generic;

namespace Magaz
{
    public class Shop
    {
        private IProductDao _productDao;
        public History History { get; private set; }
        
        public Shop()
        {
            _productDao = new ProductDao();
            History = new History();
        }

        private Order currentOrder;

        public Order getLastOrder()
        {
            return currentOrder;
        }
        public void BeginPurchase()
        {
            currentOrder = new Order();
        }

        public void FinishPurchase()
        {
            History.Add(currentOrder);
        }

        public int AmountOfProductsInStock()
        {
            return _productDao.GetAllData().Count;
        }

        public List<ProductData> GetAllProductData()
        {
            return _productDao.GetAllData();
        }

        public ProductData FindProductByInformation(ProductInformation productInformation)
        {
            return _productDao.FindByInformation(productInformation);
        }
        
        
        public bool TakeProductsFromStock(ProductData productData, int amount)
        {
            if (amount > productData.Quantity)
            {
                return false;
            }
            _productDao.Take(productData,amount);
            currentOrder.Add(new ProductData(productData.Product,amount));
            return true;
        }
    }
}