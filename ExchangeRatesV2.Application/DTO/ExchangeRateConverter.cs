using ExchangeRatesV2.Application.Models.Exchangerates;
using ExchangeRatesV2.Application.Results;
using ExchangeRatesV2.Core.Currencies.LatestPrices;
using ExchangeRatesV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Application.DTO
{
    internal static class ExchangeRateConverter
    {
        internal static ExchangeRateResult ConvertFromExchangeRateToResult(ExchangeRate er)
        {
            return new ExchangeRateResult()
            {
                FromCurrency = er.FromCurrency,
                ToCurrency = er.ToCurrency,
                Rate = er.Rate,
            };

        }

        internal static ExchangeRate ConvertFromIConverterToExchangeRate(IConverter converter)
        {
            return new ExchangeRate()
            {
                FromCurrency = converter.GetFromCurrency(),
                ToCurrency = converter.GetToCurrency(),
                Rate = converter.GetRate()
            };
        }

        internal static IEnumerable<DailyRate> ConvertFromIRateToDaily(IEnumerable<IRate> rates)
        {
            var res = new List<DailyRate>();
            foreach (var item in rates)
            {
                res.Add(new DailyRate()
                {
                    Symbol = item.GetSymbol(),
                    Rate = item.GetRate(),
                });
            }
            return res;

        }

        internal static IEnumerable<IRate> ConvertFromRateToIRate(IEnumerable<DailyRate> rates)
        {
            var rate = new List<Rate>();
            foreach (var item in rates)
            {
                rate.Add(new Rate() { CurrencyRate=item.Rate,CurrencySymbol=item.Symbol});
            }
            return rate;
        }
    }
}
