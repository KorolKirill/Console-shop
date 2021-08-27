using System.Collections.Generic;
using Magaz.Products.Food;

namespace Magaz
{
    public class DataBase
    {
        public List<ProductData> ProductDatas { get; private set; }
        public List<string> History { get; private set; }
        // Отдельный класс под историю? и там чет интересное в нем? - просто сделать отдельный класс заказ
        public DataBase()
        {
            ProductDatas = new List<ProductData>();
            History = new List<string>();
            Initialize();
        }
        
        private void Initialize()
        {
            ProductDatas.Add(new ProductData(new Fish("Fish",1001),10));
            ProductDatas.Add(new ProductData(new Bread("Bread",1010),3));
            ProductDatas.Add(new ProductData(new Meat("Meat",1008),25));
        }
    }
}