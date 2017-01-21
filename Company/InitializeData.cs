using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Security.Claims;
using RowLevelSecurity.Models;



namespace RowLevelSecurity.Company
{
    public class InitializeData : DropCreateDatabaseAlways<CompanyContext>
    {

        public Employee CreateEmp(string name, string surname, string role, CompanyContext c, Role r)
        {
            var emp = new Employee
            {
                Name = name,
                Surname = surname,
                Role = role
            };
            c.Employees.Add(emp);

            c.RowRoles.Add(new RowRoles
            {
                RowId = emp.RowId,
                RoleId = r.RoleId
            });

            return emp;
        }

        public Earnings CreateEar(double val,DateTime d, Employee e, CompanyContext c, Role r)
        {
            var e1 = new Earnings {
                Value = val,
                Date = d,
                Employee = e,
                EmployeeRefId = e.EmployeeId
            };

            c.Earningses.Add(e1);

            c.RowRoles.Add(new RowRoles
            {
                RowId = e1.RowId,
                RoleId = r.RoleId
            });

            return e1;
        }

        protected override void Seed(CompanyContext context)
        {
            var user1 = new User { UserName = "Boss_" };
            var user2 = new User { UserName = "Accountant_" };
            var user3 = new User { UserName = "Programist_" };
            var user4 = new User { UserName = "Intern_" };

            var role1 = new Role { RoleId = "CEO" };
            var role2 = new Role { RoleId = "Accountant", ParentId = role1.RoleId };
            var role3 = new Role { RoleId = "Programist", ParentId = role2.RoleId };
            var role4 = new Role { RoleId = "Intern", ParentId = role3.RoleId };

            user1.Roles.Add(role1);
            user2.Roles.Add(role2);
            user3.Roles.Add(role3);
            user4.Roles.Add(role4);

            var em1 = CreateEmp("Adam", "Adamski", "CEO", context, role1);
            var em2 = CreateEmp("Jan", "Kowalski", "Accountant", context, role2);
            var em31 = CreateEmp("Jan", "Janowski", "Programist", context, role3);
            var em32 = CreateEmp("Paweł", "Pawłowski", "Programist", context, role3);
            var em33 = CreateEmp("Alan", "Alanowski", "Programist", context, role3);
            var em4 = CreateEmp("Patryk", "Stażysta", "Intern", context, role4);

            var er11 = CreateEar(10000.0, new DateTime(2016,1,10),  em1, context, role1);
            var er12 = CreateEar(15000.0, new DateTime(2016, 2, 10), em1, context, role1);
            var er13 = CreateEar(16000.0, new DateTime(2016, 3, 10), em1, context, role1);

            var er21 = CreateEar(6000.0, new DateTime(2016, 1, 10), em2, context, role2);
            var er22 = CreateEar(7000.0, new DateTime(2016, 2, 10), em2, context, role2);
            var er23 = CreateEar(8000.0, new DateTime(2016, 3, 10), em2, context, role2);

            var er311 = CreateEar(6500.0, new DateTime(2016, 1, 10), em31, context, role3);
            var er312 = CreateEar(7500.0, new DateTime(2016, 2, 10), em31, context, role3);
            var er313 = CreateEar(8500.0, new DateTime(2016, 3, 10), em31, context, role3);

            var er321 = CreateEar(6510.0, new DateTime(2016, 1, 10), em32, context, role3);
            var er322 = CreateEar(7510.0, new DateTime(2016, 2, 10), em32, context, role3);
            var er323 = CreateEar(8510.0, new DateTime(2016, 3, 10), em32, context, role3);

            var er331 = CreateEar(6530.0, new DateTime(2016, 1, 10), em33, context, role3);
            var er332 = CreateEar(7530.0, new DateTime(2016, 2, 10), em33, context, role3);
            var er333 = CreateEar(8530.0, new DateTime(2016, 3, 10), em33, context, role3);

            var er41 = CreateEar(1000.0, new DateTime(2016, 1, 10), em4, context, role4);
            var er42 = CreateEar(2000.0, new DateTime(2016, 2, 10), em4, context, role4);
            var er43 = CreateEar(3000.0, new DateTime(2016, 3, 10), em4, context, role4);

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);

            context.SaveChanges();
        }
    }

    
}
