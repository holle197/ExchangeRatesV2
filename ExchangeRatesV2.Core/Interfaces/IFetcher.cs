using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Interfaces
{
    public interface IFetcher
    {
        Task<IEnumerable<ISymbol>> FetchAllSymbolsAsync();
        Task<IConverter> ConvertAsync(string fromCurr, string toCurr, decimal amount);
        Task<ILatestPrices> FetchLatestPriceAsync();

    }
}
