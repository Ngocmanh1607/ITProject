using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITProject_form
{
    public partial class inDiemMon : Form
    {
        private string ma;
        public inDiemMon(string ma)
        {
            InitializeComponent();
            this.ma = ma;   
        }

        private void reportMon_Load(object sender, EventArgs e)
        {
            this.dataSet1.tblKET_QUA2.Clear();
            dataSet1.EnforceConstraints = false;
            this.tblKET_QUA2TableAdapter.Fill(dataSet1.tblKET_QUA2, ma);
            this.reportViewer1.RefreshReport();
        }
    }
}
