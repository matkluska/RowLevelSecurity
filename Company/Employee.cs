using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RowLevelSecurity.Models;

namespace RowLevelSecurity.Company
{
    public class Employee : SecuredEntity
    {
        public Employee()
        {
            this.EarningsList = new List<Earnings>();
        }

        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        [ForeignKey("EarningsRefId")]
        public virtual List<Earnings> EarningsList { get; set; }
    }
}
