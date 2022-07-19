namespace CoreWebApiBoilerPlate.Models
{
    public class RoleRequestModel
    {
        public string Description { get; set; } = null!;
        public List<int> MenuIds { get; set; } = null!;
    }
}
