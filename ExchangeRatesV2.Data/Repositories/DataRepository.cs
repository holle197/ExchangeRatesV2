using ExchangeRatesV2.Application.Models;
using ExchangeRatesV2.Application.Models.Exchangerates;
using ExchangeRatesV2.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Data.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly Context _dataContext;

        public DataRepository(Context context)
        {
            this._dataContext = context;
        }
        public async Task<List<Currency>> AddAllCurrencies(List<Currency> currencies)
        {
            _dataContext.Currencies.AddRange(currencies);
            await _dataContext.SaveChangesAsync();
            return currencies;
        }

        public async Task AddDailyRates(List<DailyRate> rates)
        {
            _dataContext.DailyRates.AddRange(rates);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ExchangeRate> AddExchangeRate(ExchangeRate exchangeRate)
        {
            _dataContext.Add(exchangeRate);
            await _dataContext.SaveChangesAsync();
            return exchangeRate;
        }

        public async Task<List<DailyRate>?> GetDailyRates()
        {
            var today = DateTime.Now.ToString("yyyy/MM/dd");
            var rates = await _dataContext.DailyRates.Where(r => r.Date == today).ToListAsync();

            if (rates is null || rates.Count == 0) return null;
            return rates;
        }

        public async Task<ExchangeRate?> GetExchangeRate(string fromCur, string toCur)
        {
            var rates = await _dataContext.ExchangeRates.Where(r => r.FromCurrency == fromCur && r.ToCurrency == toCur).ToListAsync();
            var today = DateTime.Now.ToString("yyyy/MM/dd");
            if (rates is null || rates.Count == 0) return null;
            foreach (var rate in rates)
            {
                if (rate.Date == today) return rate;
            }

            return null;
        }

        public async Task<List<Currency>?> GetSupportedCurrencies()
        {
            var res = await _dataContext.Currencies.ToListAsync();
            if (res is null || res.Count <= 0) return null;
            return res;
        }

        public async Task<int> GetTotalApiCalls()
        {
            var currDate = DateTime.Now.ToString("MMMM yyyy");
            var total = await _dataContext.ApiCallManager.Where(t => t.Date == currDate).ToListAsync();
            return total?.Count ?? 0;
        }

        public async Task RegisterNewApiCall()
        {
            _dataContext.ApiCallManager.Add(new ApiCallsManager()
            {
                Date = DateTime.Now.ToString("MMMM yyyy")
            });
            await _dataContext.SaveChangesAsync();
        }
    }
}
