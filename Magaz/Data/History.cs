using System;
using System.Collections.Generic;
using System.Security;

namespace Magaz
{
    public class History
    {
        public List<Order> Orders { get; private set; }

        public History()
        {
            Orders = new List<Order>();
        }

        public void Add(Order receipt)
        {
            if (receipt==null)
            {
                throw new NullReferenceException();
            }

            if (receipt.ProductDataList.Count == 0)
            {
                //пустой чек.   
                return;
            }
            
            Orders.Add(receipt);
        }
    }
    
    public class Order
    {
        public List<ProductData> ProductDataList { get; private set; }
        private readonly DateTime _dateTimeCreation;

        public Order()
        {
            _dateTimeCreation= DateTime.Now;
            ProductDataList = new List<ProductData>();
        }

        public virtual void Add(ProductData order)
        {
            if (order == null)
            {
                throw new ArgumentNullException();
            }
            
            ProductDataList.Add(order);
        }

        public virtual DateTime  GetDataOfCreation()
        {
            return _dateTimeCreation;
        }
    }
    
}