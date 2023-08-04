using ExchangeRatesV2.Application.Models.Exchangerates;
using ExchangeRatesV2.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesV2.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<ApiCallsManager> ApiCallManager { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<DailyRate> DailyRates { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
    }
}
