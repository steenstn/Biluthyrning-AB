using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Models.CarPrices
{
    public class MinibusPrice : CarPrice
    {
        private readonly int _numberOfDays;
        private readonly int _numberOfKm;

        public MinibusPrice(int numberOfDays, int numberOfKm) : base(100, 1)
        {
            _numberOfDays = numberOfDays;
            _numberOfKm = numberOfKm;
        }

        public override decimal GetPrice()
        {
            return BasePrice * _numberOfDays * 1.7m + KmPrice * _numberOfKm * 1.5m;
        }
    }
}
