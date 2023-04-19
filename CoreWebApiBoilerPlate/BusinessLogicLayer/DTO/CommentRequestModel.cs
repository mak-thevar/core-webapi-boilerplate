using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.BusinessLogicLayer.DTO
{
    public class CommentRequestModel
    {
        [StringLength(500, MinimumLength = 1)]
        public string CommentText { get; set; } = null!;
    }
}
