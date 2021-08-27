using System;
using System.Text;
using Magaz.Visual_controller;

namespace Magaz
{
    public class Shop
    {
        private bool working;
        private readonly IProductDao _productDao;
        private readonly IVisualController _visualController;
        private readonly History _history;
        
        public Shop(IVisualController visualController)
        {
            _productDao = new ProductDao();
            _visualController = visualController;
            _history = new History();
        }

        public void Start()
        {
            _visualController.WelcomeMessage();
            working = true;
            while (working)
            {
                ShowMenu();
            }
        }

        private void ShowMenu()
        {
            _visualController.ShowMenu();
            PickOption();
        }
        
        public enum OptionType
        {
            Exit = 0,
            ProductList = 1,
            BuyStuff = 2,
            History = 3,
        }
        
        private void PickOption()
        {
            var option = _visualController.RequestOption();
            switch (option)
            {
                case OptionType.ProductList :
                    ShowProductList(); 
                    break;
                case OptionType.BuyStuff :
                    Buy();
                    break;
                case OptionType.History :
                    ShowAllHistory();
                    break;
                case OptionType.Exit:
                    Exit();
                    break;
                default:
                    _visualController.WrongOption(option);
                    break;
            }
        }

        private void ShowAllHistory()
        {
            _visualController.ShowAllHistory(_history);
        }
        public enum Action
        {
            TryAgain = 0,
            FinishBuying = 1,
            ViewListOfProducts = 2
        }
        

        private void Buy()
        {
            var userWantedProductsTypeAmount = _visualController.RequestNumberOfTypeProductsBuying();
           
            if (userWantedProductsTypeAmount <= 0)
            {
                _visualController.WrongNumberOfTypeProductsBuying(userWantedProductsTypeAmount);
                return;
            }

            var productsAmountOnStock = _productDao.GetAllData().Count;
            if (userWantedProductsTypeAmount > productsAmountOnStock )
            {
                _visualController.LackOfProductTypeOnStock(userWantedProductsTypeAmount,productsAmountOnStock);
                return;
            }

            Receipt receipt = new Receipt();
            bool continueBuying =true;
            
            for (int counter = 0; counter < userWantedProductsTypeAmount; counter++)
            {
                if (!continueBuying)
                {
                    break;
                }
                
                var userTypeOfProduct = _visualController.RequestProductType();
                var productData = _productDao.FindByInformation(userTypeOfProduct);
                
                if (productData == null)
                {
                    _visualController.WrongProductInformation(userTypeOfProduct);
                    var userNextAction = _visualController.RequestForNextAction();
                    ModerateNextAction(userNextAction,ref continueBuying, ref counter);
                    continue;
                }

                var userAmountOfProduct = _visualController.RequestAmountOfProduct(productData);
                
                if (userAmountOfProduct <= 0)
                {
                    _visualController.WrongAmountProductBuying(userAmountOfProduct);
                    counter++;
                    continue;
                }

                try
                {
                    TakeProductsFromStock(productData,userAmountOfProduct);
                    Order order = new Order(productData.Product, userAmountOfProduct);
                    receipt.Add(order);
                }
                catch (NotEnoughProductException e)
                {
                    _visualController.NotEnoughProduct(productData,userAmountOfProduct);
                    counter++;
                    continue;
                }
            }
            
            _visualController.FinishPurchasing(receipt);
            _history.Add(receipt);
            
        }

        //Этот метод мне не очень нравится в этом классе.
        private void TakeProductsFromStock(ProductData productData, int amount)
        {
            if (amount > productData.Quantity)
            {
                throw new NotEnoughProductException();
            }

            productData.Quantity -= amount;
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
            working = false;
        }

        private void ShowProductList()
        {
            var allProductsData = _productDao.GetAllData();
            _visualController.ShowProducts(allProductsData);
        }
    }
}