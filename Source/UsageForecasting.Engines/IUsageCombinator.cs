using System;
using System.Collections.Generic;
using UsageForecasting.Messages.DTO;

namespace UsageForecasting.Engines
{
    //  Value By Precendence of Usage Source (may be multiple see UsageForecastBlendStrategies)      
    //  Sum
    //  Average
    //  Max
    //  Min 
    internal interface IUsageCombinator
    {
        Usage Combine(IEnumerable<Usage> usages);
    }
}
