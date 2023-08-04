using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Application.Models
{
    public class ApiCallsManager
    {
        public int Id { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("yyyy/MM/dd");
    }
}
