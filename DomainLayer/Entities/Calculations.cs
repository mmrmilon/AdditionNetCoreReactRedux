using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities
{
    public class Calculations : Entity
    {
        [Required]
        public string FirstNumber { get; set; }

        [Required]
        public string SecondNumber { get; set; }

        [Required]
        public string Summation { get; set; }

        [Required]
        public DateTime CalculatedOn { get; set; }

        //Foreign key for Users
        [ForeignKey("FK_Calculations_UserId")]
        public long UserId { get; set; }
        public Users User { get; set; }
    }
}
