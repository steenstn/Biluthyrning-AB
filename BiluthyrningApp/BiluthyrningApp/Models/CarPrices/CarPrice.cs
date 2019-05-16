using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Models.CarPrices
{
    public abstract class CarPrice
    {

        public CarPrice(decimal basePrice, decimal kmPrice)
        {
            BasePrice = basePrice;
            KmPrice = kmPrice;
        }

        public decimal BasePrice { get; }
        public decimal KmPrice { get; }


        public abstract decimal GetPrice();
    }
}
