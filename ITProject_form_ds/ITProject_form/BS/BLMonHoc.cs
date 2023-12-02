using ITProject_form.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITProject_form.DB;

namespace ITProject_form.BS
{
    internal class BLMonHoc
    {
        Data db = null;
        public BLMonHoc()
        {
            db = new Data();
        }
        public DataSet TTMH()
        {
            return db.ExecuteQueryDataSet("select * from  tblMON", CommandType.Text);
        }
        public DataSet TTMH(string tt)
        {
            return db.ExecuteQueryDataSet("select "+tt+" from  tblMON", CommandType.Text);
        }
        public bool ThemMH(string maMH, string tenMH, string maGv,string maKhoa,int sotc, ref string err)
        {
            string sqlString = "Insert Into tblMON Values('" + maMH + "',N'" + tenMH + "','" + maGv + "','" + maKhoa + "','" + sotc + "')";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
        public bool CapNhatMH(string maMH, string tenMH, string maGv, string maKhoa, int sotc, ref string err)
        {
            string sqlString = "Update tblMON Set TenMon = N'" + tenMH + "', MaGV = '" + maGv + "', MaKhoa = '" + maKhoa + "', SoTC = '" + sotc + "' Where MaMon = '" + maMH + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
        public bool XoaMH(ref string err, string maMH)
        {
            string sqlString = "Delete from tblMON Where MaMon='" + maMH + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
    }
}
