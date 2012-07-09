using System;
using System.Linq;
using System.Collections.Generic;
using UsageForecasting.Messages.DTO;

namespace UsageForecasting.Engines
{
    //TODO : not covered validation of gaps
    //TODO: not covered validation profile is either rtc or on/off
    public interface IUsageEngine
    {
        IEnumerable<Usage> Shape(IEnumerable<Usage> usages, IEnumerable<Usage> profile);
        //IEnumerable<Usage> Expand(IEnumerable<Usage> usage, UsageFrequency toFrequency);

        //Use Cases: 
        //  Summarize to Month: Sum ByMonth
        //IEnumerable<Usage> Combine(
        //    FuncCombineUsages combineUsages,
        //    FuncReGroupUsages reGroupUsagesForCombination,
        //    IEnumerable<IEnumerable<Usage>> sourceUsagesToCombine
        //    );


        //UpdateUsagesWithIntervalValueFunc:
        //  AddAverageValue
        //  AddWeightedAverageValue
        //  MultiplyByValue
        //  DivideByValue

        //GroupIntervalsAndUsagesFunc:
        //  UsageWithinInterval
        //  UsageStartDateWithinInterval
        //  ...

        //Use Cases:
        //  Gross up: AddAverageValue to UsageWithinInterval
        //IEnumerable<Usage> Change( 
        //    FuncUpdateUsagesWithIntervalValue updateUsagesWithIntervalValue,
        //    FuncGroupIntervalsAndUsages groupIntervalValuesAndUsagesForUpdate,
        //    IEnumerable<Usage> sourceUsagesToUpdate,
        //    IEnumerable<IntervalValue> intervalValuesUsedForUpdate
        //    );
        //public delegate IEnumerable<Tuple<IntervalValue, IEnumerable<Usage>>> FuncGroupIntervalsAndUsages(IEnumerable<Usage> usages, IEnumerable<IntervalValue> intervals);
        //public delegate void FuncUpdateUsagesWithIntervalValue(IEnumerable<Usage> usageToUpdate, IntervalValue withValue);
    }
    public class UsageEngine : IUsageEngine
    {
        public IEnumerable<Usage> Shape(IEnumerable<Usage> usages, IEnumerable<Usage> profiles)
        {
            //Match the profiles to usage by start-end-tou 
            //  if the usage is RTC then it will match any detailed TOU
            //  return scaled usage per profile interval
            return  from u in usages
		            from p in profiles
		            where (u.StartDate <= p.EndDate
			            && p.EndDate <= u.EndDate
			            && (u.TimeOfUse == p.TimeOfUse || u.TimeOfUse == UsageTimeOfUse.RoundTheClock)
			            )
		            group p by u into g
                    let totalProfileUsage = g.Sum(p => p.Value)
                    let unitUsage = (totalProfileUsage==decimal.Zero)
                                    ? decimal.Zero
                                    : g.Key.Value/totalProfileUsage 
                    from p in g
                    select new Usage(p, g.Key.Source, p.Value * unitUsage);

        }

    }

}
