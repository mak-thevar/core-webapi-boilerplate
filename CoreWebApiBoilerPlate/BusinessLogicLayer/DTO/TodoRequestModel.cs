using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.BusinessLogicLayer.DTO
{
    public class TodoRequestModel
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Title { get; set; } = null!;
        [StringLength(2000)]
        public string Description { get; set; } = null!;
    }
}
