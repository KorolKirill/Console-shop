using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;

namespace Magaz.Visual_controller
{ 
    public class ConsoleController : IVisualController
    {
        public void ShowAllHistory(History history)
        {
            if (history.Orders.Count == 0)
            {
                Console.WriteLine("We don`t have anything in history right now");
                return;
            }

            int counter = 1;
            foreach (var order in history.Orders)
            {
                Console.WriteLine("____________________________");
                Console.WriteLine($"Receipt-{counter++}");
                foreach (var productType in order.ProductDataList)
                {
                    Console.WriteLine($"{productType.Product.Name} - {productType.Quantity.ToString()}");
                }

                Console.WriteLine($"Date: {order.GetDataOfCreation().ToString()}");
                Console.WriteLine("----------------------------");
            }
            
            
        }

        public void WelcomeMessage()
        {
            string welcomeMessage = "Welcome in our super shop! ;D";
            Console.WriteLine(welcomeMessage);
        }

        public void ShowMenu()
        {
            string menu =
                "\n Pick your option:" +
                "\n 1. Show list of all products" +
                "\n 2. Buy some stuff" +
                "\n 3. Show buying history " +
                "\n 0. Exit";
            Console.Write(menu);
        }

        public void GoodByeMessage()
        {
            Console.WriteLine("See you!");
        }

        public void ShowProducts(List<ProductData> productDatas)
        {
            string message = "Here is a list of all products in stock!";
            Console.WriteLine("\n" + message);
            
            int counter = 1;
            foreach (var productData in productDatas)
            {
                if (productData.Quantity == 0)
                {
                    continue;
                }
                Console.WriteLine(
                    $"{counter++}. " +
                    $"Name: {productData.Product.Name}, " +
                    $"code: {productData.Product.Code}, " +
                    $"quantity: {productData.Quantity} "
                );
            }
        }

        public ShopManager.OptionType RequestOption()
        {
            string message = "\nPick an option";
            Console.WriteLine(message);
            var option = ReadANumberFromConsole();
            return (ShopManager.OptionType) option;
        }

        public void WrongOption(ShopManager.OptionType option)
        {
            Console.WriteLine($"Sorry, but you have picked option that does not exist! You picked: {option.ToString()}");
        }
        
        
        public void WrongAmountProductBuying(int amount)
        {
            Console.WriteLine("You entered an amount that we cannot sell you");
            Console.WriteLine($"Your input: {amount.ToString()}");
            Console.WriteLine("You will need to pick an product again.");
            
            Thread.Sleep(200);
        }

        public void NotEnoughProduct(ProductData data, int userWantedAmount)
        {
            Console.WriteLine("We are sorry, but we have not enough of this product");
            Console.WriteLine("You wanted to buy:");
            Console.WriteLine($"Name: {data.Product.Name} - {userWantedAmount.ToString()} ");
            Console.WriteLine($"But we have only {data.Quantity} in out shop");
            Console.WriteLine("You will need to pick anything else again.");
            
            Thread.Sleep(200);
            
        }

        public int RequestNumberOfTypeProductsBuying()
        {
            Console.WriteLine("\nHow many products do you want to buy? Pls, enter a number.");
            var amount = ReadANumberFromConsole();
            return amount;
        }

        public void WrongNumberOfTypeProductsBuying(int number)
        {
            Console.WriteLine("You cannot buy zero or less products");
            Console.WriteLine($"Your input: {number.ToString()}");
        }

        public void WrongProductInformation(ProductInformation productInformation)
        {
            Console.WriteLine("Sorry, but we couldn`t found any products on your request");
            Console.WriteLine("You have probably wrote code or name that doesn`t exists");
            Console.Write($"Your input was:");
            if (productInformation.Name ==null)
            {
                Console.WriteLine($" code: {productInformation.Code.ToString()}");
            }
            else
            {
                Console.WriteLine($" name: {productInformation.Name}");
            }
            
        }

        public void LackOfProductTypeOnStock(int userInput, int productAmountOnStock)
        {
            Console.WriteLine("Sorry but we don`t have so much products in our shop");
            Console.WriteLine($"We have only {productAmountOnStock.ToString()} products right now");
            Console.WriteLine($"But you tried to ordered {productAmountOnStock.ToString()}");
        }

        public int RequestAmountOfProduct(ProductData data)
        {
            Console.WriteLine("Okey, we found a product that match your request.");
            Console.WriteLine($"Your Product:{data.Product.Name}, on stock we have {data.Quantity} of it.");
            Console.WriteLine("How many of it do you want? Enter a number.");
            return ReadANumberFromConsole();
        }

        public ShopManager.Action RequestForNextAction()
        {
            Console.WriteLine("\nWhat do you want to do now?");
            Console.WriteLine("You can finish you purchase : enter - 1");
            Console.WriteLine("You can try last operation again: enter - 0");
            Console.WriteLine("If you forgot out list of product, you can get it: enter - 2");
            Console.WriteLine("if you enter wrong option we will automatically finish your purchase");
           
            return (ShopManager.Action) ReadANumberFromConsole();
        }


        private enum TypeOfSearch
        {
            ByName = 1,
            ByCode = 2,
        }
        
        public ProductInformation RequestProductInformation()
        {
            Console.WriteLine("What product do you want to buy? You can pick a product by it`s name or code ");
            Console.WriteLine("Enter it`s code or name");
            
            string input = Console.ReadLine();
            if (int.TryParse(input,out var code))
            {
                return new ProductInformation(code);
            }
            //by name
            return new ProductInformation(input);
            
        }

        public void FinishPurchasing(Order order)
        {
            Console.WriteLine("\nThank you for your visit!.");
            if (order.ProductDataList.Count!=0)
            {
                Console.WriteLine("Here is your Receipt:");
                Console.WriteLine("*************************");
                foreach (var productData in order.ProductDataList)
                {
                    Console.WriteLine($"{productData.Product.Name} - {productData.Quantity.ToString()}");
                }
                Console.WriteLine("*************************");
            }
        }


        private int ReadANumberFromConsole()
        {
            string input = Console.ReadLine();
            if (! int.TryParse(input, out var number))
            {
                WarningEnteredNotANumber(input);
                return ReadANumberFromConsole();
            }
            return number;
        }
        private void WarningEnteredNotANumber(string userInput)
        {
            Console.WriteLine($"Warning! You Entered not a number. Pls try again. Your input: {userInput}");
            Console.WriteLine("Write number again:");
        }
    }
}