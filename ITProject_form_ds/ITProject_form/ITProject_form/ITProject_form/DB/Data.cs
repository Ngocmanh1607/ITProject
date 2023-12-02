using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITProject_form.DB
{
    internal class Data
    {
        public static string ConString;
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private SqlDataAdapter adapter = null;
        public Data()
        {
            ConString = "Data Source=DESKTOP-F99BSJ5\\MSSQLSERVER01;Initial Catalog=Quanlydiem;Integrated Security=True";
            con = new SqlConnection(ConString);
            cmd = con.CreateCommand();
        }
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            // con.Open();
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        public bool MyExecuteNonQuery(string strSQL, CommandType ct, ref string error)
        {
            bool f = false;
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            try
            {
                cmd.ExecuteNonQuery();
                f = true;
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return f;
        }
        public object ExecuteScalar(string query)
        {

            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            using (SqlCommand command = new SqlCommand(query, con))
            {
                return command.ExecuteScalar();
            }
        }
    }
}
