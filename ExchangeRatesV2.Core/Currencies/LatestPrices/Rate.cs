using ExchangeRatesV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Currencies.LatestPrices
{
    public class Rate : IRate
    {
        public string CurrencySymbol { get; set; } = string.Empty;
        public decimal CurrencyRate { get; set; }

        public decimal GetRate()
        {
            return CurrencyRate;
        }

        public string GetSymbol()
        {
            return CurrencySymbol;
        }
    }
}
