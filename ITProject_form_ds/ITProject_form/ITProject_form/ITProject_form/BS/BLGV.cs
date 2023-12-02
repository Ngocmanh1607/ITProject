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
    internal class BLGV
    {
        Data db = null;
        public BLGV()
        {
            db = new Data();
        }
        public DataSet TTGV()
        {
            return db.ExecuteQueryDataSet("select * from  tblGIANG_VIEN", CommandType.Text);
        }
        public DataSet LMGV()
        {
            return db.ExecuteQueryDataSet("select MaGv from  tblGIANG_VIEN", CommandType.Text);
        }
        public bool ThemGv(string maGv, string tenGv, string gioiTinh,int sdt,string email,string phanloai, ref string err)
        {
            string sqlString = "Insert Into tblGIANG_VIEN Values('" + maGv + "',N'" + tenGv + "',N'" + gioiTinh + "','"+sdt+"','"+email+"',N'"+phanloai+"')";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
        public bool CapNhatGv(string maGv, string tenGv, string gioiTinh, string sdt, string email, string phanloai, ref string err)
        {
            string sqlString = "Update tblGIANG_VIEN Set TenGV=N'" + tenGv + "', GioiTinh=N'" + gioiTinh + "', Phone='" + sdt + "', Email='" + email + "', PhanLoaiGV=N'" + phanloai + "' Where MaGV='" + maGv + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
        public bool XoaGv(ref string err, string maGv)
        {
            string sqlString = "Delete from tblGIANG_VIEN Where MaGV='" + maGv + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
    }
}
