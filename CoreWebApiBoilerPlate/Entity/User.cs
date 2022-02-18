using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Entity
{
    public class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(60)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(60)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(60)]
        [MinLength(4)]
        public string Password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool? IsActive { get; set; }
        public string Location { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
