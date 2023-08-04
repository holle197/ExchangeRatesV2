using ExchangeRatesV2.Core.Currencies;
using ExchangeRatesV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Fetchers.Fixer.Data
{
    internal class SymbolsData
    {
#pragma warning disable IDE1006
        public bool success { get; set; }
        public Dictionary<string, string> symbols { get; set; } = new();
#pragma warning restore IDE1006

        public List<ISymbol> GetAllSymbols()
        {
            var res = new List<ISymbol>();
            foreach (var i in symbols)
            {
                res.Add(new Symbol
                {
                    CurrencySymbol = i.Key,
                    CurrencyName = i.Value
                });
            }
            return res;
        }
    }
}
