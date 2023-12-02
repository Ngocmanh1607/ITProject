using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ITProject_form.BS;

namespace ITProject_form
{
    public partial class main : Form
    {
        DataTable dtKhoa = null;
        DataTable dtLop = null;
        DataTable dtGv = null;
        DataTable dtmh = null;
        DataTable dtSv = null;
        DataTable dtDiem=null;
        DataTable dttk = null;

        // Khai báo biến kiểm tra việc Thêm hay Sửa dữ liệu
        bool Them;
        string err;
        BLKhoa dbK = new BLKhoa();
        BLLop dbL = new BLLop();
        BLGV dbgv = new BLGV();
        BLMonHoc dbmh = new BLMonHoc();
        BLSinhVien dbSv = new BLSinhVien();
        BLDiem dbDiem=new BLDiem();
        BLTkDiem dbtk = new BLTkDiem();
        public main()
        {
            InitializeComponent();
            LoadData();
           

           // Gán kích thước của form khi nó đang chạy cho thuộc tính Size
            this.Size = this.ClientSize;
           
        }
        void LoadData()
        {
            try
            {
                dtKhoa = new DataTable();
                dtKhoa.Clear();
                DataSet ds = dbK.TTKhoa();
                dtKhoa = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView
                dgvKhoa.DataSource = dtKhoa;
                // Thay đổi độ rộng cột
                dgvKhoa.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel
                this.txtmaKhoa.ResetText();
                this.txtKhoa.ResetText();
                // Không cho thao tác trên các nút Lưu / Hủy
                this.btnLuu.Enabled = false;
                this.btnHuy.Enabled = false;
                // Cho thao tác trên các nút Thêm / Sửa / Xóa /Thoát
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                //
                dgvKhoa_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Khoa. Lỗi rồi!!!");
            }
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvKhoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvKhoa.CurrentCell == null)
            {
                return;
            }
            // Thứ tự dòng hiện hành
            int r = dgvKhoa.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel
            this.txtmaKhoa.Text =
            dgvKhoa.Rows[r].Cells[0].Value.ToString();
            this.txtKhoa.Text =
            dgvKhoa.Rows[r].Cells[1].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            // Kich hoạt biến Them
            Them = true;
            // Xóa trống các đối tượng trong Panel
            this.txtmaKhoa.ResetText();
            this.txtKhoa.ResetText();
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            // Đưa con trỏ đến TextField txtThanhPho
            this.txtmaKhoa.Focus();
        }

        private void dgvKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvKhoa.CurrentCell == null)
            {
                return;
            }
            // Thứ tự dòng hiện hành
            int r = dgvKhoa.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel
            this.txtmaKhoa.Text =
            dgvKhoa.Rows[r].Cells[0].Value.ToString();
            this.txtKhoa.Text =
            dgvKhoa.Rows[r].Cells[1].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Mở kết nối
            // Thêm dữ liệu
            if (Them)
            {
                try
                {
                    // Thực hiện lệnh
                    BLKhoa blKhoa = new BLKhoa();
                    blKhoa.ThemKhoa(this.txtmaKhoa.Text, this.txtKhoa.Text, ref err);
                    if (err == null)
                    {
                        // Load lại dữ liệu trên DataGridView
                        LoadData();
                        // Thông báo
                        MessageBox.Show("Đã thêm xong!");
                    }
                    else
                    {
                        MessageBox.Show(err);
                        return;
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!");
                }
            }
            else
            {
                // Thực hiện lệnh
                BLKhoa blK = new BLKhoa();
               blK.CapNhatKhoa(this.txtmaKhoa.Text, this.txtKhoa.Text, ref err);
                if (err == null)
                {
                    // Load lại dữ liệu trên DataGridView
                    LoadData();
                    // Thông báo
                    MessageBox.Show("Đã sửa xong!");
                }
                else
                {
                    MessageBox.Show(err);
                    return;
                }
            }
            // Đóng kết nối 
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong Panel
            this.txtKhoa.ResetText();
            this.txtmaKhoa.ResetText();
            // Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            // Không cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuu.Enabled = false;
            dgvKhoa_CellClick(null, null);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa
            Them = false;
            // Cho phép thao tác trên Panel
            dgvKhoa_CellClick(null, null);
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            // Đưa con trỏ đến TextField txtMaKH
            this.txtmaKhoa.Enabled = false;
            this.txtKhoa.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Thực hiện lệnh
                // Lấy thứ tự record hiện hành
                int r = dgvKhoa.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành
                string strmaKhoa =
                dgvKhoa.Rows[r].Cells[0].Value.ToString().Trim();
                // Viết câu lệnh SQL
                // Hiện thông báo xác nhận việc xóa mẫu tin
                // Khai báo biến traloi
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?
                if (traloi == DialogResult.Yes)
                {
                    dbK.XoaKhoa(ref err, strmaKhoa);
                    if (err == null)
                    {
                        // Cập nhật lại DataGridView
                        LoadData();
                        // Thông báo
                        MessageBox.Show("Đã xóa xong!");
                    }
                    else
                        MessageBox.Show("Không thể thực hiện việc xóa mẫu tin!"+
                             "Lỗi: " + err);
                }
                else
                {
                    // Thông báo
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin !");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!");
            }
        }

        private void btnkhoa_Click(object sender, EventArgs e)
        {
            this.pnlKhoa.Visible = true;
            this.pnlLop.Visible = false;
            this.pnlGV.Visible = false;
            this.pnlMH.Visible=false;
            this.pnlSV.Visible = false;
            this.pnlDiem.Visible = false;
            this.pnlTkDiem.Visible=false;
            this.LoadData();
        }

        private void btnqllop_Click(object sender, EventArgs e)
        {
            this.pnlLop.Visible = true;
            this.pnlKhoa.Visible = false;
            this.pnlGV.Visible = false;
            this.pnlMH.Visible= false;
            this.pnlSV.Visible = false;
            this.pnlDiem.Visible = false;
            this.pnlTkDiem.Visible = false;
            LoadDataLop();
        }
        void LoadDataLop()
        {
            try
            {
                dtLop = new DataTable();
                dtLop.Clear();
                DataSet ds = dbL.TTLop();
                dtLop = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView
                dgvLop.DataSource = dtLop;
                // Thay đổi độ rộng cột
                dgvLop.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel
                this.txtmaKhoa.ResetText();
                this.txtMaLop.ResetText();
                this.txtTenLop.ResetText();
                // Không cho thao tác trên các nút Lưu / Hủy
                this.btnLuuLop.Enabled = false;
                this.btnHuyLop.Enabled = false;
                // Cho thao tác trên các nút Thêm / Sửa / Xóa /Thoát
                this.btnThemLop.Enabled = true;
                this.btnSuaLop.Enabled = true;
                this.btnXoaLop.Enabled = true;
                //
                dgvLop_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Khoa. Lỗi rồi!!!");
            }
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {

            // Kich hoạt biến Them
            Them = true;
            // Xóa trống các đối tượng trong Panel
            this.txtmaKhoa.ResetText();
            this.txtMaLop.ResetText();
            this.txtTenLop.ResetText();
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuLop.Enabled = true;
            this.btnHuyLop.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThemLop.Enabled = false;
            this.btnSuaLop.Enabled = false;
            this.btnXoaLop.Enabled = false;
            // Đưa con trỏ đến TextField txtThanhPho
            this.txtmaKhoa.Focus();
        }

        private void btnSuaLop_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa
            Them = false;
            // Cho phép thao tác trên Panel
            dgvLop_CellClick(null, null);
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuLop.Enabled = true;
            this.btnHuyLop.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThemLop.Enabled = false;
            this.btnSuaLop.Enabled = false;
            this.btnXoaLop.Enabled = false;
            // Đưa con trỏ đến TextField txtMaKH
            this.txtMaLop.Enabled = false;
            this.txtMaKhoaL.Focus();
        }

        private void btnLuuLop_Click(object sender, EventArgs e)
        {
            // Mở kết nối
            // Thêm dữ liệu
            if (Them)
            {
                try
                {
                    // Thực hiện lệnh
                    BLLop blLop = new BLLop();
                    blLop.ThemLop(this.txtMaKhoaL.Text, this.txtMaLop.Text,this.txtTenLop.Text, ref err);
                    if (err == null)
                    {
                        // Load lại dữ liệu trên DataGridView
                        LoadDataLop();
                        // Thông báo
                        MessageBox.Show("Đã thêm xong!");
                    }
                    else
                    {
                        MessageBox.Show(err);
                        return;
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!");
                }
            }
            else
            {
                // Thực hiện lệnh
                BLLop blL = new BLLop();
                blL.CapNhatLop(this.txtMaKhoaL.Text, this.txtMaLop.Text,this.txtTenLop.Text ,ref err);
                if (err == null)
                {
                    // Load lại dữ liệu trên DataGridView
                    LoadDataLop();
                    // Thông báo
                    MessageBox.Show("Đã sửa xong!");
                }
                else
                {
                    MessageBox.Show(err);
                    return;
                }
            }
            // Đóng kết nối 
        }

        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLop.CurrentCell == null)
            {
                return;
            }
            // Thứ tự dòng hiện hành
            int r = dgvLop.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel
            this.txtMaKhoaL.Text =
            dgvLop.Rows[r].Cells[0].Value.ToString();
            this.txtMaLop.Text =
            dgvLop.Rows[r].Cells[1].Value.ToString();
            this.txtTenLop.Text= dgvLop.Rows[r].Cells[2].Value.ToString();
        }

        private void btnXoaLop_Click(object sender, EventArgs e)
        {
            try
            {
                // Thực hiện lệnh
                // Lấy thứ tự record hiện hành
                int r = dgvLop.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành
                string strmaLop =
                dgvLop.Rows[r].Cells[1].Value.ToString().Trim();
                // Viết câu lệnh SQL
                // Hiện thông báo xác nhận việc xóa mẫu tin
                // Khai báo biến traloi
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?
                if (traloi == DialogResult.Yes)
                {
                    dbL.XoaLop(ref err, strmaLop);
                    if (err == null)
                    {
                        // Cập nhật lại DataGridView
                        LoadDataLop();
                        // Thông báo
                        MessageBox.Show("Đã xóa xong!");
                    }
                    else
                        MessageBox.Show("Không thể thực hiện việc xóa mẫu tin!" +
                             "Lỗi: " + err);
                }
                else
                {
                    // Thông báo
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin !");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!");
            }
        }

        private void btnHuyLop_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong Panel
            this.txtMaKhoaL.ResetText();
            this.txtMaLop.ResetText();
            this.txtTenLop.ResetText();
            // Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
            this.btnThemLop.Enabled = true;
            this.btnSuaLop.Enabled = true;
            this.btnXoaLop.Enabled = true;
            // Không cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuLop.Enabled = false;
            dgvKhoa_CellClick(null, null);
        }

        //panle giang vien

        private void btngv_Click(object sender, EventArgs e)
        {
            this.pnlGV.Visible = true;
            this.pnlKhoa.Visible = false;
            this.pnlLop.Visible = false;
            this.pnlMH.Visible=false;
            this.pnlSV.Visible = false;
            this.pnlDiem.Visible = false;
            this.pnlTkDiem.Visible = false;
            LoadDataGv();
        }
        void LoadDataGv()
        {
            try
            {
                dtGv = new DataTable();
                dtGv.Clear();
                DataSet ds = dbgv.TTGV();
                dtGv = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView
                dgvGV.DataSource = dtGv;
                // Thay đổi độ rộng cột
                dgvGV.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel
                this.txtMaGv.ResetText();
                this.txtTenGv.ResetText();
                this.txtSdtGv.ResetText();
                this.txtPl.ResetText();
                this.txtEmailGv.ResetText();
                this.txtGioiTinhGv.ResetText();
                // Không cho thao tác trên các nút Lưu / Hủy
                this.btnLuuGv.Enabled = false;
                this.btnHuyGv.Enabled = false;
                // Cho thao tác trên các nút Thêm / Sửa / Xóa /Thoát
                this.btnThemGv.Enabled = true;
                this.btnSuaGv.Enabled = true;
                this.btnXoaGv.Enabled = true;
                //
                dgvGV_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table. Lỗi rồi!!!");
            }
        }
        private void dgvGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGV.CurrentCell == null)
            {
                return;
            }
            // Thứ tự dòng hiện hành
            int r = dgvGV.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel
            this.txtMaGv.Text =
            dgvGV.Rows[r].Cells[0].Value.ToString();
            this.txtTenGv.Text =
            dgvGV.Rows[r].Cells[1].Value.ToString();
            this.txtGioiTinhGv.Text= dgvGV.Rows[r].Cells[2].Value.ToString();
            this.txtSdtGv.Text= dgvGV.Rows[r].Cells[3].Value.ToString();
            this.txtEmailGv.Text= dgvGV.Rows[r].Cells[4].Value.ToString();
            this.txtPl.Text= dgvGV.Rows[r].Cells[5].Value.ToString();
        }

        private void btnThemGv_Click(object sender, EventArgs e)
        {
            // Kich hoạt biến Them
            Them = true;
            // Xóa trống các đối tượng trong Panel
            this.txtMaGv.ResetText();
            this.txtTenGv.ResetText();
            this.txtEmailGv.ResetText();
            this.txtSdtGv.ResetText();
            this.txtPl.ResetText();
            this.txtGioiTinhGv.ResetText();
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuGv.Enabled = true;
            this.btnHuyGv.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThemGv.Enabled = false;
            this.btnSuaGv.Enabled = false;
            this.btnXoaGv.Enabled = false;
            // Đưa con trỏ đến TextField txtThanhPho
            this.txtMaMon.Focus();
        }

        private void btnSuaGv_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa
            Them = false;
            // Cho phép thao tác trên Panel
            dgvGV_CellClick(null, null);
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuGv.Enabled = true;
            this.btnHuyGv.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThemGv.Enabled = false;
            this.btnSuaGv.Enabled = false;
            this.btnXoaGv.Enabled = false;
            // Đưa con trỏ đến TextField txtMaKH
            this.txtMaMon.Enabled = false;
            this.txtTenGv.Focus();
        }

        private void btnXoaGv_Click(object sender, EventArgs e)
        {
            try
            {
                // Thực hiện lệnh
                // Lấy thứ tự record hiện hành
                int r = dgvGV.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành
                string strmaGv =
                dgvGV.Rows[r].Cells[0].Value.ToString().Trim();
                // Viết câu lệnh SQL
                // Hiện thông báo xác nhận việc xóa mẫu tin
                // Khai báo biến traloi
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?
                if (traloi == DialogResult.Yes)
                {
                    dbgv.XoaGv(ref err, strmaGv);
                    if (err == null)
                    {
                        // Cập nhật lại DataGridView
                        LoadDataGv();
                        // Thông báo
                        MessageBox.Show("Đã xóa xong!");
                    }
                    else
                        MessageBox.Show("Không thể thực hiện việc xóa mẫu tin!" +
                             "Lỗi: " + err);
                }
                else
                {
                    // Thông báo
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin !");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!");
            }
        }

        private void btnHuyGv_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong Panel
            this.txtMaGv.ResetText();
            this.txtTenGv.ResetText();
            this.txtEmailGv.ResetText();
            this.txtSdtGv.ResetText();
            this.txtPl.ResetText();
            this.txtGioiTinhGv.ResetText();
            // Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
            this.btnThemGv.Enabled = true;
            this.btnSuaGv.Enabled = true;
            this.btnXoaGv.Enabled = true;
            // Không cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuGv.Enabled = false;
            dgvGV_CellClick(null, null);
        }

        private void btnLuuGv_Click(object sender, EventArgs e)
        {
            // Mở kết nối
            // Thêm dữ liệu
            if (Them)
            {
                try
                {
                    try
                    {
                        int PhoneNumber = int.Parse(this.txtSdtGv.Text);
                        // Thực hiện lệnh
                        BLGV blGv = new BLGV();
                        blGv.ThemGv(this.txtMaGv.Text, this.txtTenGv.Text, this.txtGioiTinhGv.Text, PhoneNumber, this.txtEmailGv.Text, this.txtPl.Text, ref err);
                        if (err == null)
                        {
                            // Load lại dữ liệu trên DataGridView
                            LoadDataGv();
                            // Thông báo
                            MessageBox.Show("Đã thêm xong!");
                        }
                        else
                        {
                            MessageBox.Show(err);
                            return;
                        }
                        
                    }
                    catch
                    {
                        MessageBox.Show("Định dạng không đúng !");
                        return;
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!");
                }
            }
            else
            {
                // Thực hiện lệnh
                BLGV blGv = new BLGV();
                try
                {
                    int PhoneNumber = int.Parse(this.txtSdtGv.Text);
                    blGv.CapNhatGv(this.txtMaGv.Text, this.txtTenGv.Text, this.txtGioiTinhGv.Text, this.txtSdtGv.Text, this.txtEmailGv.Text, this.txtPl.Text, ref err);
                    if (err == null)
                    {

                    }
                }
                catch {
                    MessageBox.Show("Định dạng không đúng !");
                    return;
                }
                // Load lại dữ liệu trên DataGridView
                LoadDataGv();
                // Thông báo
                MessageBox.Show("Đã sửa xong!");
            }
            // Đóng kết nối 
        }


        //panle mon học
        private void btnMH_Click(object sender, EventArgs e)
        {
            this.pnlGV.Visible = false;
            this.pnlKhoa.Visible = false;
            this.pnlLop.Visible = false;
            this.pnlSV.Visible = false;
            this.pnlMH.Visible = true;
            this.pnlDiem.Visible = false;
            this.pnlTkDiem.Visible = false;
            this.LoadMonHoc();
        }

        private void dgvMH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvMH.CurrentCell==null)
            {
                return;
            }
            int r = dgvMH.CurrentCell.RowIndex;
            this.txtMaMon.Text =
            dgvMH.Rows[r].Cells[0].Value.ToString();
            this.txtTenMon.Text =
            dgvMH.Rows[r].Cells[1].Value.ToString();
            this.lbMaGVMon.Text= dgvMH.Rows[r].Cells[2].Value.ToString();
            this.lbMakhoa.Text= dgvMH.Rows[r].Cells[3].Value.ToString();
            this.txtSoTC.Text= dgvMH.Rows[r].Cells[4].Value.ToString();
        }
        private void LoadMonHoc()

        {
            try
            {
                dtmh = new DataTable();
                dtmh.Clear();
                DataSet ds = dbmh.TTMH();
                dtmh = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView
                dgvMH.DataSource = dtmh;
                // Thay đổi độ rộng cột
                dgvMH.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel
                this.txtMaMon.ResetText();
                this.txtTenMon.ResetText();
                this.lbMaGVMon.ResetText();
                this.lbMakhoa.ResetText();
                this.txtSoTC.ResetText();
                // Không cho thao tác trên các nút Lưu / Hủy
                this.btnLuuMH.Enabled = false;
                this.btnHuyMH.Enabled = false;
                // Cho thao tác trên các nút Thêm / Sửa / Xóa /Thoát
                this.btnThemMH.Enabled = true;
                this.btnSuaMH.Enabled = true;
                this.btnXoaMH.Enabled = true;

                //lay ma gv
                BLGV bLGV = new BLGV();
                DataSet dsmagv = bLGV.LMGV();
                DataTable dtmgv = dsmagv.Tables[0];
                foreach (DataRow row in dtmgv.Rows)
                {
                    this.lbMaGVMon.Items.Add(row["MaGV"].ToString());
                }
                //lay ma khoa
                BLKhoa bLKhoa = new BLKhoa();
                DataSet dsmak = bLKhoa.LMKhoa();
                DataTable dtmk = dsmak.Tables[0];
                foreach (DataRow row in dtmk.Rows)
                {
                    this.lbMakhoa.Items.Add(row["MaKhoa"].ToString());
                }
                //
                dgvMH_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table. Lỗi rồi!!!");
            }
        }

        private void btnThemMH_Click(object sender, EventArgs e)
        {
            // Kich hoạt biến Them
            Them = true;
            // Xóa trống các đối tượng trong Panel
            this.txtMaMon.ResetText();
            this.txtTenMon.ResetText();
            this.lbMaGVMon.ResetText();
            this.lbMakhoa.ResetText();
            this.txtSoTC.ResetText();
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuMH.Enabled = true;
            this.btnHuyMH.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThemMH.Enabled = false;
            this.btnSuaMH.Enabled = false;
            this.btnXoaMH.Enabled = false;
            this.txtMaMon.Focus();
        }

        private void btnSuaMH_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa
            Them = false;
            // Cho phép thao tác trên Panel
            dgvMH_CellClick(null, null);
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuMH.Enabled = true;
            this.btnHuyMH.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThemMH.Enabled = false;
            this.btnSuaMH.Enabled = false;
            this.btnXoaMH.Enabled = false;
            // Đưa con trỏ đến TextField txtMaKH
            this.txtMaMon.Enabled = false;
            this.txtTenMon.Focus();
        }

        private void btnXoaMH_Click(object sender, EventArgs e)
        {
            try
            {
                // Thực hiện lệnh
                // Lấy thứ tự record hiện hành
                int r = dgvMH.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành
                string strmaMH =
                dgvMH.Rows[r].Cells[0].Value.ToString().Trim();
                // Viết câu lệnh SQL
                // Hiện thông báo xác nhận việc xóa mẫu tin
                // Khai báo biến traloi
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?
                if (traloi == DialogResult.Yes)
                {
                    dbmh.XoaMH(ref err, strmaMH);
                    if (err == null)
                    {
                        // Cập nhật lại DataGridView
                       LoadMonHoc();
                        // Thông báo
                        MessageBox.Show("Đã xóa xong!");
                    }
                    else { 
                        MessageBox.Show("Không thể thực hiện việc xóa mẫu tin!" +
                             "Lỗi: " + err);
                        err = null;
                }
                }
                else
                {
                    // Thông báo
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin !");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!");
            }
        }

        private void btnHuyMH_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong Panel
            this.txtMaMon.ResetText();
            this.txtTenMon.ResetText();
            this.lbMaGVMon.ResetText();
            this.lbMakhoa.ResetText();
            this.txtSoTC.ResetText();
            // Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
            this.btnThemMH.Enabled = true;
            this.btnSuaMH.Enabled = true;
            this.btnXoaMH.Enabled = true;
            // Không cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuMH.Enabled = false;
            dgvMH_CellClick(null, null);
        }

        private void btnLuuMH_Click(object sender, EventArgs e)
        {
            // Mở kết nối
            // Thêm dữ liệu
            if (Them)
            {
                try
                {
                    // Thực hiện lệnh
                    BLMonHoc blMH = new BLMonHoc();
                    try
                    {
                        int sotc = int.Parse(this.txtSoTC.Text);
                        blMH.ThemMH(this.txtMaMon.Text, this.txtTenMon.Text, this.lbMaGVMon.Text, this.lbMakhoa.Text,sotc, ref err);
                        if (err == null)
                        {
                            // Load lại dữ liệu trên DataGridView
                            LoadMonHoc();
                            // Thông báo
                            MessageBox.Show("Đã thêm xong!");
                        }
                        else
                        {
                            MessageBox.Show(err);
                            err = null;
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi định dạng ");
                        return;
                    }
                    
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!");
                }
            }
            else
            {
                // Thực hiện lệnh
                BLMonHoc blMH = new BLMonHoc();
                try
                {
                    int sotc = int.Parse(this.txtSoTC.Text);
                    blMH.CapNhatMH(this.txtMaMon.Text, this.txtTenMon.Text, this.lbMaGVMon.Text, this.lbMakhoa.Text, sotc, ref err);
                    if (err == null)
                    {
                        LoadMonHoc();
                        // Thông báo
                        MessageBox.Show("Đã sửa xong!");
                    }
                    else
                    {
                        MessageBox.Show(err);
                        err = null;
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Định dạng không đúng !");
                    return;
                }
            }
            // Đóng kết nối 
        }

        private void btnSv_Click(object sender, EventArgs e)
        {
            this.pnlSV.Visible = true;
            this.pnlKhoa.Visible = false;
            this.pnlLop.Visible = false;
            this.pnlGV.Visible=false;
            this.pnlMH.Visible = false;
            this.pnlDiem.Visible = false;
            this.pnlTkDiem.Visible = false;
            LoadSV();
        }

        //panel sinh vien

        private void LoadSV()
        {
            try
            {
                dtSv = new DataTable();
                dtSv.Clear();
                DataSet ds = dbSv.TTSV();
                dtSv = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView
                dgvSV.DataSource = dtSv;
                // Thay đổi độ rộng cột
                dgvSV.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel
                this.txtMaSV.ResetText();
                this.txtTenSV.ResetText();
                this.txtGioiTinhSV.ResetText();
                this.dtNSSV.ResetText();
                this.txtDiaChi.ResetText();
                this.cbxMaLop.ResetText();
                this.cbxMaKhoa.ResetText();
                this.txtNk.ResetText();
                // Không cho thao tác trên các nút Lưu / Hủy
                this.btnLuuSV.Enabled = false;
                this.btnHuySV.Enabled = false;
                // Cho thao tác trên các nút Thêm / Sửa / Xóa /Thoát
                this.btnThemSV.Enabled = true;
                this.btnSuaSV.Enabled = true;
                this.btnXoaSV.Enabled = true;

                //lay ma lop
                BLLop blL = new BLLop();
                DataSet dsmaL = blL.TMLop();
                DataTable dtmL = dsmaL.Tables[0];
                foreach (DataRow row in dtmL.Rows)
                {
                    this.cbxMaLop.Items.Add(row["MaLop"].ToString());
                }
                //lay ma khoa
                BLKhoa bLKhoa = new BLKhoa();
                DataSet dsmak = bLKhoa.LMKhoa();
                DataTable dtmk = dsmak.Tables[0];
                foreach (DataRow row in dtmk.Rows)
                {
                    this.cbxMaKhoa.Items.Add(row["MaKhoa"].ToString());
                }
                //
                dgvSV_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table. Lỗi rồi!!!");
            }
        }

        private void dgvSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSV.CurrentCell == null)
            {
                return;
            }
            try
            {
                int r = dgvSV.CurrentCell.RowIndex;
                this.txtMaSV.Text =
                dgvSV.Rows[r].Cells[0].Value.ToString();
                this.txtTenSV.Text =
                dgvSV.Rows[r].Cells[1].Value.ToString();
                this.dtNSSV.Text = dgvSV.Rows[r].Cells[2].Value.ToString();
                this.txtGioiTinhSV.Text = dgvSV.Rows[r].Cells[3].Value.ToString();
                this.txtDiaChi.Text = dgvSV.Rows[r].Cells[4].Value.ToString();
                this.cbxMaLop.Text = dgvSV.Rows[r].Cells[5].Value.ToString();
                this.cbxMaKhoa.Text = dgvSV.Rows[r].Cells[6].Value.ToString();
                this.txtNk.Text= dgvSV.Rows[r].Cells[7].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Lỗi thông tin ");
                return;
            }
        }

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            // Kich hoạt biến Them
            Them = true;
            // Xóa trống các đối tượng trong Panel
            this.txtMaSV.ResetText();
            this.txtTenSV.ResetText();
            this.dtNSSV.ResetText();
            this.txtGioiTinhSV.ResetText();
            this.txtDiaChi.ResetText();
            this.cbxMaLop.ResetText();
            this.cbxMaKhoa.ResetText();
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuSV.Enabled = true;
            this.btnHuySV.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThemSV.Enabled = false;
            this.btnSuaSV.Enabled = false;
            this.btnXoaSV.Enabled = false;
            this.txtMaSV.Focus();
        }

        private void btnSuaSV_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa
            Them = false;
            // Cho phép thao tác trên Panel
            dgvSV_CellClick(null, null);
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuSV.Enabled = true;
            this.btnHuySV.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThemSV.Enabled = false;
            this.btnSuaSV.Enabled = false;
            this.btnXoaSV.Enabled = false;
            // Đưa con trỏ đến TextField 
            this.txtMaSV.Enabled = false;
            this.txtTenSV.Focus();
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            try
            {
                // Thực hiện lệnh
                // Lấy thứ tự record hiện hành
                int r = dgvSV.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành
                string strmaSV =
                dgvSV.Rows[r].Cells[0].Value.ToString().Trim();
                // Khai báo biến traloi
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?
                if (traloi == DialogResult.Yes)
                {
                    dbSv.XoaSv(ref err, strmaSV);
                    if (err == null)
                    {
                        // Cập nhật lại DataGridView
                        LoadSV();
                        // Thông báo
                        MessageBox.Show("Đã xóa xong!");
                    }
                    else
                    {
                        MessageBox.Show("Không thể thực hiện việc xóa mẫu tin!" +
                             "Lỗi: " + err);
                        err = null;
                    }
                }
                else
                {
                    // Thông báo
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin !");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!");
            }
        }

        private void btnHuySV_Click(object sender, EventArgs e)
        {
            /// Xóa trống các đối tượng trong Panel
            this.txtMaSV.ResetText();
            this.txtTenSV.ResetText();
            this.dtNSSV.ResetText();
            this.txtGioiTinhSV.ResetText();
            this.txtDiaChi.ResetText();
            this.cbxMaLop.ResetText();
            this.cbxMaKhoa.ResetText();
            // Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
            this.btnThemSV.Enabled = true;
            this.btnSuaSV.Enabled = true;
            this.btnXoaSV.Enabled = true;
            // Không cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuSV.Enabled = false;
            dgvSV_CellClick(null, null);
        }

        private void btnLuuSV_Click(object sender, EventArgs e)
        {
            // Mở kết nối
            // Thêm dữ liệu
            if (Them)
            {
                try
                {
                    // Thực hiện lệnh
                    BLSinhVien blSv = new BLSinhVien();
                    try
                    {
                        blSv.ThemSv(this.txtMaSV.Text, this.txtTenSV.Text, this.dtNSSV.Text, this.txtGioiTinhSV.Text, this.txtDiaChi.Text,this.cbxMaLop.Text.Trim(),this.cbxMaKhoa.Text.Trim(),this.txtNk.Text, ref err);
                        if (err == null)
                        {
                            // Load lại dữ liệu trên DataGridView
                            LoadSV();
                            // Thông báo
                            MessageBox.Show("Đã thêm xong!");
                        }
                        else
                        {
                            MessageBox.Show(err);
                            err = null;
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi định dạng ");
                        return;
                    }

                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!");
                }
            }
            else
            {
                // Thực hiện lệnh
                BLSinhVien blSv = new BLSinhVien();
                try
                {
                    blSv.CapNhatSv(this.txtMaSV.Text, this.txtTenSV.Text, this.dtNSSV.Text, this.txtGioiTinhSV.Text, this.txtDiaChi.Text, this.cbxMaLop.Text, this.cbxMaKhoa.Text,this.txtNk.Text, ref err);
                    if (err == null)
                    {
                        LoadSV();
                        // Thông báo
                        MessageBox.Show("Đã sửa xong!");
                    }
                    else
                    {
                        MessageBox.Show(err);
                        err = null;
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Định dạng không đúng !");
                    return;
                }
            }
            // Đóng kết nối 
        }

        private void btntk_Click(object sender, EventArgs e)
        {
            this.pnlDiem.Visible = true;
            this.pnlGV.Visible = false;
            this.pnlKhoa.Visible=false;
            this.pnlLop.Visible=false;
            this.pnlMH.Visible = false;
            this.pnlSV.Visible = false;
            this.pnlTkDiem.Visible = false;

            loadDiem();
        }
        private void loadDiem()
        {
            try
            {
                dtDiem = new DataTable();
                dtDiem.Clear();
                DataSet ds = dbDiem.TTDiem();
                dtDiem = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView
                dgvDiem.DataSource = dtDiem;
                // Thay đổi độ rộng cột
                dgvDiem.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel
                this.cbxMaSVD.ResetText();
                this.cbxMaMD.ResetText();
                this.txtDqt.ResetText();
                this.txtDT.ResetText();
                this.txtTongD.ResetText();
                this.txtXepLoai.ResetText();
                this.txtHK.ResetText();
                this.txtGhichu.ResetText();
                // Không cho thao tác trên các nút Lưu / Hủy
                this.btnLuuD.Enabled = false;
                this.btnHuyD.Enabled = false;
                // Cho thao tác trên các nút Thêm / Sửa / Xóa /Thoát
                this.btnThemD.Enabled = true;
                this.btnSuaD.Enabled = true;


                //lay ma sv
                BLSinhVien blSv = new BLSinhVien();
                DataSet dsmaSV = blSv.TTSV("MaSv");
                DataTable dtmSv = dsmaSV.Tables[0];
                foreach (DataRow row in dtmSv.Rows)
                {
                    this.cbxMaSVD.Items.Add(row["MaSv"].ToString());
                }

                //lay ma mon
                BLMonHoc bLMon = new BLMonHoc();
                DataSet dsmon = bLMon.TTMH("MaMon");
                DataTable dtmM = dsmon.Tables[0];
                foreach (DataRow row in dtmM.Rows)
                {
                    this.cbxMaMD.Items.Add(row["MaMon"].ToString());
                }
                //
                dgvDiem_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table. Lỗi rồi!!!");
            }
        }
        private void dgvDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDiem.CurrentCell == null)
            {
                return;
            }
            try
            {
                int r = dgvDiem.CurrentCell.RowIndex;
                this.cbxMaSVD.Text =
                dgvDiem.Rows[r].Cells[0].Value.ToString();
                this.cbxMaMD.Text =
                dgvDiem.Rows[r].Cells[1].Value.ToString();
                this.txtDqt.Text = dgvDiem.Rows[r].Cells[2].Value.ToString();
                this.txtDT.Text = dgvDiem.Rows[r].Cells[3].Value.ToString();
                this.txtTongD.Text = dgvDiem.Rows[r].Cells[4].Value.ToString();
                this.txtXepLoai.Text = dgvDiem.Rows[r].Cells[5].Value.ToString();
                this.txtHK.Text = dgvDiem.Rows[r].Cells[6].Value.ToString();
                this.txtGhichu.Text = dgvDiem.Rows[r].Cells[7].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Lỗi thông tin ");
                return;
            }
        }

        private void btnThemD_Click(object sender, EventArgs e)
        {
            // Kich hoạt biến Them
            Them = true;
            // Xóa trống các đối tượng trong Panel
                this.cbxMaSVD.ResetText();
            this.cbxMaMD.ResetText();
            this.txtDqt.ResetText();
            this.txtDT.ResetText();
            this.txtTongD.ResetText();
            this.txtXepLoai.ResetText();
            this.txtHK.ResetText();
            this.txtGhichu.ResetText();
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuD.Enabled = true;
            this.btnHuyD.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThemD.Enabled = false;
            this.btnSuaD.Enabled = false;
            this.cbxMaSVD.Focus();
        }

        private void btnSuaD_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa
            Them = false;
            // Cho phép thao tác trên Panel
            dgvDiem_CellClick(null, null);
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuD.Enabled = true;
            this.btnHuyD.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            this.btnThemD.Enabled = false;
            this.btnSuaD.Enabled = false;
            // Đưa con trỏ đến TextField 
            this.cbxMaSVD.Enabled = false;
            this.cbxMaMD.Enabled = false;
            this.txtDqt.Focus();
        }

        private void btnHuyD_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong Panel
            this.cbxMaSVD.ResetText();
            this.cbxMaMD.ResetText();
            this.txtDqt.ResetText();
            this.txtDT.ResetText();
            this.txtTongD.ResetText();
            this.txtXepLoai.ResetText();
            this.txtHK.ResetText();
            this.txtGhichu.ResetText();
            // Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
            this.btnThemD.Enabled = true;
            this.btnSuaD.Enabled = true;
            // Không cho thao tác trên các nút Lưu / Hủy / Panel
            this.btnLuuD.Enabled = false;
            dgvDiem_CellClick(null, null);
        }

        private void btnLuuD_Click(object sender, EventArgs e)
        {

            // Mở kết nối
            // Thêm dữ liệu
            float diemqt = 0;
            float diemthi = 0;
            float tong = 0;
            int hocky ;
            string xephang = null;
            if (Them)
            {
                try
                {
                    if (this.txtDqt.Text != null)
                    {
                        diemqt = float.Parse(this.txtDqt.Text);
                    }
                    if (this.txtDT.Text != "")
                    {
                        diemthi = float.Parse(this.txtDT.Text);
                    }
                    tong = (diemqt + diemthi) / 2;
                    if (this.txtHK.Text != "")
                    {
                        hocky = int.Parse(this.txtHK.Text);
                    }
                    string xeploai;
                    if (tong <= 5)
                    {
                        xephang = "D";
                    }
                    else if (tong <= 6.5)
                    {
                        xephang = "C";
                    }
                    else if (tong <= 8)
                    {
                        xephang = "B";
                    }
                    else if (tong <= 10)
                    {
                        xephang = "A";
                    }
                }
                catch
                {
                    MessageBox.Show("Lỗi định dạng");
                    return;
                }
                try
                {
                    // Thực hiện lệnh
                    BLDiem blDiem = new BLDiem();
                    try
                    {
                        
                        blDiem.ThemDiem(this.cbxMaSVD.Text, this.cbxMaMD.Text, this.txtDqt.Text, this.txtDT.Text, tong,xephang, this.txtHK.Text, this.txtGhichu.Text, ref err);
                        if (err == null)
                        {
                            // Load lại dữ liệu trên DataGridView
                            loadDiem();
                            // Thông báo
                            MessageBox.Show("Đã thêm xong!");
                        }
                        else
                        {
                            MessageBox.Show(err);
                            err = null;
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi định dạng ");
                        return;
                    }

                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!");
                }
            }
            else
            {
                if (this.txtDqt.Text != null)
                {
                    diemqt = float.Parse(this.txtDqt.Text);
                }
                if (this.txtDT.Text != "")
                {
                    diemthi = float.Parse(this.txtDT.Text);
                }
                tong = (diemqt + diemthi) / 2;
                if (this.txtHK.Text != "")
                {
                    hocky = int.Parse(this.txtHK.Text);
                }
                string xeploai;
                if (tong <= 5)
                {
                    xephang = "D";
                }
                else if (tong <= 6.5)
                {
                    xephang = "C";
                }
                else if (tong <= 8)
                {
                    xephang = "B";
                }
                else if (tong <= 10)
                {
                    xephang = "A";
                }
                // Thực hiện lệnh
                BLDiem blDiem = new BLDiem();
                try
                {
                    blDiem.CapNhatDiem(this.cbxMaSVD.Text, this.cbxMaMD.Text,this.txtDqt.Text, this.txtDT.Text, tong, xephang, this.txtHK.Text, this.txtGhichu.Text, ref err);
                    if (err == null)
                    {
                        loadDiem();
                        // Thông báo
                        MessageBox.Show("Đã sửa xong!");
                    }
                    else
                    {
                        MessageBox.Show(err);
                        err = null;
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Định dạng không đúng !");
                    return;
                }
            }
            // Đóng kết nối 
        }

        private void cbxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxSearch.SelectedIndex == 0)
            {
                this.cbxma.Items.Clear();
                BLLop blL = new BLLop();
                DataSet dsmaL = blL.TMLop();
                DataTable dtmL = dsmaL.Tables[0];
                foreach (DataRow row in dtmL.Rows)
                {
                    this.cbxma.Items.Add(row["MaLop"].ToString());
                }
            }
            else if(cbxSearch.SelectedIndex==1)
            {
                this.cbxma.Items.Clear();
                //lay ma sv
                BLSinhVien blSv = new BLSinhVien();
                DataSet dsmaSV = blSv.TTSV("MaSv");
                DataTable dtmSv = dsmaSV.Tables[0];
                foreach (DataRow row in dtmSv.Rows)
                {
                    this.cbxma.Items.Add(row["MaSv"].ToString());
                }
            }
            else if(cbxSearch.SelectedIndex==2)
            {
                this.cbxma.Items.Clear();
                BLMonHoc bLMon = new BLMonHoc();
                DataSet dsmon = bLMon.TTMH("MaMon");
                DataTable dtmM = dsmon.Tables[0];
                foreach (DataRow row in dtmM.Rows)
                {
                    this.cbxma.Items.Add(row["MaMon"].ToString());
                }
            }

        }

        private void btnTkDiem_Click(object sender, EventArgs e)
        {
            this.pnlTkDiem.Visible = true;
            this.pnlDiem.Visible = false ;
            this.pnlGV.Visible = false;
            this.pnlKhoa.Visible = false;
            this.pnlLop.Visible = false;
            this.pnlMH.Visible = false;
            this.pnlSV.Visible = false;
            loadtk();

        }
        private void loadtk()
        {
            dtDiem = new DataTable();
            dtDiem.Clear();
            DataSet ds = dbDiem.TTDiem();
            dtDiem = ds.Tables[0];
            // Đưa dữ liệu lên DataGridView
            dgvTk.DataSource = dtDiem;
            // Thay đổi độ rộng cột
            dgvTk.AutoResizeColumns();
        }
        private void cbxma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cbxSearch.SelectedIndex==0)
            {
                dttk = new DataTable();
                dttk.Clear();
                DataSet ds = dbtk.searchLop(this.cbxma.Text);
                dttk = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView
                dgvTk.DataSource = dttk;
                // Thay đổi độ rộng cột
               
                dgvTk.AutoResizeColumns();
            }
            else if(this.cbxSearch.SelectedIndex==1)
            {
                dttk = new DataTable();
                dttk.Clear();
                DataSet ds = dbtk.searchSv(this.cbxma.Text);
                dttk = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView
                dgvTk.DataSource = dttk;
                // Thay đổi độ rộng cột

                dgvTk.AutoResizeColumns();
            }
            else if (this.cbxSearch.SelectedIndex == 2)
            {
                dttk = new DataTable();
                dttk.Clear();
                DataSet ds = dbtk.searchMon(this.cbxma.Text);
                dttk = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView
                dgvTk.DataSource = dttk;
                // Thay đổi độ rộng cột

                dgvTk.AutoResizeColumns();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if(this.cbxSearch.SelectedIndex==0)
            {
                inDiemLop inDiemLop = new inDiemLop(this.cbxma.Text);
                inDiemLop.ShowDialog();

            }
            else if(this.cbxSearch.SelectedIndex==1)
            {
                inDiemSv indsv = new inDiemSv(this.cbxma.Text);
                indsv.ShowDialog();
            }
            else if(this.cbxSearch.SelectedIndex==2)
            {
                inDiemMon indm = new inDiemMon(this.cbxma.Text);
                indm.ShowDialog();
            }
        }

        private void main_Load(object sender, EventArgs e)
        {

        }
    }

}
