using System;
using System.Data.Entity;
using RowLevelSecurity.Aspect;

namespace RowLevelSecurity.Company
{

    internal class Company
    {
        public static void Main(string[] args) {
            using (var context = new CompanyContext()) {
                Database.SetInitializer(new InitializeData());

                Console.WriteLine("Zaloguj się do systemu: ");
                Console.WriteLine("1. wersja 1");
                Console.WriteLine("2. wersja 2");
                Console.WriteLine("0. Exit");

                var res = Console.ReadLine();
                switch (res) {
                    case "1":
                        while (true)
                            PrintEmployeeEarnings(context);
                    case "2":
                        while (true) {
                            var username = PrintMenu(context);
                            PrintEmployeeEarnings(username, context);
                        }
                    case "0":
                        Environment.Exit(1);
                        break;
                }
            }
        }

        public static void PrintEmployeeEarnings(CompanyContext context) {
            context.Authorize(PrintMenu(context));

            PrintData(context);
        }

        [Authorize]
        public static void PrintEmployeeEarnings(string userName, CompanyContext context) {
            PrintData(context);
        }
        public static string PrintMenu(CompanyContext c)
        {
            while (true)
            {
                Console.WriteLine("Zaloguj się do systemu: ");
                Console.WriteLine("1. Boss");
                Console.WriteLine("2. Accountant");
                Console.WriteLine("3. Programist");
                Console.WriteLine("4. Intern");
                Console.WriteLine("5. Test User 1");
                Console.WriteLine("6. Test User 2");
                Console.WriteLine("0. Exit");
                var res = Console.ReadLine();
                switch (res)
                {
                    case "1":
                        return "Boss_login";
                    case "2":
                        return "Accountant_login";
                    case "3":
                        return "Programist_login";
                    case "4":
                        return "Intern_login";
                    case "5":
                        return "Test_login1";
                    case "6":
                        return "Test_login2";
                    case "0":
                        Environment.Exit(1);
                        ;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Niepoprawny wybór");
                        continue;
                }
            }
        }

        private static void PrintData(CompanyContext context) {
            foreach (var contextEmp in context.Employees) {
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
        }
    }
}