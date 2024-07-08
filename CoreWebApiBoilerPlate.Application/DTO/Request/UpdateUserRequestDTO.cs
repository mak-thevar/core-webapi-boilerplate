using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Application.DTO.Request
{
    public record UpdateUserRequestDTO
    (
        [Required]
        [StringLength(100, MinimumLength = 3)]
        string Name,

        [Required]
        [StringLength(100, MinimumLength = 3)]
        string Username,

        [Required]
        [DataType(DataType.EmailAddress)]
        string EmailId
    );
}
