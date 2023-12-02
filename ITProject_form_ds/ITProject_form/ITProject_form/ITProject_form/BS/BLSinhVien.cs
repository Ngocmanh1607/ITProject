using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITProject_form.DB;

namespace ITProject_form.BS
{
    internal class BLSinhVien
    {
        Data db = null;
        public BLSinhVien()
        {
            db = new Data();
        }
        public DataSet TTSV()
        {
            return db.ExecuteQueryDataSet("select * from  tblSINH_VIEN", CommandType.Text);
        }
        public DataSet TTSV(string tt)
        {
            return db.ExecuteQueryDataSet("select "+tt+"  from  tblSINH_VIEN", CommandType.Text);
        }

        public bool ThemSv(string maSv, string tenSv,string ngaysinh, string gioiTinh,string diachi,string maLop,string maKhoa,string nk, ref string err)
        {
            string sqlString = "Insert Into tblSINH_VIEN Values('" + maSv + "',N'" + tenSv + "','" + ngaysinh + "',N'" + gioiTinh + "','" + diachi + "','" + maLop + "','" + maKhoa + "','" + nk + "')";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
        public bool CapNhatSv(string maSv, string tenSv, string ngaysinh, string gioiTinh, string diachi, string maLop, string maKhoa,string nk, ref string err)
        {
            string sqlString = "UPDATE tblSINH_VIEN SET HoTen= N'" + tenSv + "', NgaySinh = '" + ngaysinh + "', GioiTinh = N'" + gioiTinh + "', DiaChi = N'" + diachi + "', MaLop = '" + maLop + "', MaKhoa = '" + maKhoa + "',NienKhoa='"+nk+"' WHERE MaSv = '" + maSv + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }

        public bool XoaSv(ref string err, string maSv)
        {
            string sqlString = "Delete from tblSINH_VIEN Where MaSv='" + maSv + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
    }
}
