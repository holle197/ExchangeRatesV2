using ExchangeRatesV2.Application.Models;
using ExchangeRatesV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Application.DTO
{
    internal static class SymbolsToCurrencies
    {
        internal static List<Currency> Convert(IEnumerable<ISymbol> symbols)
        {
            List<Currency> res = new();
            foreach (var symbol in symbols)
            {
                res.Add(new Currency() { Name = symbol.GetCurrencyName(), Symbol=symbol.GetCurrencySymbol() });
            }
            return res;
        }
    }
}
