using CoreWebApiBoilerPlate.DataLayer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.DataLayer.Entities
{
    public class Role : EntityBase, IStatusEntity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [StringLength(100,MinimumLength =3)]
        public string Description { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public int? CreatedById { get; set; }
        public virtual User? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }
        public virtual User? ModifiedBy { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
