using System;

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