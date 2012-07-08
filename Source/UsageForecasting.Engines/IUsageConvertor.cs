using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsageForecasting.Messages.DTO;

namespace UsageForecasting.Managers
{
    public interface IUsageConvertor
    {
        IEnumerable<Usage> Expand(IEnumerable<Usage> usage, IEnumerable<Usage> toShape);
        IEnumerable<Usage> Expand(IEnumerable<Usage> usage, UsageFrequency toFrequency);
        IEnumerable<Usage> Summarize(IEnumerable<Usage> usage, UsageFrequency toFrequency);
    }

    //private class UsageConvertor: IUsageConvertor 
    //{
    //    //TODO: match predicate (ALL or TOU), may just be settled by type of summary and no need to pull a different version

    //    public IEnumerable<Usage> Profile(IEnumerable<Usage> summaryUsage, IEnumerable<Usage> intervalProfile)
    //    {
    //        throw new NotImplementedException();
    //        //var grouped = from su in summaryUsage
    //        //             let matchingProfiles = GetMatchingProfiles(su, intervalProfile)
    //        //             let matchingProfilesUsage = matchingProfiles.Sum(p => p.Value)
    //        //             select new
    //        //             {
    //        //                 Summary = su,
    //        //                 Profiles = matchingProfiles,
    //        //                 UnitUsage = (matchingProfilesUsage == decimal.Zero) ? decimal.Zero : su.Value / matchingProfilesUsage
    //        //             };

    //        //var result = from g in grouped
    //        //             from p in g.Profiles
    //        //             select new Usage(p,p.Value * g.UnitUsage);

    //        //return result;
    //    }                          

    //    //Differing strategies allow for matching on differing properties
    //    protected abstract IEnumerable<Usage> GetMatchingProfiles(Usage summaryUsage, IEnumerable<Usage> intervalProfile);
    //}
}
