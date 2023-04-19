namespace CoreWebApiBoilerPlate.BusinessLogicLayer.DTO
{
    public class Constants
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUserName { get; set; } = "admin";
        public static int CurrentRoleId { get; internal set; }
    }
}
