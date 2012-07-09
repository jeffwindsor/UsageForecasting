using System;
using System.Linq;
using System.Collections.Generic;
using UsageForecasting.Messages.DTO;

namespace UsageForecasting.Engines
{
    internal interface IUsageSelector
    {
        IEnumerable<Usage> Select(IEnumerable<IEnumerable<Usage>> usages);
    }

    internal class UsageSelector: IUsageSelector 
    {
        private IUsageGrouper _grouper;
        private IUsageCombinator _combinator;
        public UsageSelector(IUsageGrouper grouper,
             IUsageCombinator combinator)
        {
            _grouper = grouper;
            _combinator = combinator;
        }

        public IEnumerable<Usage> Select(IEnumerable<IEnumerable<Usage>> usages)
        {
            return from g in _grouper.ReGroup(usages)
                   select _combinator.Combine(g);
        }
    }
}
