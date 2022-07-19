namespace CoreWebApiBoilerPlate.DataLayer.Entities.Base
{
    public interface IAuditedEntity
    {
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }
    }
}
