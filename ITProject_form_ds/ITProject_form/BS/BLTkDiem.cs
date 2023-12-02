using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITProject_form.DB;
namespace ITProject_form.BS
{
    internal class BLTkDiem
    {
        Data db = null;
        public BLTkDiem()
        {
            db = new Data();
        }
        public DataSet searchLop(string malop)
        {
            return db.ExecuteQueryDataSet("SELECT kq.MaSV,kq.MaMon,kq.DiemQT,kq.DiemThi,kq.TongKet,kq.XepLoai,kq.HocKi,kq.GhiChu FROM tblKET_QUA as kq join tblSINH_VIEN ON tblSINH_VIEN.MaSv =kq.MaSV join tblLOP ON tblLOP.MaLop=tblSINH_VIEN.MaLop where tblSINH_VIEN.MaLop = '" + malop+"'", CommandType.Text);
        }
        public DataSet searchSv(string masv)
        {
            return db.ExecuteQueryDataSet("select MaMon,DiemQT,DiemThi,TongKet,XepLoai,HocKi,GhiChu from tblKET_QUA where MaSV='" + masv + "'", CommandType.Text);
        }
        public DataSet searchMon(string mamon)
        {
            return db.ExecuteQueryDataSet("select MaSV,DiemQT,DiemThi,TongKet,XepLoai,HocKi,GhiChu from tblKET_QUA where MaMon='" + mamon + "'", CommandType.Text);
        }
    }
}
