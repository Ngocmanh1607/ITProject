using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITProject_form.DB;

namespace ITProject_form.BS
{
    internal class BLKhoa
    {
        Data db=null;
        public BLKhoa()
        {
            db = new Data();
        }
        public DataSet TTKhoa()
        {
            return db.ExecuteQueryDataSet("select * from  tblKHOA", CommandType.Text);
        }
        public DataSet LMKhoa()
        {
            return db.ExecuteQueryDataSet("select MaKhoa from  tblKHOA", CommandType.Text);
        }
        public bool ThemKhoa(string maKhoa, string tenKhoa, ref string err)
        {
            string sqlString = "Insert Into tblKHOA Values(" + "'" +
            maKhoa + "',N'" +
            tenKhoa + "')";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
        public bool CapNhatKhoa(string maKhoa, string tenKhoa, ref string err)
        {
            string sqlString = "Update tblKHOA Set TenKhoa=N'" +
            tenKhoa + "' Where MaKhoa='" + maKhoa + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
        public bool XoaKhoa(ref string err, string maKhoa)
        {
            string sqlString = "Delete from tblKHOA Where MaKhoa='" + maKhoa + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
    }
}
