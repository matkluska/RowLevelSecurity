using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RowLevelSecurity.Models
{
    public abstract class SecuredEntity
    {
        protected SecuredEntity()
        {
            this.RowId = Guid.NewGuid();
        }

        public Guid RowId { get; set; }
    }
}