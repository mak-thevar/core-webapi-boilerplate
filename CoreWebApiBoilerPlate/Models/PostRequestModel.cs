using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Models
{
    public class PostRequestModel
    {
        [Required]
        [StringLength(150)]
        [MinLength(4)]
        public string PostTitle { get; set; }
        [Required]
        [MinLength(50)]
        public string PostBody { get; set; }
        [DataType(DataType.ImageUrl)]
        public string PostImage { get; set; }
    }
}
