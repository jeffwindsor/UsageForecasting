using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using UsageForecasting.Specs.Contexts;
using UsageForecasting.Messages;
using UsageForecasting.Messages.DTO;
using FluentAssertions;

namespace UsageForecasting.Specs.Steps
{
    [Binding]
    public class UsageEngineSteps
    {
        private UsageContext _usage;

        public UsageEngineSteps(UsageContext usage)
        {
            _usage = usage;
        }

        [Given(@"a summary usage for {(\d+\.?\d*)} {(.*)} from {(.*)} to {(.*)}")]
        public void GivenASummaryUsage(decimal value, UsageUnitOfMeasure uom, DateTime startDate, DateTime endDate)
        {
            _usage.SummaryUsage.Add(new Usage()
            {
                StartDate = startDate,
                EndDate = endDate,
                Source = UsageSource.HistoricalUsage,
                TimeOfUse= UsageTimeOfUse.RoundTheClock,
                UnitOfMeasure=uom,
                Value=value 
            });
        }

        [Given(@"an hourly profile from {(.*)} to {(.*)}")]
        public void GivenAProfileFrom(DateTime startDate, DateTime endDate)
        {
            //sortof random values for hours between star and end date
            var rnd = new Random(DateTime.Now.Millisecond);
            var hours = (int)endDate.Subtract(startDate).TotalHours;
            var profileUsages = from hour in Enumerable.Range(0, 1 + hours)
                                select new Usage()
                                {
                                    StartDate = startDate.AddHours(hour),
                                    EndDate = startDate.AddHours(hour + 1),
                                    Source = UsageSource.ProfileUsage,
                                    TimeOfUse = UsageTimeOfUse.RoundTheClock,
                                    Value = (decimal)rnd.NextDouble()
                                };
            _usage.SummaryUsageProfile.AddRange(profileUsages);
        }
        
        [When(@"the usage forecaster requests the summary usage be profiled")]
        public void WhenTheUsageForecasterRequestsTheSummaryUsageBeProfiled()
        {
            _usage.ProfiledSummaryUsage.AddRange(_usage.Engine.Shape(_usage.SummaryUsage, _usage.SummaryUsageProfile));
        }
              
        [Then(@"the usage engine should return a profiled summary")]
        public void ThenTheUsageEngineShouldReturnAProfiledSummary()
        {
            _usage.ProfiledSummaryUsage.Count.Should().BeGreaterThan(0);
        }

        [Then(@"the profiled summaries should range from {(.*)} to {(.*)}")]
        public void ThenTheProfiledSummaryShouldRangeFrom(DateTime startDate, DateTime endDate)
        {
            _usage.ProfiledSummaryUsage
                .Min(u => u.StartDate)
                .Should().Be(startDate);

            _usage.ProfiledSummaryUsage
                .Max(u => u.EndDate)
                .Should().Be(endDate);
        }

        [Then(@"the profiled summary values should be equal to the profile value times the unit value")]
        public void ThenTheProfiledSummaryValuesShouldBeEqualToTheProfileValueTimesTheUnitValue()
        {
            var matchedValues = from u in _usage.SummaryUsage
                                from p in _usage.ProfiledSummaryUsage
                                where (u.StartDate <= p.EndDate
                                    && p.EndDate <= u.EndDate
                                    && (u.TimeOfUse == p.TimeOfUse || u.TimeOfUse == UsageTimeOfUse.RoundTheClock)
                                    )
                                group p by u into g
                                select g.Key.Value - g.Sum(p => p.Value);

            matchedValues.All(i => -0.000001M < i && i < 0.000001M);
        }

    }
}
