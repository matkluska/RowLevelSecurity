using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RowLevelSecurity.Model
{
    public class Role
    {
        public Role()
        {
            this.Users = new HashSet<User>();
        }

        [Key]
        [MinLength(3)]
        [MaxLength(20)]
        public string RoleId { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        public string ParentId { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}