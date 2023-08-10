using ExchangeRatesV2.Application.Repositories;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ExchangeRatesV2.Web.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly IDataRepository _dataRepository;

        public DatabaseHealthCheck(IDataRepository dataRepository)
        {
            this._dataRepository = dataRepository;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var res = await _dataRepository.GetSupportedCurrencies();
                return HealthCheckResult.Healthy();
            }
            catch (Exception e)
            {

                return HealthCheckResult.Degraded(exception: e);
            }
        }
    }
}
