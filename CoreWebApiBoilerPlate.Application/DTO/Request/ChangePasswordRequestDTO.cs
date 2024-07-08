using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Application.DTO.Request
{
    public record ChangePasswordRequestDTO(
        [Required]
        [StringLength(150, MinimumLength = 6)]
        [DataType(DataType.Password)] 
        string OldPassword, 
        
        [Required]
        [StringLength(150, MinimumLength = 6)]
        [DataType(DataType.Password)] 
        string NewPassword
        )
    {
        [Required]
        [StringLength(150, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
