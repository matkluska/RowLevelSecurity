using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFramework.DynamicFilters;
using RowLevelSecurity.Model;

namespace RowLevelSecurity.Context
{
    public abstract class RowSecurityContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RowRoles> RowRoles { get; set; }
        private string _userName;

        protected RowSecurityContext()
        {
            this.InitializeDynamicFilters();
        }

        protected RowSecurityContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            this.InitializeDynamicFilters();
        }

        protected sealed override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("SecuredByRole",
                (SecuredEntity securedEntity, IEnumerable<Guid> userRows) => userRows.Contains(securedEntity.RowId),
                (RowSecurityContext context) => context.GetUserRowIds(_userName));
            modelBuilder.EnableFilter("IfNotAdmin", (RowSecurityContext context) => !context.IsAdmin());

            base.OnModelCreating(modelBuilder);
        }

        public void SetUsername(string username)
        {
            _userName = username;
        }

        public string GetUsername()
        {
            return _userName;
        }

        public IEnumerable<string> GetUserRoles(string userName) {
            var user = Users.First(u => u.UserName == userName);
            var roles = user.Roles.Select(r => r.RoleId);
            return roles.Concat(GetChildRoleIds(roles)).Distinct();
        }

        public IEnumerable<string> GetChildRoleIds(IEnumerable<string> roleParentIds) {
            var childRoleIds =
                Roles.Where(r => roleParentIds.Contains(r.ParentId)).Select(r => r.RoleId).AsEnumerable();
            if (childRoleIds.Any())
                return childRoleIds.Concat(GetChildRoleIds(childRoleIds));
            return childRoleIds;
        }

        public IEnumerable<Guid> GetUserRowIds(string username) {
            var roles = GetUserRoles(username);
            return RowRoles.Where(r => roles.Contains(r.RoleId)).Select(r => r.RowId);
        }

        public bool IsAdmin()
        {
            return GetUserRoles(_userName).Contains("Admin");
        }

        public void AddRoleToRow(Guid rowId, string roleId)
        {
            if (GetUserRoles(_userName).Contains(roleId))
            {
                RowRoles.Add(new RowRoles {RoleId = roleId, RowId = rowId});
            }
        }

        public void AddRoleToUser(string userNameToRole, string roleId)
        {
            var user = Users.FirstOrDefault(u => u.UserName == userNameToRole);
            if (user == null)
            {
                return;
            }
            var role = new Role {RoleId = roleId};

            Roles.Attach(role);

            user.Roles.Add(role);

            SaveChanges();
        }

    }
}