using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Core.BaseEntities
{
    public class BaseEntityDto
    {
        [Display(ResourceType = typeof(Resources.DisplayLabels), Name = "Id")]
        public int Id { get; set; }
        [Display(ResourceType = typeof(Resources.DisplayLabels), Name = "IsActive")]
        public bool IsActive { get; set; }
        [Display(ResourceType = typeof(Resources.DisplayLabels), Name = "IsDeleted")]
        public bool IsDeleted { get; set; }
        [Display(ResourceType = typeof(Resources.DisplayLabels), Name = "CreatedAt")]
        public System.DateTime CreatedAt { get; set; }
        [Display(ResourceType = typeof(Resources.DisplayLabels), Name = "ModifiedAt")]
        public DateTime? ModifiedAt { get; set; }
    }
}
