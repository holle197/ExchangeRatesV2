using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Application.Models.Exchangerates
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; } = string.Empty;
        public string ToCurrency { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("yyyy/MM/dd");
    }
}
