namespace ITProject_form
{
    partial class inDiemMon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.tblKETQUA2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new ITProject_form.DataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tblKET_QUA2TableAdapter = new ITProject_form.DataSet1TableAdapters.tblKET_QUA2TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.tblKETQUA2BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // tblKETQUA2BindingSource
            // 
            this.tblKETQUA2BindingSource.DataMember = "tblKET_QUA2";
            this.tblKETQUA2BindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.tblKETQUA2BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ITProject_form.ReportMon.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(971, 495);
            this.reportViewer1.TabIndex = 0;
            // 
            // tblKET_QUA2TableAdapter
            // 
            this.tblKET_QUA2TableAdapter.ClearBeforeFill = true;
            // 
            // inDiemMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 495);
            this.Controls.Add(this.reportViewer1);
            this.Name = "inDiemMon";
            this.Text = "reportMon";
            this.Load += new System.EventHandler(this.reportMon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblKETQUA2BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource tblKETQUA2BindingSource;
        private DataSet1 dataSet1;
        private DataSet1TableAdapters.tblKET_QUA2TableAdapter tblKET_QUA2TableAdapter;
    }
}