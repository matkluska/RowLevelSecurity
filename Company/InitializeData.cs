using System;
using System.Data.Entity;
using RowLevelSecurity.Model;

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
                Role = r.RoleId
            };
            c.Employees.Add(emp);

            c.RowRoles.Add(new RowRoles
            {
                RowId = emp.RowId,
                RoleId = r.RoleId
            });

            return emp;
        }

        public Earnings CreateEar(double val, DateTime d, Employee e, CompanyContext c, Role r)
        {
            var e1 = new Earnings
            {
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
            var user1 = new User {Login = "Boss_login"};
            var user2 = new User {Login = "Accountant_login"};
            var user3 = new User {Login = "Programist_login"};
            var user4 = new User {Login = "Intern_login"};

            //test users
            var testUser1 = new User { Login = "Test_login1" };
            var testUser2 = new User { Login = "Test_login2" };

            var role1 = new Role {RoleId = "CEO"};
            var role2 = new Role {RoleId = "Accountant", ParentId = role1.RoleId};
            var role3 = new Role {RoleId = "Programist", ParentId = role2.RoleId};
            var role4 = new Role {RoleId = "Intern", ParentId = role3.RoleId};

            //test roles
            var testRole1 = new Role() { RoleId = "Test1" };
            var testRole2 = new Role() { RoleId = "Test2", ParentId = testRole1.RoleId };

            user1.Roles.Add(role1);
            user2.Roles.Add(role2);
            user3.Roles.Add(role3);
            user4.Roles.Add(role4);

            //test users - adding role to user
            testUser1.Roles.Add(testRole1);
            testUser2.Roles.Add(testRole2);

            var em1 = CreateEmp("Adam", "Adamski", "CEO", context, role1);
            var em2 = CreateEmp("Jan", "Kowalski", "Accountant", context, role2);
            var em31 = CreateEmp("Jan", "Janowski", "Programist", context, role3);
            var em32 = CreateEmp("Paweł", "Pawłowski", "Programist", context, role3);
            var em33 = CreateEmp("Alan", "Alanowski", "Programist", context, role3);
            var em4 = CreateEmp("Patryk", "Stażysta", "Intern", context, role4);

            //test users employee
            var t1 = CreateEmp("Test1", "Test1S", "Tester1", context, testRole1);
            var t2 = CreateEmp("Test2", "Test2S", "Tester2", context, testRole2);

            CreateEar(10000.0, new DateTime(2016, 1, 10), em1, context, role1);
            CreateEar(15000.0, new DateTime(2016, 2, 10), em1, context, role1);
            CreateEar(16000.0, new DateTime(2016, 3, 10), em1, context, role1);

            CreateEar(6000.0, new DateTime(2016, 1, 10), em2, context, role2);
            CreateEar(7000.0, new DateTime(2016, 2, 10), em2, context, role2);
            CreateEar(8000.0, new DateTime(2016, 3, 10), em2, context, role2);

            CreateEar(6500.0, new DateTime(2016, 1, 10), em31, context, role3);
            CreateEar(7500.0, new DateTime(2016, 2, 10), em31, context, role3);
            CreateEar(8500.0, new DateTime(2016, 3, 10), em31, context, role3);

            CreateEar(6510.0, new DateTime(2016, 1, 10), em32, context, role3);
            CreateEar(7510.0, new DateTime(2016, 2, 10), em32, context, role3);
            CreateEar(8510.0, new DateTime(2016, 3, 10), em32, context, role3);

            CreateEar(6530.0, new DateTime(2016, 1, 10), em33, context, role3);
            CreateEar(7530.0, new DateTime(2016, 2, 10), em33, context, role3);
            CreateEar(8530.0, new DateTime(2016, 3, 10), em33, context, role3);

            CreateEar(1000.0, new DateTime(2016, 1, 10), em4, context, role4);
            CreateEar(2000.0, new DateTime(2016, 2, 10), em4, context, role4);
            CreateEar(3000.0, new DateTime(2016, 3, 10), em4, context, role4);

            //test users earnings
            CreateEar(10.0, new DateTime(2013, 3, 2), t1, context, testRole1);
            CreateEar(70.0, new DateTime(2013, 6, 2), t1, context, testRole1);
            CreateEar(50.0, new DateTime(2014, 6, 2), t2, context, testRole2);
            CreateEar(120.0, new DateTime(2015, 6, 2), t2, context, testRole2);

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);

            //adding test users to context
            context.Users.Add(testUser1);
            context.Users.Add(testUser2);

            context.SaveChanges();
        }
    }
}