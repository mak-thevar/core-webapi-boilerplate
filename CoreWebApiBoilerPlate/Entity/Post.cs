using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Entity
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        [StringLength(150)]
        public string PostTitle { get; set; }
        [Required]
        public string PostBody { get; set; }
        public string PostImage { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        public virtual User User { get; set; }
    }
}
