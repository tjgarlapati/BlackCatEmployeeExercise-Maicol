using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Employees.Models.Domain
{
    public class EmployeeAddresses
    {
        public int AddressID { get; set; }
        public int EmployeeID { get; set; }
        public string Address { get; set; }
        public  string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Employees Employee { get; set; }
    }
}