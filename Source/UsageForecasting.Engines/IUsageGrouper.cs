using System;
using System.Collections.Generic;
using UsageForecasting.Messages.DTO;

namespace UsageForecasting.Engines
{
    internal interface IUsageGrouper
    {
        //  ByExactDateMatch
        //  ByMonth
        //  ByTerm (min start and max end date)
        IEnumerable<IEnumerable<Usage>> ReGroup(IEnumerable<IEnumerable<Usage>> usages);
    }
}
