using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities.Base;

namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities
{
    public partial class User : EntityBase, IStatusEntity
    {
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public int RoleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }
        public virtual User? ModifiedBy { get; set; }
        public virtual Role Role { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
