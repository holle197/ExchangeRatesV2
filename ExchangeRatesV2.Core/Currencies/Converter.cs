﻿using ExchangeRatesV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Currencies
{
    internal class Converter : IConverter
    {
        public string FromCurrency { get; set; } = string.Empty;
        public string ToCurrency { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public decimal Result { get; set; }

        public string GetFromCurrency()
        {
            return FromCurrency;
        }

        public decimal GetRate()
        {
            return Rate;
        }

        public string GetToCurrency()
        {
            return ToCurrency;
        }

        public decimal GetResult()
        {
            return Result;
        }
    }
}
