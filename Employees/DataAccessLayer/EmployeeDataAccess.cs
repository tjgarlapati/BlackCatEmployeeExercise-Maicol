using System.Data.Entity;
using Employees.DataAccessLayer.Data;

namespace Employees.DataAccessLayer
{
    public class EmployeeDataAccess : DbContext
    {
        private readonly BlackCatEmployeesEntities _context;

        public EmployeeDataAccess()
        {
            _context = new BlackCatEmployeesEntities();
        }

        //---------------Build your Data Access Methods To Get Data From Database----------------------
        //---------------Use Entity Framework or ADO.Net based on your comfort levels------------------
        //---------------If you decide to use ADO.Net, you could use helper methods setup in SqlHelper.cs----------
        public DbSet<Employee> employees { get; set; }
        public DbSet<EmployeePhoneNumber> employeePhoneNumbers { get; set; }
        public DbSet<EmployeeAddress> employeeAddresses { get; set; }
    }
}