using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Magaz.Users;

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

        public List<Order> FindUserOrders(IAuthorisedUser user)
        {
            var userOrderList = new List<Order>();
            foreach (var order in Orders)
            {
                if (order.GetUser().Login.Equals(user.Login))
                {
                    userOrderList.Add(order);
                }
            }
            return userOrderList;
        }

        public void DeleteAllPresence(Product product)
        {
            //todo remake
            var newOrders =  new List<Order>();
            foreach (var order in Orders.ToArray())
            {
                order.ProductDataList.RemoveAll(x => x.Product.Equals(product));
                if (order.ProductDataList.Count is 0)
                {
                    Orders.Remove(order);
                }
            }
        }
    }
    
    public class Order
    {
        public List<ProductData> ProductDataList { get; private set; }
        private readonly DateTime _dateTimeCreation;
        private readonly IAuthorisedUser user;
        public Order(IAuthorisedUser user)
        {
            _dateTimeCreation= DateTime.Now;
            ProductDataList = new List<ProductData>();
            this.user = user;
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

        public virtual IAuthorisedUser GetUser()
        {
            return user;
        }
    }
    
}