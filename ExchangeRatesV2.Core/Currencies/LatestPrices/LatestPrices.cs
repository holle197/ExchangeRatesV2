using ExchangeRatesV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Currencies.LatestPrices
{
    public class LatestPrices : ILatestPrices
    {
        public string BaseCurrency { get; set; } = "USD";
        public string Date { get; set; } = string.Empty;
        public List<IRate> Rates { get; set; } = new();
        public string GetBase()
        {
            return BaseCurrency;
        }

        public string GetDate()
        {
            return Date;
        }

        public IEnumerable<IRate> GetRates()
        {
            return Rates;
        }

       
    }
}
