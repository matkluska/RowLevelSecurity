using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RowLevelSecurity.Model
{
    public abstract class SecuredEntity
    {
        protected SecuredEntity()
        {
            this.RowId = Guid.NewGuid();
        }

        [Required]
        public Guid RowId { get; set; }
    }
}