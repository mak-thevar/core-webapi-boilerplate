using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.Application.DTO.Request
{
    public record NewUserRequestDTO
    (

        [Required]
        [StringLength(150, MinimumLength = 6)]
        [DataType(DataType.Password)]
        string Password,

        [Required]
        [StringLength(100, MinimumLength = 3)]
        string Name,

        [Required]
        [StringLength(100, MinimumLength = 3)]
        string Username,

        [Required]
        [DataType(DataType.EmailAddress)]
        string EmailId
    )
    {
        [Required]
        [StringLength(150, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
