using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RowLevelSecurity.Models
{
    public class User
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
        }

        public int UserId { get; set; }

        [Required]
        [Column("Name")]
        [MinLength(3)]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        public virtual ICollection<Role> Roles { get; set; }


    }
}