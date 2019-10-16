using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Entities
{
    public class Users : Entity
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string UserName { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
