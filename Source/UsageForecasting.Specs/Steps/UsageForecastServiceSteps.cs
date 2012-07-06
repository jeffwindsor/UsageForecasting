using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using UsageForecasting.Specs.Contexts;
using UsageForecasting.Messages;
using FluentAssertions;

namespace UsageForecasting.Specs.Steps
{
    [Binding]
    public class UsageForecastServiceSteps
    {
        private readonly UsageForecastServiceContext _sc;
        private readonly AccountContext _ac;

        public UsageForecastServiceSteps(UsageForecastServiceContext sc,
            AccountContext ac)
        {
            _sc = sc;
            _ac = ac;
        }

        [Given(@"a usage forecast service")]
        public void GivenAUsageForecastService()
        {
            _sc.Service = new UsageForecastingService();
        }

        [Given(@"an account")]
        public void GivenAnAccount()
        {
            _ac.AccountUri = Guid.NewGuid().ToString();
        }

        [When(@"getting an {hourly} forecast from {(.*)} to {(.*)}")]
        public void WhenGettingAnHourlyForecastFromStartToEnd(DateTime startDate, DateTime endDate)
        {
            _sc.GetUsageRequest = new GetUsageForecastRequest()
            {
                AccountUri = _ac.AccountUri,
                StartDate = startDate,
                EndDate = endDate,
                BlendStrategy = UsageForecastBlendStrategy.Default,
                SourcingStrategy = UsageForecastSourcingStrategy.Default
            };
            _sc.UsageResponse = _sc.Service.Get(_sc.GetUsageRequest);
        }
                
        [Then(@"a usage forecast response is returned")]
        public void ThenAUsageForecastResponseIsReturned()
        {
            _sc.UsageResponse.Should().NotBeNull();
        }

        [Then(@"the response has {(\d+)} usages")]
        public void ThenTheResponseHasNUsages(int size)
        {
            _sc.UsageResponse.Usages.Length.Should().Be(size);
        }

        [Then(@"the minimum usage start date is {(.*)}")]
        public void ThenTheMinimumStartDateIs(DateTime target)
        {
            _sc.UsageResponse.Usages.Min(u => u.StartDate).Should().Be(target);
        }

        [Then(@"the maximum usage end date is {(.*)}")]
        public void ThenTheMaximumStartDateIs(DateTime target)
        {
            _sc.UsageResponse.Usages.Max(u => u.EndDate).Should().Be(target.AddDays(1));
        }
    }
}
