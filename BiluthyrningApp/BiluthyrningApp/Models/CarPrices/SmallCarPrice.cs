using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Models.CarPrices
{
    public class SmallCarPrice : CarPrice
    {
        private readonly int _numberOfDays;
        public SmallCarPrice(int numberOfDays) : base(100, 1)
        {
            _numberOfDays = numberOfDays;
        }

        public override decimal GetPrice()
        {
            return BasePrice * _numberOfDays;
        }
    }
}
