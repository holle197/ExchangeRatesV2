using ExchangeRatesV2.Core.Currencies;
using ExchangeRatesV2.Core.Currencies.LatestPrices;
using ExchangeRatesV2.Core.ErrorHandling;
using ExchangeRatesV2.Core.Fetchers.Fixer.Data;
using ExchangeRatesV2.Core.Fetchers.Fixer.Data.Converting;
using ExchangeRatesV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.Fetchers.Fixer
{
    public class FixerFetcher : IFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly string _fixedUrl;
        public FixerFetcher(string apiKey, string url)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
            _fixedUrl = url;
        }

        // this constructor is for testing only
        public FixerFetcher()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", "nQ79FQEm879L7xHxyPORbMD6PPofZMvk");
            _fixedUrl = "https://api.apilayer.com/fixer/";
        }


        public async Task<IConverter> ConvertAsync(string fromCurr, string toCurr, decimal amount)
        {
            if (fromCurr == toCurr) throw new FetcherException("Cannot Convert The Same Currency.");
            if (amount <= 0m) throw new FetcherException("Amount Must Be Greather Than 0.");
            try
            {
                var apiCall = await _httpClient.GetAsync(_fixedUrl + $"convert?to={toCurr}&from={fromCurr}&amount={amount}");
                var res = await apiCall.Content.ReadFromJsonAsync<ConvertingData>();

                if (res is { info: not null } && res.info.rate > 0m && res.result > 0m)
                {
                    return new Converter
                    {
                        FromCurrency = fromCurr,
                        ToCurrency = toCurr,
                        Rate = res.info.rate,
                        Result = res.result
                    };
                }

                throw new FetcherException("Internal Server Error");

            }
            catch (Exception e)
            {

                throw new FetcherException(e.Message);
            }
        }


        public async Task<IEnumerable<ISymbol>> FetchAllSymbolsAsync()
        {
            try
            {
                var apiCall = await _httpClient.GetAsync(_fixedUrl + "symbols");
                var res = await apiCall.Content.ReadFromJsonAsync<SymbolsData>();

                if (res is { symbols: not null } && res.symbols.Count > 0) return res.GetAllSymbols();

                throw new FetcherException("Internal Server Error");

            }
            catch (Exception e)
            {

                throw new FetcherException(e.Message);
            }
        }


        public async Task<ILatestPrices> FetchLatestPriceAsync()
        {
            try
            {
                var apiCall = await _httpClient.GetAsync(_fixedUrl + "latest?base=USD");
                var res = await apiCall.Content.ReadFromJsonAsync<LatestPricesData>() ?? throw new NullReferenceException();

                if (res is null || res.rates.Count < 1) throw new FetcherException("Internal Server Error");

                return new LatestPrices
                {
                    //curr date formated as year month and day
                    Date = DateTime.Now.ToString("yyyy/MM/dd"),
                    Rates = GenerateRates(res.rates),
                    BaseCurrency = "USD"
                };
            }

            catch (Exception e)
            {

                throw new FetcherException(e.Message);
            }
        }


        private static List<IRate> GenerateRates(Dictionary<string, decimal> ratesAsDic)
        {
            var res = new List<IRate>();
            foreach (var i in ratesAsDic)
            {
                res.Add(new Rate
                {
                    CurrencySymbol = i.Key,
                    CurrencyRate = i.Value
                });
            }
            return res;
        }


    }
}
