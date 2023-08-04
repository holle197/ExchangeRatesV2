using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Interfaces
{
    public interface IRate
    {
        string GetSymbol();
        decimal GetRate();
    }
}
