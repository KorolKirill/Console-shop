using System;
using Magaz.Products.Food;
using Magaz.Visual_controller;

namespace Magaz
{
    class Program
    {
        static void Main(string[] args)
        {
            ShopManager manager = new ShopManager(new Shop());
            manager.Start();
        }
    }
}