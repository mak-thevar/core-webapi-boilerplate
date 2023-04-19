using CoreWebApiBoilerPlate.DataLayer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.DataLayer.Entities
{
    public class Todo : Base.EntityBase, IAuditedEntity, IStatusEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Title { get; set; } = null!;
        [StringLength(2000)]
        public string Description { get; set; } = null!;
        public bool IsActive { get ; set ; }
        public virtual TodoStatus TodoStatus { get; set; } = null!;
        public int TodoStatusId { get; set; }
        public DateTime CreatedOn { get ; set ; }
        public int CreatedById { get ; set ; }
        public virtual User CreatedBy { get; set; } = null!;
        public DateTime? ModifiedOn { get ; set ; }
        public int? ModifiedById { get ; set ; }
        public virtual User? ModifiedBy { get ; set ; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Todo()
        {
            Comments = new HashSet<Comment>();
        }
    }
}
