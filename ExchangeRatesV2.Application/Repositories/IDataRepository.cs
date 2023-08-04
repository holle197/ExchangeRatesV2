using ExchangeRatesV2.Application.Models.Exchangerates;
using ExchangeRatesV2.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Application.Repositories
{
    public interface IDataRepository
    {
        Task<List<Currency>> AddAllCurrencies(List<Currency> currencies);
        Task<List<Currency>?> GetSupportedCurrencies();

        Task<ExchangeRate> AddExchangeRate(ExchangeRate exchangeRate);
        Task<ExchangeRate?> GetExchangeRate(string fromCur, string toCur);

        Task AddDailyRates(List<DailyRate> rates);
        Task<List<DailyRate>?> GetDailyRates();

        Task<int> GetTotalApiCalls();
        Task RegisterNewApiCall();

    }
}
