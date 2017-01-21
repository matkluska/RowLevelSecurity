using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using RowLevelSecurity.Models;
using EntityFramework.DynamicFilters;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("SecuredByRole",
                (SecuredEntity securedEntity, IEnumerable<Guid> userRows) => userRows.Contains(securedEntity.RowId),
                () => new List<Guid>());

            base.OnModelCreating(modelBuilder);
        }

        public void SetUsername(string username)
        {
            _userName = username;
            var userRowIds = GetUserRowIds(username);
            this.SetFilterScopedParameterValue("SecuredByRole", "userRows", userRowIds);
            this.SetFilterGlobalParameterValue("SecuredByRole", "userRows", userRowIds);
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

        public void AddRoleToRow(Guid rowId, string roleId)
        {
            RowRoles.Add(new RowRoles {RoleId = roleId, RowId = rowId});
        }

        public void AddRoleToUser(string userName, string roleId)
        {
            var user = Users.First(u => u.UserName == userName);
            var role = new Role {RoleId = roleId};

            Roles.Attach(role);

            user.Roles.Add(role);

            SaveChanges();
        }

    }
}