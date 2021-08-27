using System;
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