using CoreWebApiBoilerPlate.Domain.Entities.Enums;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities
{
    public class Todo : AuditedEntityBase, IStatusEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Title { get; set; } = null!;
        [StringLength(2000)]
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }

        [EnumDataType(typeof(TodoStatusEnum))]
        public TodoStatusEnum Status { get; set; } = TodoStatusEnum.NotStarted;
        public virtual ICollection<Comment> Comments { get; set; }

        public Todo()
        {
            Comments = new HashSet<Comment>();
        }
    }
}
