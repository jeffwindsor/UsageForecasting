using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsageForecasting.Messages.DTO
{
    public class Usage
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Decimal Amount { get; set; }
        //UOM
    }
}
