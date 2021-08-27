﻿using System.Collections.Generic;

namespace Magaz.Visual_controller
{
    public interface IVisualController
    {
        public void ShowAllHistory(History history);
        public void WelcomeMessage();
        public void ShowMenu();
        public void GoodByeMessage();

        public void ShowProducts(List<ProductData> productDatas);
        public Shop.OptionType RequestOption();
        public void WrongOption(Shop.OptionType option);

        public void WrongAmountProductBuying(int amount);

        public void NotEnoughProduct(ProductData data, int userWantedAmount);
        public int RequestNumberOfTypeProductsBuying();
        public void WrongNumberOfTypeProductsBuying(int userInput);

        public void WrongProductInformation(ProductInformation productInformation); 
        public void LackOfProductTypeOnStock(int userInput, int productAmountOnStock);

        public int RequestAmountOfProduct(ProductData data);
        public Shop.Action RequestForNextAction();
        public ProductInformation RequestProductType();

        public void FinishPurchasing(Receipt receipt);
        
    }
}