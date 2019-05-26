namespace Stock_App
{
    partial class Form1
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
            this.tpWeeklyGrowthChange = new System.Windows.Forms.TabPage();
            this.CartChartGrowthChange = new LiveCharts.WinForms.CartesianChart();
            this.tpWeeklyPricePointChange = new System.Windows.Forms.TabPage();
            this.btnCheckNews = new System.Windows.Forms.Button();
            this.btnEditCompany = new System.Windows.Forms.Button();
            this.btnAddCompany = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Symbols = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSaveTodaysDataToServer = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpWeeklyGrowthChange.SuspendLayout();
            this.tpWeeklyPricePointChange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpWeeklyGrowthChange
            // 
            this.tpWeeklyGrowthChange.Controls.Add(this.CartChartGrowthChange);
            this.tpWeeklyGrowthChange.Location = new System.Drawing.Point(4, 22);
            this.tpWeeklyGrowthChange.Margin = new System.Windows.Forms.Padding(2);
            this.tpWeeklyGrowthChange.Name = "tpWeeklyGrowthChange";
            this.tpWeeklyGrowthChange.Padding = new System.Windows.Forms.Padding(2);
            this.tpWeeklyGrowthChange.Size = new System.Drawing.Size(659, 324);
            this.tpWeeklyGrowthChange.TabIndex = 1;
            this.tpWeeklyGrowthChange.Text = "Weekly Growth";
            this.tpWeeklyGrowthChange.UseVisualStyleBackColor = true;
            // 
            // CartChartGrowthChange
            // 
            this.CartChartGrowthChange.Location = new System.Drawing.Point(5, 5);
            this.CartChartGrowthChange.Name = "CartChartGrowthChange";
            this.CartChartGrowthChange.Size = new System.Drawing.Size(649, 314);
            this.CartChartGrowthChange.TabIndex = 0;
            this.CartChartGrowthChange.Text = "cartesianChart1";
            // 
            // tpWeeklyPricePointChange
            // 
            this.tpWeeklyPricePointChange.Controls.Add(this.btnCheckNews);
            this.tpWeeklyPricePointChange.Controls.Add(this.btnEditCompany);
            this.tpWeeklyPricePointChange.Controls.Add(this.btnAddCompany);
            this.tpWeeklyPricePointChange.Controls.Add(this.dataGridView1);
            this.tpWeeklyPricePointChange.Controls.Add(this.btnSaveTodaysDataToServer);
            this.tpWeeklyPricePointChange.Location = new System.Drawing.Point(4, 22);
            this.tpWeeklyPricePointChange.Margin = new System.Windows.Forms.Padding(2);
            this.tpWeeklyPricePointChange.Name = "tpWeeklyPricePointChange";
            this.tpWeeklyPricePointChange.Padding = new System.Windows.Forms.Padding(2);
            this.tpWeeklyPricePointChange.Size = new System.Drawing.Size(659, 324);
            this.tpWeeklyPricePointChange.TabIndex = 0;
            this.tpWeeklyPricePointChange.Text = "Weekly Price";
            this.tpWeeklyPricePointChange.UseVisualStyleBackColor = true;
            // 
            // btnCheckNews
            // 
            this.btnCheckNews.Location = new System.Drawing.Point(361, 287);
            this.btnCheckNews.Name = "btnCheckNews";
            this.btnCheckNews.Size = new System.Drawing.Size(93, 25);
            this.btnCheckNews.TabIndex = 4;
            this.btnCheckNews.Text = "Check News";
            this.btnCheckNews.UseVisualStyleBackColor = true;
            this.btnCheckNews.Click += new System.EventHandler(this.btnCheckNews_Click);
            // 
            // btnEditCompany
            // 
            this.btnEditCompany.Location = new System.Drawing.Point(268, 287);
            this.btnEditCompany.Name = "btnEditCompany";
            this.btnEditCompany.Size = new System.Drawing.Size(87, 25);
            this.btnEditCompany.TabIndex = 3;
            this.btnEditCompany.Text = "Edit Company";
            this.btnEditCompany.UseVisualStyleBackColor = true;
            this.btnEditCompany.Click += new System.EventHandler(this.btnEditCompany_Click);
            // 
            // btnAddCompany
            // 
            this.btnAddCompany.Location = new System.Drawing.Point(167, 287);
            this.btnAddCompany.Name = "btnAddCompany";
            this.btnAddCompany.Size = new System.Drawing.Size(95, 25);
            this.btnAddCompany.TabIndex = 2;
            this.btnAddCompany.Text = "Add Company";
            this.btnAddCompany.UseVisualStyleBackColor = true;
            this.btnAddCompany.Click += new System.EventHandler(this.btnAddCompany_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Symbols});
            this.dataGridView1.Location = new System.Drawing.Point(4, 5);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(651, 277);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // Symbols
            // 
            this.Symbols.HeaderText = "Symbols";
            this.Symbols.Name = "Symbols";
            // 
            // btnSaveTodaysDataToServer
            // 
            this.btnSaveTodaysDataToServer.Location = new System.Drawing.Point(4, 287);
            this.btnSaveTodaysDataToServer.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveTodaysDataToServer.Name = "btnSaveTodaysDataToServer";
            this.btnSaveTodaysDataToServer.Size = new System.Drawing.Size(158, 25);
            this.btnSaveTodaysDataToServer.TabIndex = 1;
            this.btnSaveTodaysDataToServer.Text = "Save Todays Data To Server";
            this.btnSaveTodaysDataToServer.UseVisualStyleBackColor = true;
            this.btnSaveTodaysDataToServer.Click += new System.EventHandler(this.btnSaveTodaysDataToServer_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpWeeklyPricePointChange);
            this.tabControl1.Controls.Add(this.tpWeeklyGrowthChange);
            this.tabControl1.Location = new System.Drawing.Point(9, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(667, 350);
            this.tabControl1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 374);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tpWeeklyGrowthChange.ResumeLayout(false);
            this.tpWeeklyPricePointChange.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpWeeklyGrowthChange;
        private LiveCharts.WinForms.CartesianChart CartChartGrowthChange;
        private System.Windows.Forms.TabPage tpWeeklyPricePointChange;
        private System.Windows.Forms.Button btnCheckNews;
        private System.Windows.Forms.Button btnEditCompany;
        private System.Windows.Forms.Button btnAddCompany;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Symbols;
        private System.Windows.Forms.Button btnSaveTodaysDataToServer;
        private System.Windows.Forms.TabControl tabControl1;
    }
}

