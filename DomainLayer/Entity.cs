using System.ComponentModel.DataAnnotations;

namespace DomainLayer
{
    public class Entity
    {
        [Key]
        public long Id { get; set; }
    }
}
