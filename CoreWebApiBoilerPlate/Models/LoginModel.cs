using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Models
{
    public class LoginModel
    {
        [Required]
        [MinLength(3)]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        [MinLength(4)]
        [StringLength(200)]
        public string Password { get; set; }
    }
}
