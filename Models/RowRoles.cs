using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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