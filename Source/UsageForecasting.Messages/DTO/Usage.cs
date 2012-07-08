using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsageForecasting.Messages.DTO
{
    public class Usage : IntervalValue
    {
        public Usage() { }
        public Usage(Usage source, Decimal value) :
            base(source, value)
        {
            TimeOfUse = source.TimeOfUse;
            Source = source.Source;
            //Frequency = source.Fwrequency;
        }
        public UsageTimeOfUse TimeOfUse { get; set; }
        public UsageSource Source { get; set; }
        //public UsageFrequency Frequency { get; set; }
    }
}
