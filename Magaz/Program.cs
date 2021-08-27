using System;
using Magaz.Visual_controller;

namespace Magaz
{
    class Program
    {
        static void Main(string[] args)
        {
            var asketShop = new Shop(new ConsoleController());
            asketShop.Start(); 

        }
    }
}