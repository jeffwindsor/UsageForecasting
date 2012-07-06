using System;
using System.Collections.Generic;
using UsageForecasting;
using UsageForecasting.Messages;

namespace UsageForecasting.Specs.Contexts
{
    public class UsageForecastServiceContext
    {
       
        public UsageForecastingService Service { get; set; }
        public GetUsageForecastRequest GetUsageRequest { get; set; }
        public UsageForecastResponse UsageResponse { get; set; }
    }
}
