using CoreWebApiBoilerPlate.DataLayer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.DataLayer.Entities
{
    public class Comment : EntityBase, IAuditedEntity
    {
        [StringLength(500, MinimumLength = 1)]
        public string CommentText { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }
        public virtual User? ModifiedBy { get; set; }
        public int TodoId { get; set; }
        public virtual Todo Todo { get; set; } = null!;
    }
}
