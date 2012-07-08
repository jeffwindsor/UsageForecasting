using System;
using System.Collections.Generic;
using UsageForecasting.Messages.DTO;

namespace UsageForecasting.Engines
{
    public delegate IEnumerable<IEnumerable<Usage>> ReGroupUsagesFunc(IEnumerable<IEnumerable<Usage>> usages);
    public delegate Usage CombineUsagesFunc(IEnumerable<Usage> usagesToCombine);

    public delegate IEnumerable<Tuple<IntervalValue, IEnumerable<Usage>>> GroupIntervalsAndUsagesFunc(IEnumerable<Usage> usages, IEnumerable<IntervalValue> intervals);
    public delegate void UpdateUsagesWithIntervalValueFunc(IEnumerable<Usage> usageToUpdate, IntervalValue withValue);

    public interface IUsageEngine
    {
        IEnumerable<Usage> Expand(IEnumerable<Usage> usage, IEnumerable<Usage> toShape);
        IEnumerable<Usage> Expand(IEnumerable<Usage> usage, UsageFrequency toFrequency);

        //ReGroupUsagesFunc:
        //  ByExactDateMatch
        //  ByMonth
        //  ByTerm (min start and max end date)

        //CombineUsagesFunc:
        //  Value By Precendence of Usage Source (may be multiple see UsageForecastBlendStrategies)      
        //  Sum
        //  Average
        //  Max
        //  Min 

        //Use Cases: 
        //  Summarize to Month: Sum ByMonth
        IEnumerable<Usage> Combine(
            CombineUsagesFunc combineUsages,
            ReGroupUsagesFunc reGroupUsagesForCombination,
            IEnumerable<IEnumerable<Usage>> sourceUsagesToCombine
            );


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
        IEnumerable<Usage> Change( 
            UpdateUsagesWithIntervalValueFunc updateUsagesWithIntervalValue,
            GroupIntervalsAndUsagesFunc groupIntervalValuesAndUsagesForUpdate,
            IEnumerable<Usage> sourceUsagesToUpdate,
            IEnumerable<IntervalValue> intervalValuesUsedForUpdate
            );

    }
}
