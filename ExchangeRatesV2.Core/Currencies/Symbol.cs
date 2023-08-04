using ExchangeRatesV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Currencies
{
    public class Symbol : ISymbol
    {
        public string CurrencyName { get; set; } = string.Empty;
        public string CurrencySymbol { get; set; } = string.Empty;
        public string GetCurrencyName()
        {
            return CurrencyName;
        }

        public string GetCurrencySymbol()
        {
            return CurrencySymbol;
        }
    }
}
