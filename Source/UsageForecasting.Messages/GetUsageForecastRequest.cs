using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsageForecasting.Messages
{
    public class GetUsageForecastRequest : IUsageForecastingMessage 
    {
        public string AccountUri { get; set; }
        public UsageForecastBlendStrategy BlendStrategy { get; set; }
        public UsageForecastSourcingStrategy SourcingStrategy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
