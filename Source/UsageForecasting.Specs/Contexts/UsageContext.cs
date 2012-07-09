using System;
using System.Collections.Generic;
using System.Linq;
using UsageForecasting.Engines;
using UsageForecasting.Messages.DTO;

namespace UsageForecasting.Specs.Contexts
{
    public class UsageContext
    {
        public IUsageEngine Engine = new UsageForecasting.Engines.UsageEngine();
        public List<Usage> IntervalUsage { get; set; }
        public List<Usage> SummaryUsage = new List<Usage>();
        public List<Usage> SummaryUsageProfile = new List<Usage>();
        public List<Usage> ProfiledSummaryUsage = new List<Usage>();
    }
}
