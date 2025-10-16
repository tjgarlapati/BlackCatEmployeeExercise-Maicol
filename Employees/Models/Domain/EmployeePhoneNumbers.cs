using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employees.Models.Domain
{
    public class EmployeePhoneNumbers
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public Employees Employee { get; set; }
    }
}