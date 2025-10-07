using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Employees.DataAccessLayer
{
    public class SqlHelper
    {
        //-------------------------Replace YourConnectionString with the ConnectionString in Web.config-----------------------------
        private static string strConn = ConfigurationManager.ConnectionStrings["EmployeesConnectionString"].ConnectionString;

        public static object ExecuteSP(string spName, SqlParameter[] p)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            FillParameters(cmd, p);
            con.Open();
            object retval = cmd.ExecuteScalar();
            con.Close();
            con.Dispose();
            return retval;
        }
        public static object ExecuteSP(string spName)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            object retval = cmd.ExecuteScalar();
            con.Close();
            con.Dispose();
            return retval;
        }
        public static object ExecuteSPWithOutPut(string spName, SqlParameter[] p)
        {
            string outputName = "";
            foreach (SqlParameter par in p)
            {
                if (par.Direction == ParameterDirection.Output)
                    outputName = par.ParameterName;
            }
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            FillParameters(cmd, p);
            con.Open();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

            object outputObject = cmd.Parameters[outputName].Value.ToString();
            con.Close();
            con.Dispose();
            return outputObject;
        }
        public static SqlDataReader ExecuteSPDataReader(string spName, SqlParameter[] p)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            FillParameters(cmd, p);
            con.Open();
            return cmd.ExecuteReader();
        }

        public static SqlDataReader ExecuteSPDataReader(string spName)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            return cmd.ExecuteReader();
        }

        public static int ExecuteNonQuery(string query)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandTimeout = 1200;
            con.Open();
            int retval = cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return retval;
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] p)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(query, con);
            FillParameters(cmd, p);
            con.Open();
            int retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            con.Close();
            con.Dispose();
            return retval;
        }

        public static DataTable ExecuteDataTable(string sql)
        {

            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            con.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            con.Dispose();
            return dt;
        }

        public static DataTable ExecuteDataTable(string sql, SqlParameter[] p)
        {

            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, con);
            FillParameters(cmd, p);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            con.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.Parameters.Clear();
            con.Close();
            con.Dispose();
            return dt;
        }

        public static SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static SqlDataReader ExecuteReader(string sql, SqlParameter[] p)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, con);
            FillParameters(cmd, p);
            con.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static object ExecuteScalar(string sql)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            object retval = cmd.ExecuteScalar();
            con.Close();
            con.Dispose();
            return retval;
        }

        public static object ExecuteScalar(string sql, SqlParameter[] p)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, con);
            FillParameters(cmd, p);
            con.Open();
            object retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            con.Close();
            con.Dispose();
            return retval;
        }

        public static DataSet ExecuteDataSet(string sql)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            con.Dispose();
            return ds;
        }

        public static DataSet ExecuteDataSet(string sql, SqlParameter[] p)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                FillParameters(cmd, p);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                try
                {
                    conn.Open();
                    da.Fill(ds);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return ds;
        }

        private static void FillParameters(SqlCommand cmd, SqlParameter[] parameters)
        {
            int i = 0;
            while (!(i == parameters.Length))
            {
                cmd.Parameters.Add(parameters[i]);
                i = i + 1;
            }
        }

        public static Object DBNullValue(object obj)
        {
            return obj == System.DBNull.Value ? null : obj;
        }

        public static string QuoteReplace(string sql)
        {
            string val = sql.Replace("'", "''");
            return val;
        }

        public static Expression<Func<T, object>> GetLambdaExpressionForSortProperty<T>(string propertyname) where T : class
        {
            Expression member = null;
            var propertyParts = propertyname.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            var param = Expression.Parameter(typeof(T), "arg");
            Expression previous = param;

            foreach (string s in propertyParts)
            {
                member = Expression.Property(previous, s);
                previous = member;
            }

            if (member.Type.IsValueType)
            {
                member = Expression.Convert(member, typeof(object));
            }

            return Expression.Lambda<Func<T, object>>(member, param);
        }

        public static DataTable ExecuteSPDataTable(string spName, SqlParameter[] p)
        {
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            FillParameters(cmd, p);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }
    }
}