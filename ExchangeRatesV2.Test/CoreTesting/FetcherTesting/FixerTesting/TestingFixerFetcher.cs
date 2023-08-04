using ExchangeRatesV2.Core.Fetchers.Fixer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Test.CoreTesting.FetcherTesting.FixerTesting
{
    public class TestingFixerFetcher
    {

        [Fact]

        public async Task FetchAllSymbols_OnSuccess_ReturnsIEnumerableOfISymbols()
        {
            var fetcher = new FixerFetcher();

            var res = await fetcher.FetchAllSymbolsAsync();

            Assert.NotNull(res);
            Assert.True(res?.Count() > 0);
        }

        [Fact]
        public async Task ConvertTwoCurr_OnSuccess_ReturnsIConvert()
        {
            var fetcher = new FixerFetcher();

            var res = await fetcher.ConvertAsync("USD", "EUR", 2.4m);

            Assert.NotNull(res);
            Assert.True(res?.GetRate() > 0m);
            Assert.True(res?.GetResult() > 0m);
        }

        [Fact]
        public async Task FetchLatestPrices_OnSuccess_ReturnILatestPrice()
        {
            var fetcher = new FixerFetcher();

            var res = await fetcher.FetchLatestPriceAsync();

            Assert.NotNull(res);
            Assert.True(res?.GetRates()?.Count() > 0);
        }

    }

}
