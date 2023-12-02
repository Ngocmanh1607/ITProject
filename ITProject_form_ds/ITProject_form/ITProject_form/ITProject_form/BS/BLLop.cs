using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITProject_form.DB;

namespace ITProject_form.BS
{
    internal class BLLop
    {
        Data db = null;
        public BLLop()
        {
            db = new Data();
        }
        public DataSet TTLop()
        {
            return db.ExecuteQueryDataSet("select * from  tblLOP", CommandType.Text);
        }
        public DataSet TMLop()
        {
            return db.ExecuteQueryDataSet("select MaLop from  tblLOP", CommandType.Text);
        }
        public bool ThemLop(string maKhoa,string maLop, string tenLop, ref string err)
        {
            string sqlString = "Insert Into tblLOP Values('" + maKhoa + "','" + maLop + "',N'" + tenLop + "')";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
        public bool CapNhatLop(string maKhoa, string maLop,string tenLop, ref string err)
        {
            string sqlString = "Update tblLOP Set MaKhoa='" + maKhoa + "', TenLop='" + tenLop + "' Where MaLop='" + maLop + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
        public bool XoaLop(ref string err, string maLop)
        {
            string sqlString = "Delete from tblLOP Where MaLop='" + maLop + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
    }
}
