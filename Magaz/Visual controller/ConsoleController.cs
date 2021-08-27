using System;
using System.Threading;

namespace Magaz.Visual_controller
{ 
    public class ConsoleController : IVisualController
    {
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

        public Shop.OptionType RequestOption()
        {
            string message = "\nPick an option";
            Console.WriteLine(message);
            var option = ReadANumberFromConsole();
            return (Shop.OptionType) option;
        }

        public void WrongOption(Shop.OptionType option)
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

        public Shop.Action RequestForNextAction()
        {
            Console.WriteLine("What do you want to do now?");
            Console.WriteLine("You can finish you purchase : enter - 1");
            Console.WriteLine("You can try last operation again: enter - 0");
            Console.WriteLine("If you forgot out list of product, you can get it: enter - 2");
            Console.WriteLine("if you enter wrong option we will automatically finish your purchase");
           
            return (Shop.Action) ReadANumberFromConsole();
        }


        private enum TypeOfSearch
        {
            ByName = 1,
            ByCode = 2,
        }
        
        public ProductInformation RequestProductType()
        {
            Console.WriteLine("What product do you want to buy? You can pick a product by it`s name or code ");
            Console.WriteLine("Type: 1 - to search by name, 2- to code ");
            var userWantedTypeOfSearch = (TypeOfSearch) ReadANumberFromConsole();
            ProductInformation productInfo;
            switch (userWantedTypeOfSearch)
            {
                case TypeOfSearch.ByName:
                    string name = Console.ReadLine();
                    return productInfo = new ProductInformation(name);
                    break;
                case TypeOfSearch.ByCode:
                    int code = ReadANumberFromConsole();
                    return productInfo = new ProductInformation(code);
                    break;
                default:
                    Console.WriteLine("Sorry, buy you trying to use search method that doesn`t exist");
                    Console.WriteLine($"You entered {userWantedTypeOfSearch.ToString()}, but was expected: 1 or 2");
                    Console.WriteLine("Pls, try again");
                    return RequestProductType();
                    break;
            }

            if userInput 
            if (int.TryParse(userInput, out var productCode))
            {
                if (_productDao.CheckIfExistsByCode(productCode))
                {
                    int amount = AskForQuantity();
                    try
                    {
                        _productDao.Take(productCode,amount);
                        string purchase = $"{i + 1}. Code: {productCode}, amount: {amount}\n";
                        purshaseList.Append(purchase);
                    }
                    catch (NotEnoughProductException e)
                    {
                        Console.WriteLine("Sorry, but you want more than we have in our shop. Chose something again");
                        i--;
                    }
                }
                else
                {
                    Console.WriteLine("You wrote code of product that doesn`t exist. Try again!");
                    i--;
                }
            }
            else if (_productDao.CheckIfExistsByName(userInput))
            {
                int amount = AskForQuantity();
                try
                {
                    _productDao.Take(userInput,amount);
                    string purchase = $"{i + 1}. Name: {userInput}, amount: {amount}\n";
                    purshaseList.Append(purchase);
                }
                catch (NotEnoughProductException e)
                {
                    Console.WriteLine("Sorry, but you want more than we have in our shop. Chose something again");
                    i--;
                }
            }
            else
            {
                Console.WriteLine("You entered wrong name. Try again");
                i--;
            }
        }

        public void FinishPurchasing(Receipt receipt)
        {
            Console.WriteLine("Thank you for your visit!.");
            if (receipt.OrderList.Count!=0)
            {
                Console.WriteLine("Here is your Receipt:");
                Console.WriteLine("*************************");
                foreach (var order in receipt.OrderList)
                {
                    Console.WriteLine($"{order.Product.Name} - {order.Amount.ToString()}");
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
        }
    }
}