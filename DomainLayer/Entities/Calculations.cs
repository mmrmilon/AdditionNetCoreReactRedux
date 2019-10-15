using System;

namespace DomainLayer.Entities
{
    public class Calculations : Entity
    {
        public long UserId { get; set; }

        public string FirstNumber { get; set; }

        public string SecondNumber { get; set; }

        public string Summation { get; set; }

        public DateTime CalculatedOn { get; set; }
    }
}
