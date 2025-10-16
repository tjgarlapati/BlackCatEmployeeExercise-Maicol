using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Employees.BusinessLogicLayer
{
    public class EmployeeBusinessLogic
    {
		private readonly string conn = ConfigurationManager.ConnectionStrings["EmployeesConnectionString"].ConnectionString;

        private void LoadEmployees()
        {
			try
			{
				DataTable dataTable = new DataTable();
				using (SqlConnection cn = new SqlConnection(conn))
				using (SqlCommand cmd = new SqlCommand("SP_GET_ALL_EMPLOYEE", cn))

			}
			catch (Exception ex)
			{

				throw ex;
			}
        }
    }
}