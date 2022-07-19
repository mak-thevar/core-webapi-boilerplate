using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.Models
{
    public class CommentRequestModel
    {
        [StringLength(500, MinimumLength = 1)]
        public string CommentText { get; set; } = null!;
    }
}
