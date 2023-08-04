using ExchangeRatesV2.Application.Models;
using ExchangeRatesV2.Application.Repositories;
using ExchangeRatesV2.Core.Interfaces;
using ExchangeRatesV2.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Application.Queries.Currencies
{
    public class SupportedCurrenciesQuery
    {
        private readonly IFetcher _fetcher;
        private readonly IDataRepository _dataRepository;



        public SupportedCurrenciesQuery(IFetcher fetcher,IDataRepository dataRepository)
        {
            this._fetcher = fetcher;
            this._dataRepository = dataRepository;
        }

        public async Task<IEnumerable<Currency>> GetSupportedCurrenceis()
        {
            var res = await _dataRepository.GetSupportedCurrencies();

            if(res is null)
            {
                var currencies = await _fetcher.FetchAllSymbolsAsync();
                await _dataRepository.RegisterNewApiCall();
                return await _dataRepository.AddAllCurrencies(SymbolsToCurrencies.Convert(currencies));
            }

            return res;
        }
    }
}
