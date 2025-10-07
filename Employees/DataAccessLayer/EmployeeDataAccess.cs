using Employees.DataAccessLayer.Data;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employees.DataAccessLayer
{
    public class EmployeeDataAccess
    {
        private readonly BlackCatEmployeesEntities _context;

        public EmployeeDataAccess()
        {
            _context = new BlackCatEmployeesEntities();
        }

        //---------------Build your Data Access Methods To Get Data From Database----------------------
    }
}