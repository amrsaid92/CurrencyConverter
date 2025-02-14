using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Core.BaseEntities
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
