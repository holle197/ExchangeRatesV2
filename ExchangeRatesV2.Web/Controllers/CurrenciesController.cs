using ExchangeRatesV2.Application.Models;
using ExchangeRatesV2.Application.Queries.Currencies;
using ExchangeRatesV2.Application.Repositories;
using ExchangeRatesV2.Application.Results;
using ExchangeRatesV2.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExchangeRatesV2.Web.Controllers
{
    public class CurrenciesController :ControllerBase
    {
        private readonly ConvertQuery _converterQuery;
        private readonly SupportedCurrenciesQuery _supportedCurrenciesQuery;

        public CurrenciesController(IFetcher fetcher,IDataRepository dataRepository)
        {
            this._converterQuery = new ConvertQuery(fetcher, dataRepository);
            this._supportedCurrenciesQuery = new SupportedCurrenciesQuery(fetcher, dataRepository);
        }

        [HttpGet("GetSupportedCurrencies")]
        public async Task<IEnumerable<Currency>> Get()
        {
            return await _supportedCurrenciesQuery.GetSupportedCurrenceis();
        }

        [HttpGet("Convert")]
        public async Task<ExchangeRateResult> Convert(string from,string to,decimal amount)
        {
            var numOfMaxApiCalls = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ApiCallsSettings")["MaxApiCallsPerMonth"] ?? "0";
            return await _converterQuery.Convert(from, to,amount,numOfMaxApiCalls);
        }
    }
}
