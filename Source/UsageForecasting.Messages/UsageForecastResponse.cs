using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsageForecasting.Messages.DTO;

namespace UsageForecasting.Messages
{
    public class UsageForecastResponse: IUsageForecastingMessage 
    {
        public Usage[] Usages { get; set; }
    }
}
