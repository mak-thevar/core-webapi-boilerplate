namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities.Base
{

    public interface ICreationTracking
    {
        DateTime CreatedOn { get; set; }
        int CreatedById { get; set; }
        User CreatedBy { get; set; }
    }

    public interface IModificationTracking
    {
        DateTime? ModifiedOn { get; set; }
        int? ModifiedById { get; set; }
        User? ModifiedBy { get; set; }
    }

    public interface IAuditedEntity : ICreationTracking, IModificationTracking { }

    public abstract class AuditedEntityBase : EntityBase, IAuditedEntity
    {
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }
        public virtual User? ModifiedBy { get; set; }
    }
}
