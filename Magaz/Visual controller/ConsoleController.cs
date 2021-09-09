using System;
using System.Collections.Generic;

namespace Magaz.Visual_controller
{ 
    public class ConsoleController : IVisualController
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string RequestUserLogin()
        {
            Console.WriteLine("Enter your Login:");
            return Console.ReadLine();
        }

        public string RequestUserPassword()
        {
            Console.WriteLine("Enter your password");
            return Console.ReadLine();
        }

        public void ShowOrders(List<Order> orders)
        {
            if (orders.Count == 0)
            {
                Console.WriteLine("We don`t have anything in history right now");
                return;
            }

            int counter = 1;
            foreach (var order in orders)
            {
                Console.WriteLine("____________________________");
                Console.WriteLine($"Receipt-{counter++}");
                foreach (var productType in order.ProductDataList)
                {
                    Console.WriteLine($"{productType.Product.Name} - {productType.Quantity.ToString()}");
                }

                Console.WriteLine($"Date: {order.GetDataOfCreation().ToString()}");
                Console.WriteLine($"Owner: {order.GetUser().Login}");
                Console.WriteLine("----------------------------");
            }
            
            
        }

        public void WelcomeShopMessage()
        {
            string welcomeMessage = "Welcome in our super shop! ;D";
            Console.WriteLine(welcomeMessage);
        }

        public void FailedLogIn()
        {
            Console.WriteLine("You entered wrong login or password.");
            Console.WriteLine("Try again or create new account.");
        }

        public void WelcomeUserMessage(string name)
        {
            Console.WriteLine($"Glad to see you again {name}");
        }

        public void ShowUserMenu()
        {
            string menu =
                "\n Pick your option:" +
                "\n 1. Show list of all products" +
                "\n 2. Buy some stuff" +
                "\n 3. Show your buying history " +
                "\n 0. Exit from your account";
            Console.Write(menu);
        }

        public void ShowGuestMenu()
        {
            string menu =
                "\nPick your option:" +
                "\n1. Show List of all products." +
                "\n0. back to menu";
                Console.Write(menu);
        }

        public void ShowAdminMenu()
        {
            string menu =
                "\n Pick your option:" +
                "\n 1. Show list of all products" +
                "\n 2. Buy some stuff" +
                "\n 3. Show your buying history " +
                "\n 4. Show all buying history in shop " +
                "\n 5. Add product in shop." +
                "\n 6. Remove product from shop" +
                "\n 0. Exit from your account";
            Console.Write(menu);
        }

        public void ShowMainMenu()
        {
            string menu =
                "\n Pick your option:" +
                "\n 1. Login." +
                "\n 2. Register." +
                "\n 3. Login as Guest." +
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

                // Console.WriteLine(
                //     $"{counter++}. " +
                //     $"Name: {productData.Product.Name}, " +
                //     $"code: {productData.Product.Code}, " +
                //     $"quantity: {productData.Quantity} "
                // );
                Console.WriteLine(
                $"{counter++}. " +
                $"{productData.ToString()}");
            }
        }

        public int RequestOption()
        {
            string message = "\nPick an option";
            Console.WriteLine(message);
            var option = ReadANumberFromConsole();
            return option;
        }

        public void WrongOption<T>(T option) where T : struct 
        {
            Console.WriteLine($"Sorry, but you have picked option that does not exist! You picked: {option.ToString()}");
        }
        
        
        public void WrongAmountProductBuying(int amount)
        {
            Console.WriteLine("You entered an amount that we cannot sell you");
            Console.WriteLine($"Your input: {amount.ToString()}");
            Console.WriteLine("You will need to pick an product again.");
        }

        public void NotEnoughProduct(ProductData data, int userWantedAmount)
        {
            Console.WriteLine("We are sorry, but we have not enough of this product");
            Console.WriteLine("You wanted to buy:");
            Console.WriteLine($"Name: {data.Product.Name} - {userWantedAmount.ToString()} ");
            Console.WriteLine($"But we have only {data.Quantity} in out shop");
            Console.WriteLine("You will need to pick anything else again.");

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

        public bool WarningAddingNewProductType(ProductInformation productInformation)
        {
            if (productInformation.Name is not null)
            {
                Console.WriteLine("Warning you entered a product name that we don`t have. Do you really want to add it?");
            }

            if (productInformation.Name is null)
            {
                Console.WriteLine($"You wanted to add a product buy it`s code. " +
                                  $"But a product with a code {productInformation.Code} doesn`t exist");
                Console.WriteLine("Do you want to add new product with this code?");
            }

            return YesOrNot();
        }

        public void WarningAddingNewProductType()
        {
            Console.WriteLine("Be careful!");
            Console.WriteLine("If you try select product by code that doesn`t exist, you will need");
        }

        public int RequestAmountOfProduct(ProductData data)
        {
            Console.WriteLine("Okey, we found a product that match your request.");
            Console.WriteLine($"Your Product:{data.Product.Name}, on stock we have {data.Quantity} of it.");
            Console.WriteLine("How many of it ? Enter a number.");
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
            Console.WriteLine("Enter product code or name.");
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

        public void LoginIsNotAvailable()
        {
            Console.WriteLine("Sorry, but your wished login is already taken");
            Console.WriteLine("Try registrate with another one");
        }


        public string RequestProductName()
        {
            Console.WriteLine("Enter product name:");
            return Console.ReadLine();
        }

        public ShopManager.CodeSetting RequestCodeSetting()
        {
            Console.WriteLine("What about product code?");
            Console.WriteLine("We can set it automatically");
            Console.WriteLine("Or you can enter it.");
            Console.WriteLine("Enter: Automatically - 0, Manually - 1");
            return (ShopManager.CodeSetting) ReadANumberFromConsole();
        }

        public int RequestProductCode()
        {
            Console.WriteLine("Enter product code:");
            return ReadANumberFromConsole();
        }

        public void WarningCodeTaken(int code)
        {
            Console.WriteLine($"Code: {code} is already taken. Enter another one.");
        }

        private bool YesOrNot()
        {
            Console.WriteLine("Enter: 1 - yes, 0 - no");
            var input = ReadANumberFromConsole();
            if (input is 1)
            {
                return true;
            }

            if (input is 0)
            {
                return false;
            }
            WrongOption(input);
            return YesOrNot();
        }

        public int RequestAddingAmount(ProductInformation productInformation)
        {
            Console.WriteLine($"How many of {productInformation.Name} do you want to add?");
            var amount = ReadANumberFromConsole();
            return  amount < 0 ? 0 : amount ;
        }

        public bool AskForDeleteHistory(Product product)
        {
            Console.WriteLine("Do you want to delete all history of this product?");

            return YesOrNot();
        }

        public void WrongAmount()
        {
            Console.WriteLine("You entered wrong amount.");
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