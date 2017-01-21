using System;
using System.ComponentModel.DataAnnotations;

namespace RowLevelSecurity.Models
{
    public class RowRoles
    {
        [Required]
        [Key]
        public Guid RowId { get; set; }

        [Required]
        public string RoleId { get; set; }

    }
}