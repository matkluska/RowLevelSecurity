﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RowLevelSecurity.Models;

namespace RowLevelSecurity.Company
{
    public class Earnings : SecuredEntity
    {
        [Key]
        public int EarningId { get; set; }
        public double Value { get; set; }
        public int EmployeeRefId { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("EmployeeRefId")]
        public virtual Employee Employee { get; set; }
    }
}
