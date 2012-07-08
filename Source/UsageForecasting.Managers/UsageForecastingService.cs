using System;
using System.Linq;
using Common.Logging;
using FluentValidation;
using UsageForecasting.Messages;
using UsageForecasting.Messages.DTO;
//using UsageForecasting.Managers;

namespace UsageForecasting
{
    public class UsageForecastingService
    {      
        //TODO: Name? purpose is to test the api is live aand running after deployment, may want to return more info
        public string IsAlive()
        {
            return "Usage Forecast Service";
        }

        public UsageForecastResponse Get(GetUsageForecastRequest request)
        {
            var now = request.StartDate;
            var usages = from index in Enumerable.Range(1,744)
                         select new Usage()
                         {
                             StartDate= now.AddHours(index-1),
                             EndDate=now.AddHours(index),
                             Value=index
                         };

            var result = new UsageForecastResponse()
            {
                Usages = usages.ToArray()
            };

            return result;
        }
        
    }
}
