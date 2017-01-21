using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using RowLevelSecurity.Context;

namespace RowLevelSecurity.Company
{
    internal class Company
    {
        public static void PrintMenu(CompanyContext c)
        {
            Console.WriteLine("Zaloguj się do systemu: ");
            Console.WriteLine("1. Boss");
            Console.WriteLine("2. Accountant");
            Console.WriteLine("3. Programist");
            Console.WriteLine("4. Intern");
            Console.WriteLine("0. Exit");
            var res = Console.ReadLine();
            switch (res)
            {
                case "1":
                    c.SetUsername("Boss_");
                    break;
                case "2":
                    c.SetUsername("Accountant_");
                    break;
                case "3":
                    c.SetUsername("Programist_");
                    break;
                case "4":
                    c.SetUsername("Intern_");
                    break;
                case "0":
                    System.Environment.Exit(1); ;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Niepoprawny wybór");
                    PrintMenu(c);
                    break;
            }

        }

        public static void PrintEmployeeEarnings()
        {
            using (var context = new CompanyContext())
            {
                Database.SetInitializer(new InitializeData());

                PrintMenu(context);

                
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
                    { 
                        Console.WriteLine(
                            "{0,-10} {1,-10} {2,-4} {3,-5} {4,-3} {5,-10}",
                            e.EmployeeRefId,
                            e.Value,
                            e.Date.Year,
                            e.Date.Month,
                            e.Date.Day,
                            e.EarningId);
                    }
                    Console.WriteLine();
                }
            }
        }
        public static void Main(string[] args)
        {
            while (true)
            {
                PrintEmployeeEarnings();
            }
        }
    }
}
