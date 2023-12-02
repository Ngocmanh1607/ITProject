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
    public partial class inDiemSv : Form
    {
        private string ma;
        public inDiemSv(string ma)
        {
            InitializeComponent();
            this.ma = ma;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.dataSet1.tblKET_QUA.Clear();
            dataSet1.EnforceConstraints = false;
            this.tblKET_QUATableAdapter.Fill(dataSet1.tblKET_QUA,ma);
            this.reportViewer1.RefreshReport();
        }
    }
}
