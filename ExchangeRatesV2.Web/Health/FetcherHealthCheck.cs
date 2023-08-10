using ExchangeRatesV2.Core.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ExchangeRatesV2.Web.Health
{
    public class FetcherHealthCheck : IHealthCheck
    {
        private readonly IFetcher _fetcher;
        public FetcherHealthCheck(IFetcher fetcher)
        {
            this._fetcher = fetcher;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var res = await _fetcher.FetchAllSymbolsAsync();
                return HealthCheckResult.Healthy();
            }
            catch (Exception e)
            {

                return HealthCheckResult.Degraded(exception: e);
            }
        }
    }
}
