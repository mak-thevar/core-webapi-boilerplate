using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Application.DTO.Request
{
    public record LoginRequestDTO
    (
        [Required]
        [StringLength(100, MinimumLength = 3)]
        string UserName,

        [Required]
        [StringLength(100)]
        string Password
    );
}
