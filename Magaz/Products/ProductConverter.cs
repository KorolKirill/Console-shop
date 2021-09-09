using System;
using System.Collections.Generic;
using Magaz.Products.Food;

namespace Magaz
{
    public static class ProductConverter
    {
       public static Product ConvertToProduct(ProductInformation productInformation)
        {
            if (productInformation.Code == 0)
            {
                throw new Exception("Trying to create product without code.");
            }

            if (string.IsNullOrWhiteSpace(productInformation.Name))
            {
                throw new Exception("Trying to create product without name.");
            }
            
            switch (productInformation.Name.ToLower())
            {
                case "fish":
                    return new Fish(productInformation.Name, productInformation.Code);
                    break;
                case "bread":
                    return new Bread(productInformation.Name, productInformation.Code);
                    break;
                case "water":
                    return new Water(productInformation.Name, productInformation.Code);
                    break;
                case "meat":
                    return new Meat(productInformation.Name, productInformation.Code);
                default:
                    // для продуктов под которые мы не сделали объекты то что делать? сделать какой-то UndefinedProduct 
                    // и в него все запихивать или шо?
                    throw new Exception("Trying to create product that we don`t support right now.");
            }       
        }
    }
}