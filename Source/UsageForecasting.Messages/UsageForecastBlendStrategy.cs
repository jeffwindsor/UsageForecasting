using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsageForecasting.Messages
{
    public enum UsageForecastBlendStrategy
    {
        Default,
        IntervalThenSummary,
        SummaryThenInterval,
        IntervalPlusAverageSummaryExcess
    }
}
