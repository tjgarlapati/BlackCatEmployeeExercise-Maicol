using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employees.Models.Domain
{
    public class Employees
    {
        public int ID { get; set; }
        public string BadgeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
}
}