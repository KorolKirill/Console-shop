using System.Collections.Generic;
using Magaz.Users;

namespace Magaz
{
    public class Shop
    {
        private IProductDao _productDao;
        private IUserDao _userDao;
        public History History {  get; private set; }
        // redo get - > private get 
        public Shop()
        {
            _userDao = new UserDao();
            _productDao = new ProductDao();
            History = new History();
        }

        public List<Order> GetAllOrders()
        {
            return History.Orders;
        }
        public List<Order> GetUserOrders(IAuthorisedUser user)
        {
            return History.FindUserOrders(user);
        }
        public void AddOrder(Order order)
        {
            History.Add(order);
        }
        public IAuthorisedUser LogIntoAccount(string login, string password)
        {
            var user =  _userDao.FindAccount(login, password);
            return user;
        }

        public IAuthorisedUser RegistrateNewUser(string login, string password)
        {
            var newUser =  _userDao.Registrate(login, password);
            //returns null if couldn`t registrate.
            return newUser;
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

        public int GetLastProductCode()
        {
            var productsData = _productDao.GetAllData();
            var lastCode = 1;
            foreach (var productData in productsData)
            {
                if (productData.Product.Code > lastCode)
                {
                    lastCode = productData.Product.Code;
                }
            }

            return ++lastCode;
        }
        
        public bool TakeProductsFromStock(ProductData productData, int amount)
        {
            if (amount > productData.Quantity)
            {
                return false;
            }
            _productDao.Take(productData,amount);
            return true;
        }

        public void AddNewProduct(Product product, int amount = 0)
        {
            _productDao.Put(new ProductData(product,amount));
        }

        public void AddProduct(ProductData data, int amount)
        {
            _productDao.Put(data,amount);
        }

        public void DeleteHistory(Product product)
        {
            History.DeleteAllPresence(product);
        }
    }
}