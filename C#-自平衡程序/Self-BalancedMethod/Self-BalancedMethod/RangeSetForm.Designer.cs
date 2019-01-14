namespace Self_BalancedMethod {
    partial class RangeSetForm {
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
            this.ch_text = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.range_new = new System.Windows.Forms.TextBox();
            this.range_new_big = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ok_button = new System.Windows.Forms.Button();
            this.ok_button_full = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ch_text
            // 
            this.ch_text.AutoSize = true;
            this.ch_text.Location = new System.Drawing.Point(86, 9);
            this.ch_text.Name = "ch_text";
            this.ch_text.Size = new System.Drawing.Size(71, 12);
            this.ch_text.TabIndex = 13;
            this.ch_text.Text = "量程(CH01):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "量程零位:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "量程上限:";
            // 
            // range_new
            // 
            this.range_new.Location = new System.Drawing.Point(77, 37);
            this.range_new.Name = "range_new";
            this.range_new.Size = new System.Drawing.Size(90, 21);
            this.range_new.TabIndex = 20;
            // 
            // range_new_big
            // 
            this.range_new_big.Location = new System.Drawing.Point(77, 71);
            this.range_new_big.Name = "range_new_big";
            this.range_new_big.Size = new System.Drawing.Size(90, 21);
            this.range_new_big.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "(mm)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "(mm)";
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(208, 35);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(64, 23);
            this.ok_button.TabIndex = 24;
            this.ok_button.Text = "零位修改";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // ok_button_full
            // 
            this.ok_button_full.Location = new System.Drawing.Point(208, 69);
            this.ok_button_full.Name = "ok_button_full";
            this.ok_button_full.Size = new System.Drawing.Size(64, 23);
            this.ok_button_full.TabIndex = 25;
            this.ok_button_full.Text = "满位修改";
            this.ok_button_full.UseVisualStyleBackColor = true;
            this.ok_button_full.Click += new System.EventHandler(this.ok_button_full_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(145, 98);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(127, 23);
            this.cancel_button.TabIndex = 26;
            this.cancel_button.Text = "取消";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // RangeSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 130);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button_full);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.range_new_big);
            this.Controls.Add(this.range_new);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ch_text);
            this.Name = "RangeSetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "量程设置";
            this.Load += new System.EventHandler(this.RangeSetForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ch_text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox range_new;
        private System.Windows.Forms.TextBox range_new_big;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button ok_button_full;
        private System.Windows.Forms.Button cancel_button;
    }
}