using System.ComponentModel.DataAnnotations;
using RowLevelSecurity.Model;

namespace RowLevelSecurity.Example
{
//    [SecurityAspect]
    public class Blog : SecuredEntity
    {
        [Key]
        public int BlogId { get; set; }

        public string Name { get; set; }

        //        public virtual ICollection<Post> Posts { get; set; }
    }
}