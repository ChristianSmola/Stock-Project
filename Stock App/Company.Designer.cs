namespace Stock_App
{
    partial class Company
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpLiveChart = new System.Windows.Forms.TabPage();
            this.cartesianChart2 = new LiveCharts.WinForms.CartesianChart();
            this.tpGrowth = new System.Windows.Forms.TabPage();
            this.CartChartGrowthVsIndustry = new LiveCharts.WinForms.CartesianChart();
            this.btnMLTest = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpLiveChart.SuspendLayout();
            this.tpGrowth.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(485, 345);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(404, 345);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpLiveChart);
            this.tabControl1.Controls.Add(this.tpGrowth);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(552, 327);
            this.tabControl1.TabIndex = 4;
            // 
            // tpLiveChart
            // 
            this.tpLiveChart.Controls.Add(this.cartesianChart2);
            this.tpLiveChart.Location = new System.Drawing.Point(4, 22);
            this.tpLiveChart.Name = "tpLiveChart";
            this.tpLiveChart.Padding = new System.Windows.Forms.Padding(3);
            this.tpLiveChart.Size = new System.Drawing.Size(544, 301);
            this.tpLiveChart.TabIndex = 0;
            this.tpLiveChart.Text = "Live Chart";
            this.tpLiveChart.UseVisualStyleBackColor = true;
            // 
            // cartesianChart2
            // 
            this.cartesianChart2.Location = new System.Drawing.Point(6, 6);
            this.cartesianChart2.Name = "cartesianChart2";
            this.cartesianChart2.Size = new System.Drawing.Size(532, 264);
            this.cartesianChart2.TabIndex = 0;
            this.cartesianChart2.Text = "cartesianChart2";
            // 
            // tpGrowth
            // 
            this.tpGrowth.Controls.Add(this.CartChartGrowthVsIndustry);
            this.tpGrowth.Location = new System.Drawing.Point(4, 22);
            this.tpGrowth.Name = "tpGrowth";
            this.tpGrowth.Padding = new System.Windows.Forms.Padding(3);
            this.tpGrowth.Size = new System.Drawing.Size(544, 301);
            this.tpGrowth.TabIndex = 1;
            this.tpGrowth.Text = "Growth Vs Industry";
            this.tpGrowth.UseVisualStyleBackColor = true;
            // 
            // CartChartGrowthVsIndustry
            // 
            this.CartChartGrowthVsIndustry.Location = new System.Drawing.Point(6, 6);
            this.CartChartGrowthVsIndustry.Name = "CartChartGrowthVsIndustry";
            this.CartChartGrowthVsIndustry.Size = new System.Drawing.Size(532, 256);
            this.CartChartGrowthVsIndustry.TabIndex = 0;
            this.CartChartGrowthVsIndustry.Text = "CartChartGrowthVsIndustry";
            // 
            // btnMLTest
            // 
            this.btnMLTest.Location = new System.Drawing.Point(239, 346);
            this.btnMLTest.Name = "btnMLTest";
            this.btnMLTest.Size = new System.Drawing.Size(75, 23);
            this.btnMLTest.TabIndex = 5;
            this.btnMLTest.Text = "ML Test";
            this.btnMLTest.UseVisualStyleBackColor = true;
            this.btnMLTest.Click += new System.EventHandler(this.btnMLTest_Click);
            // 
            // Company
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 380);
            this.Controls.Add(this.btnMLTest);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
            this.Name = "Company";
            this.Text = "Company";
            this.Load += new System.EventHandler(this.Company_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpLiveChart.ResumeLayout(false);
            this.tpGrowth.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpLiveChart;
        private System.Windows.Forms.TabPage tpGrowth;
        private LiveCharts.WinForms.CartesianChart CartChartGrowthVsIndustry;
        private LiveCharts.WinForms.CartesianChart cartesianChart2;
        private System.Windows.Forms.Button btnMLTest;
    }
}