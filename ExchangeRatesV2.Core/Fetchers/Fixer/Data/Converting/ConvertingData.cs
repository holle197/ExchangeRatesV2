using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Fetchers.Fixer.Data.Converting
{
    internal class ConvertingData
    {
#pragma warning disable IDE1006

        public RateData? info { get; set; }
        public decimal result { get; set; }
#pragma warning restore IDE1006

    }
}
