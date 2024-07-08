using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities
{
    public class Role : EntityBase
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [StringLength(100, MinimumLength = 3)]
        public string Description { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
