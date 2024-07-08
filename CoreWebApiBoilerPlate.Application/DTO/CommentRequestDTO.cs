using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.Application.DTO
{
    public record CommentRequestDTO(
        [StringLength(500,MinimumLength = 1)] string CommentText, 
        int todoId);
}
