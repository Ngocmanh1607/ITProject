using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITProject_form.DB;
namespace ITProject_form.BS
{
    internal class BLDiem
    {
        Data db = null;
        public BLDiem()
        {
            db = new Data();
        }
        public DataSet TTDiem()
        {
            return db.ExecuteQueryDataSet("select * from  tblKET_QUA", CommandType.Text);
        }
        public bool ThemDiem(string maSV,string maMon,string diemQt,string diemthi,float tongket,string xeploai, string hocki, string ghichu,ref string err)
        {
            string sqlString = "INSERT INTO tblKET_QUA  VALUES ('" + maSV + "', '" + maMon + "', '" + diemQt + "', '" + diemthi + "', '" + tongket + "', N'" + xeploai + "', '" + hocki + "', N'" + ghichu + "')";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
        public bool CapNhatDiem(string maSV, string maMon, string diemQt, string diemthi, float tongket, string xeploai, string hocki, string ghichu, ref string err)
        {
            string sqlString = "UPDATE tblKET_QUA SET DiemQT = '" + diemQt + "', DiemThi = '" + diemthi + "', TongKet = '" + tongket + "', XepLoai = N'" + xeploai + "', HocKi = '" + hocki + "', GhiChu = N'" + ghichu + "' WHERE MaSV = '" + maSV + "' AND MaMon = '" + maMon + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
        }
    }
}
