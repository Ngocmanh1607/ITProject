using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITProject_form.DB;

namespace ITProject_form.BS
{
    internal class BLLogin
    {
        Data db = null;
        public BLLogin()
        {
            db = new Data();
        }
        public bool check(string Id, string Pass)
        {
            string query = "SELECT COUNT(*) FROM tblLOGIN WHERE TenDN = '"+Id+"' AND MatKhau = '"+Pass+"'";
            object result = db.ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
            {
                string temp = result.ToString();
                if (int.Parse(temp) == 1)
                {
                    return true;
                }
                else
                    return false;
            }

            return false;
        }
    }
}
