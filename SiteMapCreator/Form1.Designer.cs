namespace SiteMapCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.listUrl = new System.Windows.Forms.ListView();
            this.Sira = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Site = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnBasla = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Site Url :";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(67, 20);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(240, 20);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "http://www.tuncaybas.com/";
            // 
            // listUrl
            // 
            this.listUrl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Sira,
            this.Site});
            this.listUrl.FullRowSelect = true;
            this.listUrl.GridLines = true;
            this.listUrl.Location = new System.Drawing.Point(16, 49);
            this.listUrl.Name = "listUrl";
            this.listUrl.Size = new System.Drawing.Size(1064, 442);
            this.listUrl.TabIndex = 4;
            this.listUrl.UseCompatibleStateImageBehavior = false;
            this.listUrl.View = System.Windows.Forms.View.Details;
            // 
            // Sira
            // 
            this.Sira.Text = "Sira";
            // 
            // Site
            // 
            this.Site.Text = "Site";
            this.Site.Width = 613;
            // 
            // btnBasla
            // 
            this.btnBasla.Location = new System.Drawing.Point(335, 20);
            this.btnBasla.Name = "btnBasla";
            this.btnBasla.Size = new System.Drawing.Size(75, 23);
            this.btnBasla.TabIndex = 5;
            this.btnBasla.Text = "Başla";
            this.btnBasla.UseVisualStyleBackColor = true;
            this.btnBasla.Click += new System.EventHandler(this.btnBasla_Click);
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(440, 25);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(0, 13);
            this.lblLog.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 503);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.btnBasla);
            this.Controls.Add(this.listUrl);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SiteMap Creator & Tuncay BAŞ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.ListView listUrl;
        private System.Windows.Forms.ColumnHeader Sira;
        private System.Windows.Forms.ColumnHeader Site;
        private System.Windows.Forms.Button btnBasla;
        private System.Windows.Forms.Label lblLog;
    }
}

