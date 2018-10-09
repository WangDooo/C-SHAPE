namespace StudentScoreApp {
    partial class FlashForm {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlashForm));
            this.label1 = new System.Windows.Forms.Label();
            this.linkAdminLogin = new System.Windows.Forms.LinkLabel();
            this.linkUserLogin = new System.Windows.Forms.LinkLabel();
            this.linkExit = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("华文隶书", 30F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "学生成绩管理系统";
            // 
            // linkAdminLogin
            // 
            this.linkAdminLogin.AutoSize = true;
            this.linkAdminLogin.BackColor = System.Drawing.Color.Transparent;
            this.linkAdminLogin.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkAdminLogin.Location = new System.Drawing.Point(258, 305);
            this.linkAdminLogin.Name = "linkAdminLogin";
            this.linkAdminLogin.Size = new System.Drawing.Size(114, 20);
            this.linkAdminLogin.TabIndex = 1;
            this.linkAdminLogin.TabStop = true;
            this.linkAdminLogin.Text = "管理员登录";
            this.linkAdminLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAdminLogin_LinkClicked);
            // 
            // linkUserLogin
            // 
            this.linkUserLogin.AutoSize = true;
            this.linkUserLogin.BackColor = System.Drawing.Color.Transparent;
            this.linkUserLogin.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkUserLogin.Location = new System.Drawing.Point(391, 305);
            this.linkUserLogin.Name = "linkUserLogin";
            this.linkUserLogin.Size = new System.Drawing.Size(93, 20);
            this.linkUserLogin.TabIndex = 2;
            this.linkUserLogin.TabStop = true;
            this.linkUserLogin.Text = "用户登录";
            // 
            // linkExit
            // 
            this.linkExit.AutoSize = true;
            this.linkExit.BackColor = System.Drawing.Color.Transparent;
            this.linkExit.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.linkExit.Location = new System.Drawing.Point(508, 305);
            this.linkExit.Name = "linkExit";
            this.linkExit.Size = new System.Drawing.Size(51, 20);
            this.linkExit.TabIndex = 3;
            this.linkExit.TabStop = true;
            this.linkExit.Text = "退出";
            this.linkExit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkExit_LinkClicked);
            // 
            // FlashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(584, 334);
            this.ControlBox = false;
            this.Controls.Add(this.linkExit);
            this.Controls.Add(this.linkUserLogin);
            this.Controls.Add(this.linkAdminLogin);
            this.Controls.Add(this.label1);
            this.Name = "FlashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkAdminLogin;
        private System.Windows.Forms.LinkLabel linkUserLogin;
        private System.Windows.Forms.LinkLabel linkExit;
    }
}

