using RowLevelSecurity.Models;
using System.Data.Entity;

namespace RowLevelSecurity.Example
{
    public class InitializeBlogData : DropCreateDatabaseAlways<ExampleContext>
    {
        protected override void Seed(ExampleContext context)
        {
            var user1 = new User {UserName = "Mati"};
            var user2 = new User {UserName = "Miki"};
            var role1 = new Role {RoleId = "SuperAdmin"};
            var role2 = new Role {RoleId = "Manager", ParentId = role1.RoleId};
            var role3 = new Role {RoleId = "NormalUser", ParentId = role2.RoleId};

            user1.Roles.Add(role1);
            //user1.Roles.Add(role2);
            user2.Roles.Add(role2);
            context.Roles.Add(role3);

            var blog = new Blog {Name = "MyHome"};
            context.Blogs.Add(blog);
            context.RowRoles.Add(new RowRoles
            {
                RowId = blog.RowId,
                RoleId = role2.RoleId
            });

            var blog2 = new Blog {Name = "OnlyAdmin"};
            context.Blogs.Add(blog2);
            context.RowRoles.Add(new RowRoles
            {
                RowId = blog2.RowId,
                RoleId = role1.RoleId
            });

            context.Users.Add(user1);
            context.Users.Add(user2);
        }
    }
}