using System;
using System.Collections.Generic;

namespace Magaz.Visual_controller
{
    public interface IVisualController
    {
        public void ShowMessage(string message);
        public string RequestUserLogin();
        public string RequestUserPassword();
        public void ShowOrders(List<Order> orders);
        public void WelcomeShopMessage();
        public void FailedLogIn();
        public void WelcomeUserMessage(string name);
        public void ShowUserMenu();
        public void ShowGuestMenu();
        public void ShowAdminMenu();
        public void ShowMainMenu();
        public void GoodByeMessage();
        public void ShowProducts(List<ProductData> productDatas);
        public int RequestOption();
        public void WrongOption<T>(T option) where T : struct;
        public void WrongAmountProductBuying(int amount);
        public void NotEnoughProduct(ProductData data, int userWantedAmount);
        public int RequestNumberOfTypeProductsBuying();
        public void WrongNumberOfTypeProductsBuying(int userInput);
        public void WrongProductInformation(ProductInformation productInformation); 
        public void LackOfProductTypeOnStock(int userInput, int productAmountOnStock);
        public bool WarningAddingNewProductType(ProductInformation productInformation);
        public int RequestAmountOfProduct(ProductData data);
        public ShopManager.Action RequestForNextAction();
        public ProductInformation RequestProductInformation();
        public void FinishPurchasing(Order receipt);
        public void LoginIsNotAvailable();
        public  string RequestProductName();
        public ShopManager.CodeSetting RequestCodeSetting();
        public int RequestProductCode();
        public void WarningCodeTaken(int code);
        public int RequestAddingAmount(ProductInformation productInformation);
        public bool AskForDeleteHistory(Product product);
        public void WrongAmount();
    }
}