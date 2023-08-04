using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Interfaces
{
    public interface IConverter
    {
        string GetFromCurrency();
        string GetToCurrency();
        decimal GetRate();
        decimal GetResult();
    }
}
