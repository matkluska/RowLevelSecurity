using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using RowLevelSecurity.Context;

namespace RowLevelSecurity.Company
{
    [Table("CompanyContext")]
    public class CompanyContext : RowSecurityContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Earnings> Earningses { get; set; }
    }
}