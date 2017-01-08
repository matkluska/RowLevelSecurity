using System.Data.Entity;
using RowLevelSecurity.Model;

namespace RowLevelSecurity.Example
{
    public class InitializeBlogData : DropCreateDatabaseAlways<ExampleContext>
    {
        protected override void Seed(ExampleContext context)
        {
            var user1 = new User {UserName = "Mati"};
            var user2 = new User {UserName = "Miki"};
            var user3 = new User {UserName = "Pieter"};
            var role1 = new Role {RoleId = "Admin"};
            var role2 = new Role {RoleId = "Manager", ParentId = role1.RoleId};
            var role3 = new Role {RoleId = "NormalUser", ParentId = role2.RoleId};

            user1.Roles.Add(role1);
            user2.Roles.Add(role3);
            user3.Roles.Add(role3);
            context.Roles.Add(role2);

            var blog = new Blog {Name = "ManagerAccess"};
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

            var blog3 = new Blog { Name = "ForAll" };
            context.Blogs.Add(blog3);
            context.RowRoles.Add(new RowRoles {
                RowId = blog3.RowId,
                RoleId = role3.RoleId
            });

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
        }
    }
}