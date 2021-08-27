﻿using System;
using System.Collections.Generic;
using System.Security;

namespace Magaz
{
    public class History
    {
        public List<Receipt> Receipts { get; private set; }

        public History()
        {
            Receipts = new List<Receipt>();
        }

        public void Add(Receipt receipt)
        {
            if (receipt==null)
            {
                throw new NullReferenceException();
            }

            if (receipt.OrderList.Count==0)
            {
                //пустой чек.   
                return;
            }
            
            Receipts.Add(receipt);
        }
    }
    
    public class Receipt
    {
        public List<Order> OrderList { get; private set; }
        private readonly DateTime _dateTimeCreation;

        public Receipt()
        {
            _dateTimeCreation= DateTime.Now;
            OrderList = new List<Order>();
        }

        public Receipt(Order order) :this()
        {
            
        }

        public virtual void Add(Order order)
        {
            if (order==null)
            {
                throw new ArgumentNullException();
            }
            
            OrderList.Add(order);
        }

        public virtual DateTime  GetDataOfCreation()
        {
            return _dateTimeCreation;
        }
    }

    public class Order
    {
        public Product Product { get; private set; }
        public int Amount { get; private set;  }

        public Order(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }
    }
}