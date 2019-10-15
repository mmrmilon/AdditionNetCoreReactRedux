using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.DataModel
{
    public class CalculationDataModel
    {
        public long CalculationId { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public string FirstNumber { get; set; }

        public string SecondNumber { get; set; }

        public string Summation { get; set; }

        public DateTime CalculatedOn { get; set; }
    }
}
