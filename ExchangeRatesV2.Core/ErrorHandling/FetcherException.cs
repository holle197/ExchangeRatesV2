using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesV2.Core.ErrorHandling
{
    public class FetcherException : Exception
    {
        public FetcherException(string msg) : base(msg)
        {
                
        }
    }
}
