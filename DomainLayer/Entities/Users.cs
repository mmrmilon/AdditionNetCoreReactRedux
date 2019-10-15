using System;

namespace DomainLayer.Entities
{
    public class Users : Entity
    {
        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
