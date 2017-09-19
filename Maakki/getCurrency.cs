using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Maakki
{
    public class getCurrency
    {

       public ExchangeRate currency {get; set;}

        public getCurrency()
        {
            do
            {
                try
                {
                    using (var webClient = new WebClient())
                    {
                        var money = webClient.DownloadString("http://www.maakki.com/Mcoins/getExchangeRate.aspx");
                        currency = JsonConvert.DeserializeObject<ExchangeRate>(money);
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Can't get currency " + e.Message);
                }
            } while (currency == null);
  

        }


    }
    public class ExchangeRate
    {
        public double HKD { get; set; }
        public double RMB { get; set; }
        public double USD { get; set; }
        public double MYR { get; set; }
        public double JPY { get; set; }
        public double NTD { get; set; }
        public double AAA { get; set; }

    }
}
