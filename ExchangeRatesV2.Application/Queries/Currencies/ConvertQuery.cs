using ExchangeRatesV2.Application.DTO;
using ExchangeRatesV2.Application.Models.Exchangerates;
using ExchangeRatesV2.Application.Repositories;
using ExchangeRatesV2.Application.Results;
using ExchangeRatesV2.Core.Interfaces;
using ExchangeRatesV2.Core.RateConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Application.Queries.Currencies
{
    public class ConvertQuery
    {
        private readonly IFetcher _fetcher;
        private readonly IDataRepository _dataRepository;

        public ConvertQuery(IFetcher fetcher, IDataRepository dataRepository)
        {
            this._fetcher = fetcher;
            this._dataRepository = dataRepository;
        }


        public async Task<ExchangeRateResult> Convert(string from, string to, decimal amount, string maxApiCalls)
        {

            from = from.ToUpper();
            to = to.ToUpper();
            // db have current exchange rate for given pair and time of fetched data is not older than 1 day
            var rateFromDb = await _dataRepository.GetExchangeRate(from, to);
            if (rateFromDb is not null)
            {
                return ConvertFromDb(rateFromDb, amount);
            }


            //there are enough free api calls so user can get direct exchange rate
            if (await HaveEnoughFreeApiCalls(maxApiCalls))
            {
                var rate = await _fetcher.ConvertAsync(from, to, amount);
                return await ConvertFromFetcher(rate, amount);
            }

            //there are not enough free api calls
            var dailyRates = await _dataRepository.GetDailyRates();
            if (dailyRates is null)
            {
                var latestPrices = await _fetcher.FetchLatestPriceAsync();
                var rates = latestPrices.GetRates();
                var res = ExchangeRateConverter.ConvertFromIRateToDaily(rates);
                await _dataRepository.AddDailyRates(res.ToList());
                await _dataRepository.RegisterNewApiCall();
                return ConvertFromDbDailyRates(res.ToList(), from, to, amount);

            }

            return ConvertFromDbDailyRates(dailyRates, from, to, amount);
        }

        private static ExchangeRateResult ConvertFromDb(ExchangeRate exchangeRate, decimal amount)
        {
            var total = OfflineRateConversion.ConvertBetweenTwoCurencies(exchangeRate.Rate, amount);

            var result = ExchangeRateConverter.ConvertFromExchangeRateToResult(exchangeRate);
            result.Result = total;
            result.Amount = amount;

            return result;
        }

        //check if there are enough free api calls
        private async Task<bool> HaveEnoughFreeApiCalls(string maxApiCalls)
        {
            var maxApiCallsPerMonth = Int32.Parse(maxApiCalls!);
            var totalApiCallsThisMonth = await _dataRepository.GetTotalApiCalls();

            var curYear = (int)DateTime.Now.Year;
            var curMonth = (int)DateTime.Now.Month;
            int maxDaysInCurMonth = DateTime.DaysInMonth(curYear, curMonth);
            int currDay = DateTime.Now.Day;

            var freeApiCalls = maxApiCallsPerMonth - totalApiCallsThisMonth;
            var reservedCalls = maxDaysInCurMonth - currDay;

            return freeApiCalls > reservedCalls;
        }

        //fetch rates from given fetcher and write to db result and return 
        private async Task<ExchangeRateResult> ConvertFromFetcher(IConverter converter, decimal amount)
        {
            var rate = await _dataRepository.AddExchangeRate(ExchangeRateConverter.ConvertFromIConverterToExchangeRate(converter));
            var res = ExchangeRateConverter.ConvertFromExchangeRateToResult(rate);
            res.Amount = amount;
            res.Result = converter.GetResult();
            await _dataRepository.RegisterNewApiCall();
            return res;
        }

        private static ExchangeRateResult ConvertFromDbDailyRates(List<DailyRate> dailyRates, string from, string to, decimal amount)
        {
            var dailyRateToIRates = ExchangeRateConverter.ConvertFromRateToIRate(dailyRates);
            var result = OfflineRateConversion.GenerateMiddlePrice(dailyRateToIRates.ToList(), from, to, amount);
            return new ExchangeRateResult()
            {
                FromCurrency = from,
                ToCurrency = to,
                Amount = amount,
                Rate = result / amount,
                Result = result,
            };
        }

    }
}
