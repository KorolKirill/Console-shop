using Magaz.Visual_controller;

namespace Magaz
{
    public class ShopManager
    {
        public Shop Shop { get; private set; }
        private bool working;
        private readonly IVisualController _visualController;

        public ShopManager(Shop shop)
        {
            Shop = shop;
            _visualController = new ConsoleController();
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

        private void PickOption()
        {
            var option = _visualController.RequestOption();
            switch (option)
            {
                case OptionType.ProductList:
                    ShowProductList();
                    break;
                case OptionType.BuyStuff:
                    Buy();
                    break;
                case OptionType.History:
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
            var history = Shop.History;
            _visualController.ShowAllHistory(history);
        }
        
         private void Buy()
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
            
            Shop.BeginPurchase();
            bool continueBuying =true;
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

                if (!Shop.TakeProductsFromStock(productData,userAmountOfProduct))
                {
                    _visualController.NotEnoughProduct(productData,userAmountOfProduct);
                    counter++;
                }
                Shop.FinishPurchase();
            }
            var order = Shop.getLastOrder();
            _visualController.FinishPurchasing(order);
        }
        public enum OptionType
        {
            Exit = 0,
            ProductList = 1,
            BuyStuff = 2,
            History = 3,
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
             working = false;
         }
         private void ShowProductList()
         {
             var allProductsData = Shop.GetAllProductData();
             _visualController.ShowProducts(allProductsData);
         }
    }
}