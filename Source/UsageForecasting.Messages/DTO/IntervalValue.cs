using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsageForecasting.Messages.DTO
{
    public class IntervalValue
    {
        public IntervalValue() { }
        public IntervalValue(Usage source, Decimal value)
        {
            StartDate = source.StartDate;
            EndDate = source.EndDate;
            UnitOfMeasure = source.UnitOfMeasure;
            Value = value;
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Decimal Value { get; set; }
        public UsageUnitOfMeasure UnitOfMeasure { get; set; }
    }
}
