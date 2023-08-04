using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Fetchers.Fixer.Data
{
    internal class LatestPricesData
    {
#pragma warning disable IDE1006
        public Dictionary<string, decimal> rates { get; set; } = new();
#pragma warning restore IDE1006

    }
}
