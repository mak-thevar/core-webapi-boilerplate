namespace CoreWebApiBoilerPlate.BusinessLogicLayer.DTO
{
    public class RoleResponseModel
    {
        public RoleResponseModel()
        {
            RoleAccess = new List<RoleAccessResponseModel>();
        }
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public bool IsActive { get; set; }
        public List<RoleAccessResponseModel> RoleAccess { get; set; }
    }


    public class RoleAccessResponseModel
    {
        public string MenuDescription { get; set; } = null!;
        public int MenuId { get; set; }
    }
}
