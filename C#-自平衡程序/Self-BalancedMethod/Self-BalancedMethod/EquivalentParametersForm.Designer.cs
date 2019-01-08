namespace Self_BalancedMethod {
    partial class EquivalentParametersForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EquivalentParametersForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtParmAp = new System.Windows.Forms.TextBox();
            this.txtParmEp = new System.Windows.Forms.TextBox();
            this.txtParmLu = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtParmGamma1 = new System.Windows.Forms.TextBox();
            this.txtParmW = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtParmAp);
            this.groupBox1.Controls.Add(this.txtParmEp);
            this.groupBox1.Controls.Add(this.txtParmLu);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.txtParmGamma1);
            this.groupBox1.Controls.Add(this.txtParmW);
            this.groupBox1.Location = new System.Drawing.Point(12, 289);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 221);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "等效参数";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(347, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(242, 68);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(347, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(242, 68);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtParmAp
            // 
            this.txtParmAp.Location = new System.Drawing.Point(104, 194);
            this.txtParmAp.Name = "txtParmAp";
            this.txtParmAp.Size = new System.Drawing.Size(177, 21);
            this.txtParmAp.TabIndex = 5;
            // 
            // txtParmEp
            // 
            this.txtParmEp.Location = new System.Drawing.Point(104, 151);
            this.txtParmEp.Name = "txtParmEp";
            this.txtParmEp.Size = new System.Drawing.Size(177, 21);
            this.txtParmEp.TabIndex = 4;
            // 
            // txtParmLu
            // 
            this.txtParmLu.Location = new System.Drawing.Point(104, 110);
            this.txtParmLu.Name = "txtParmLu";
            this.txtParmLu.Size = new System.Drawing.Size(177, 21);
            this.txtParmLu.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 17);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(85, 201);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // txtParmGamma1
            // 
            this.txtParmGamma1.Location = new System.Drawing.Point(104, 67);
            this.txtParmGamma1.Name = "txtParmGamma1";
            this.txtParmGamma1.Size = new System.Drawing.Size(177, 21);
            this.txtParmGamma1.TabIndex = 1;
            // 
            // txtParmW
            // 
            this.txtParmW.Location = new System.Drawing.Point(104, 20);
            this.txtParmW.Name = "txtParmW";
            this.txtParmW.Size = new System.Drawing.Size(177, 21);
            this.txtParmW.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(672, 283);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // EquivalentParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 519);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EquivalentParametersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自平衡等效换算参数";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtParmGamma1;
        private System.Windows.Forms.TextBox txtParmW;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtParmLu;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtParmAp;
        private System.Windows.Forms.TextBox txtParmEp;
    }
}