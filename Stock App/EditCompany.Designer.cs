namespace Stock_App
{
    partial class EditCompany
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
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblCompanySymbol = new System.Windows.Forms.Label();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.lblIndustry = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.cboIndustry = new System.Windows.Forms.ComboBox();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtDividendYield = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDateAdded = new System.Windows.Forms.Label();
            this.txtDateAdded = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(125, 12);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(157, 20);
            this.txtCompanyName.TabIndex = 0;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Location = new System.Drawing.Point(34, 15);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(85, 13);
            this.lblCompanyName.TabIndex = 1;
            this.lblCompanyName.Text = "Company Name:";
            // 
            // lblCompanySymbol
            // 
            this.lblCompanySymbol.AutoSize = true;
            this.lblCompanySymbol.Location = new System.Drawing.Point(75, 41);
            this.lblCompanySymbol.Name = "lblCompanySymbol";
            this.lblCompanySymbol.Size = new System.Drawing.Size(44, 13);
            this.lblCompanySymbol.TabIndex = 2;
            this.lblCompanySymbol.Text = "Symbol:";
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(125, 38);
            this.txtSymbol.MaxLength = 5;
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(71, 20);
            this.txtSymbol.TabIndex = 3;
            // 
            // lblIndustry
            // 
            this.lblIndustry.AutoSize = true;
            this.lblIndustry.Location = new System.Drawing.Point(72, 67);
            this.lblIndustry.Name = "lblIndustry";
            this.lblIndustry.Size = new System.Drawing.Size(47, 13);
            this.lblIndustry.TabIndex = 4;
            this.lblIndustry.Text = "Industry:";
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(42, 146);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(77, 13);
            this.lblSummary.TabIndex = 5;
            this.lblSummary.Text = "Breif Summary:";
            // 
            // cboIndustry
            // 
            this.cboIndustry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIndustry.FormattingEnabled = true;
            this.cboIndustry.Location = new System.Drawing.Point(125, 64);
            this.cboIndustry.Name = "cboIndustry";
            this.cboIndustry.Size = new System.Drawing.Size(121, 21);
            this.cboIndustry.TabIndex = 6;
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(125, 143);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(201, 20);
            this.txtSummary.TabIndex = 7;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(125, 169);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(201, 50);
            this.txtNotes.TabIndex = 8;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(81, 172);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(38, 13);
            this.lblNotes.TabIndex = 9;
            this.lblNotes.Text = "Notes:";
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(251, 225);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(170, 225);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 25);
            this.button1.TabIndex = 12;
            this.button1.Text = "Update Dividend";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtDividendYield
            // 
            this.txtDividendYield.Location = new System.Drawing.Point(125, 91);
            this.txtDividendYield.Name = "txtDividendYield";
            this.txtDividendYield.ReadOnly = true;
            this.txtDividendYield.Size = new System.Drawing.Size(100, 20);
            this.txtDividendYield.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Latest Dividend Yield:";
            // 
            // lblDateAdded
            // 
            this.lblDateAdded.AutoSize = true;
            this.lblDateAdded.Location = new System.Drawing.Point(52, 120);
            this.lblDateAdded.Name = "lblDateAdded";
            this.lblDateAdded.Size = new System.Drawing.Size(67, 13);
            this.lblDateAdded.TabIndex = 15;
            this.lblDateAdded.Text = "Date Added:";
            // 
            // txtDateAdded
            // 
            this.txtDateAdded.Location = new System.Drawing.Point(125, 117);
            this.txtDateAdded.Name = "txtDateAdded";
            this.txtDateAdded.ReadOnly = true;
            this.txtDateAdded.Size = new System.Drawing.Size(100, 20);
            this.txtDateAdded.TabIndex = 16;
            // 
            // EditCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 261);
            this.Controls.Add(this.txtDateAdded);
            this.Controls.Add(this.lblDateAdded);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDividendYield);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.cboIndustry);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblIndustry);
            this.Controls.Add(this.txtSymbol);
            this.Controls.Add(this.lblCompanySymbol);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.txtCompanyName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditCompany";
            this.Text = "Edit Company";
            this.Load += new System.EventHandler(this.EditCompany_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblCompanySymbol;
        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.Label lblIndustry;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.ComboBox cboIndustry;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDividendYield;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDateAdded;
        private System.Windows.Forms.TextBox txtDateAdded;
    }
}