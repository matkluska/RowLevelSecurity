using System;
using System.Data.Entity;
using RowLevelSecurity.Company.Auth;

namespace RowLevelSecurity.Company
{
    internal class Company
    {
        public static void Main(string[] args)
        {
            using (var context = new CompanyContext())
            {
                while (true)
                {
                    Database.SetInitializer(new InitializeData());

                    Console.Clear();
                    Console.WriteLine("Zaloguj się do systemu: ");
                    Console.WriteLine("1. Normalna autentykacja");
                    Console.WriteLine("2. Autentykacja aspektowa");

                    var res = Console.ReadLine();
                    Authentication auth;

                    switch (res)
                    {
                        case "1":
                            auth = new NormalAuthentication();
                            break;
                        case "2":
                            auth = new AspectAuthentication();
                            break;
                        default:
                            Console.WriteLine("Niepoprawny wybór");
                            continue;
                    }

                    var user = PrintMenu(context);
                    auth.authenticate(user, context);
                    PrintData(context);
                }
            }
        }

        public static string PrintMenu(CompanyContext c)
        {
            UserFactory uf = new UserFactory();
            while (true)
            {
                Console.WriteLine("Zaloguj się do systemu: ");
                Console.WriteLine("1. Boss");
                Console.WriteLine("2. Accountant");
                Console.WriteLine("3. Programist");
                Console.WriteLine("4. Intern");
                Console.WriteLine("5. Test User 1");
                Console.WriteLine("6. Test User 2");
                var res = Console.ReadLine();
                var user = uf.getUser(res);
                if (user == null)
                {
                    Console.Clear();
                    Console.WriteLine("Niepoprawny wybór");
                    continue;
                }
                return user;
            }
        }

        private static void PrintData(CompanyContext context)
        {
            foreach (var contextEmp in context.Employees)
            {
                //printing employees table
                Console.WriteLine(
                    "{0,-4} {1,-10} {2,-15} {3,-10}",
                    "Id",
                    "Name",
                    "Surname",
                    "Role");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine(
                    "{0,-4} {1,-10} {2,-15} {3,-10}",
                    contextEmp.EmployeeId,
                    contextEmp.Name,
                    contextEmp.Surname,
                    contextEmp.Role);
                Console.WriteLine("------------------------------------------------");

                //printing earnings table
                Console.WriteLine(
                    "{0,-10} {1,-10} {2,-4} {3,-5} {4,-3} {5,-10}",
                    "EmployeeId",
                    "Value",
                    "Year",
                    "Month",
                    "Day",
                    "EarningId");
                Console.WriteLine("------------------------------------------------");
                foreach (var e in contextEmp.EarningsList)
                    Console.WriteLine(
                        "{0,-10} {1,-10} {2,-4} {3,-5} {4,-3} {5,-10}",
                        e.EmployeeRefId,
                        e.Value,
                        e.Date.Year,
                        e.Date.Month,
                        e.Date.Day,
                        e.EarningId);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}