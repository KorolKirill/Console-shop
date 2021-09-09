using System;
using System.Threading;
using Magaz.Users;
using Magaz.Visual_controller;

namespace Magaz
{
    public class ShopManager
    {
        private Shop Shop { get;  set; }
        private bool _working;
        private readonly IVisualController _visualController;

        private IUser _user;
        public ShopManager(Shop shop)
        {
            Shop = shop;
            _visualController = new ConsoleController();
        }

        public void Start()
        {
            _visualController.WelcomeShopMessage();
            _working = true;
            while (_working)
            {
                ShowMenu();
            }
        }

        private void ShowMenu()
        {
            if (_user is Admin)
            {
                AdminMenu();
            }

            if (_user is User)
            {
                UserMenu();
            }

            if (_user is Guest)
            {
                GuestMenu();
            }

            if (_user == null)
            {
                AuthMenu();
            }
        }

        private void AuthMenu()
        {
            _visualController.ShowMainMenu();
            PickAuthOption();
        }

        private void PickAuthOption()
        {
            var option = (AuthUserOption) _visualController.RequestOption();
            switch (option)
            {
                case AuthUserOption.LoginAsUser:
                    LoginToAccount();
                    break;
                case AuthUserOption.LoginAsGuest:
                    LoginAsGuest();
                    break;
                case AuthUserOption.Register:
                    RegistrateUser();
                    break;
                case AuthUserOption.Exit:
                    Exit();
                    break;
                default:
                    _visualController.WrongOption(option);
                    break;
            }
        }

        private void LoginAsGuest()
        {
            _user = new Guest();
        }

        private void LoginToAccount()
        {
            var login = _visualController.RequestUserLogin();
            var password = _visualController.RequestUserPassword(); 
            
            var user = Shop.LogIntoAccount(login, password);

            if (user == null)
            {
                _visualController.FailedLogIn();
                return;
            }

            this._user = (IUser) user;
            _visualController.WelcomeUserMessage(user.Login);
            
        }

        private void RegistrateUser()
        {
            var login = _visualController.RequestUserLogin();
            var password = _visualController.RequestUserPassword();
            var newUser = Shop.RegistrateNewUser(login, password);
            
            if (newUser == null)
            {
                _visualController.LoginIsNotAvailable();
                return;
            }

            _user = (IUser) newUser;
            _visualController.WelcomeUserMessage(newUser.Login);
        }
        
        private void GuestMenu()
        {
            _visualController.ShowGuestMenu();
            PickGuestOption();
        }

        private void UserMenu()
        {
            _visualController.ShowUserMenu();
            PickUserOption();
        }

        private void AdminMenu()
        {
            _visualController.ShowAdminMenu();
            PickAdminOption();
        }

        private void PickAdminOption()
        {
            var option = (OptionType) _visualController.RequestOption();
            
            if (_user is not Admin user)
            {
                return;
            }

            switch (option)
            {
                case OptionType.ProductList:
                    ShowProductList();
                    break;
                case OptionType.BuyProduct:
                    Sell(user);
                    break;
                case OptionType.AllHistory:
                    ShowAllHistory();
                    break;
                case OptionType.UserHistory:
                    ShowUserHistory(user);
                    break;
                case OptionType.AddProduct:
                    AddProduct();
                    break;
                case OptionType.RemoveProduct :
                    RemoveProduct();
                    break;
                case OptionType.Exit:
                    ExitFromAccount();
                    break;
                default:
                    _visualController.WrongOption(option);
                    break;
            }
        }

        private void RemoveProduct()
        {
            var productInformation = _visualController.RequestProductInformation();
            var productData = Shop.FindProductByInformation(productInformation);
            
            if (productData is null)
            {
                _visualController.WrongProductInformation(productInformation);
                return;
            }

            var amount =  _visualController.RequestAmountOfProduct(productData);

            if (amount < 0)
            {
                _visualController.WrongNumberOfTypeProductsBuying(amount);
                return;
            }
            
            if (!Shop.TakeProductsFromStock(productData,amount))
            {
                _visualController.WrongAmount();
                return;
            }

            if (productData.Quantity == 0)
            {
                var deleteHistory = _visualController.AskForDeleteHistory(productData.Product);
                if (deleteHistory) {
                
                    Shop.DeleteHistory(productData.Product);
                }  
            }
        
        }
        
        private void AddProduct()
        {
            var productName = _visualController.RequestProductName();
            var productInformation = new ProductInformation(productName);
            var productData = Shop.FindProductByInformation(productInformation);
            
            int amount;
            
            //Если не нашли продукт в шопе
            if (productData is null)
            {
                var createNewProduct =  _visualController.WarningAddingNewProductType(productInformation);

                if (!createNewProduct)
                {
                    return;
                }
                
                SetProductCode(productInformation);
                var product = ProductConverter.ConvertToProduct(productInformation);
                amount = _visualController.RequestAddingAmount(productInformation);
                Shop.AddNewProduct(product,amount);
                return;
            }

            amount = _visualController.RequestAddingAmount(productInformation);
            Shop.AddProduct(productData,amount);
            
        }

        public enum CodeSetting
        {
            Automatically = 0,
            Manually = 1,
        }

        private void SetProductCode(ProductInformation productInformation)
        {
            var codeSetting = _visualController.RequestCodeSetting();
            switch (codeSetting)
            {
                case CodeSetting.Automatically:
                    productInformation.Code = Shop.GetLastProductCode();
                    break;
                case CodeSetting.Manually:
                    productInformation.Code = SetNewProductCode();
                    break;
            }
        }

        private int SetNewProductCode()
        {
            var code =  _visualController.RequestProductCode();
            
            if (Shop.FindProductByInformation(new ProductInformation(code)) is null)
            {
                return code;
            }
            
            _visualController.WarningCodeTaken(code);
            return SetNewProductCode();

        }
        
        private void ShowUserHistory(IAuthorisedUser user)
        {
            var orders = Shop.GetUserOrders(user);
            _visualController.ShowOrders(orders);
        }

        private void ExitFromAccount()
        {
            _user = null;
        }
        
        private void PickGuestOption()
        {
            var option = (OptionType) _visualController.RequestOption();
            switch (option)
            {
                case OptionType.ProductList:
                    ShowProductList();
                    break;
                case OptionType.Exit:
                    ExitFromAccount();
                    break;
                default:
                    _visualController.WrongOption(option);
                    break;
            }
        }
        
        private void PickUserOption()
        {
            var option = (OptionType) _visualController.RequestOption();
            var user = (User) this._user;
            
            switch (option)
            {
                case OptionType.ProductList:
                    ShowProductList();
                    break;
                case OptionType.BuyProduct:
                    Sell(user);
                    break;
                case OptionType.UserHistory:
                    ShowUserHistory(user);
                    break;
                case OptionType.Exit:
                    ExitFromAccount();
                    break;
                default:
                    _visualController.WrongOption(option);
                    break;
            }
        }
        
        
        private void ShowAllHistory()
        {
            _visualController.ShowOrders(Shop.GetAllOrders());
        }
        
         private void Sell(IAuthorisedUser user)
        {
            var userWantedProductsTypeAmount = _visualController.RequestNumberOfTypeProductsBuying();
           
            if (userWantedProductsTypeAmount <= 0)
            {
                _visualController.WrongNumberOfTypeProductsBuying(userWantedProductsTypeAmount);
                return;
            }

            var productsAmountOnStock = Shop.AmountOfProductsInStock();
            if (userWantedProductsTypeAmount > productsAmountOnStock )
            {
                _visualController.LackOfProductTypeOnStock(userWantedProductsTypeAmount,productsAmountOnStock);
                return;
            }
            
            var order = new Order( user);
            var continueBuying = true;
            for (int counter = 0; counter < userWantedProductsTypeAmount; counter++)
            {
                if (!continueBuying)
                {
                    break;
                }
                
                var userTypeOfProduct = _visualController.RequestProductInformation();
                var productData = Shop.FindProductByInformation(userTypeOfProduct);
                
                if (productData == null)
                {
                    _visualController.WrongProductInformation(userTypeOfProduct);
                    var userNextAction = _visualController.RequestForNextAction();
                    ModerateNextAction(userNextAction, ref continueBuying, ref counter);
                    continue;
                }

                var userAmountOfProduct = _visualController.RequestAmountOfProduct(productData);
                
                if (userAmountOfProduct <= 0)
                {
                    _visualController.WrongAmountProductBuying(userAmountOfProduct);
                    counter++;
                    continue;
                }

                if (Shop.TakeProductsFromStock(productData,userAmountOfProduct))
                {
                    order.Add(new ProductData(productData.Product, userAmountOfProduct));
                }
                else
                {
                    _visualController.NotEnoughProduct(productData,userAmountOfProduct);
                    counter++;
                }
            }
            
            Shop.AddOrder(order);
            _visualController.FinishPurchasing(order);
        }
         public enum AuthUserOption
         {
             LoginAsGuest = 3,
             LoginAsUser = 1,
             Register = 2,
             Exit = 0,
         }
        private enum OptionType
        {
            Exit = 0,
            ProductList = 1,
            BuyProduct = 2,
            UserHistory = 3,
            AllHistory = 4,
            AddProduct = 5,
            RemoveProduct =6,
        }
        
        public enum Action
        {
            TryAgain = 0,
            FinishBuying = 1,
            ViewListOfProducts = 2
        }
        
         private void ModerateNextAction(Action userAction, ref bool continueBuying, ref int counter)
         {
             switch (userAction)
             {
                 case Action.TryAgain:
                     counter--;
                     break;
                 case Action.FinishBuying:
                     continueBuying = false;
                     break;
                 case Action.ViewListOfProducts:
                     ShowProductList();
                     counter--;
                     break;
                 default:
                     continueBuying = false;
                     break;
             }
         }
         
         private void Exit()
         {
             _visualController.GoodByeMessage();
             _working = false;
         }
         private void ShowProductList()
         {
             var allProductsData = Shop.GetAllProductData();
             _visualController.ShowProducts(allProductsData);
         }
    }
}