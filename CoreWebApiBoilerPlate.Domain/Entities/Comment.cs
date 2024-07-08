using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities
{
    public class Comment : AuditedEntityBase
    {
        [StringLength(500, MinimumLength = 1)]
        public string CommentText { get; set; } = null!;
        public int TodoId { get; set; }
        public virtual Todo Todo { get; set; } = null!;
    }
}
