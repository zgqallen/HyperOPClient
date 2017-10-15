namespace GUI.CView
{
    partial class SetDBConnection
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
            this.m_DBUrl = new System.Windows.Forms.Label();
            this.m_DBUrl_Value = new System.Windows.Forms.TextBox();
            this.m_DBUserLabel = new System.Windows.Forms.Label();
            this.m_DBUsername_Value = new System.Windows.Forms.TextBox();
            this.m_DBPasswordLabel = new System.Windows.Forms.Label();
            this.m_DBPassword_Value = new System.Windows.Forms.TextBox();
            this.m_TestDBConnection_Button = new System.Windows.Forms.Button();
            this.m_DBName = new System.Windows.Forms.Label();
            this.m_DBName_Value = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_DBUrl
            // 
            this.m_DBUrl.AutoSize = true;
            this.m_DBUrl.Location = new System.Drawing.Point(12, 19);
            this.m_DBUrl.Name = "m_DBUrl";
            this.m_DBUrl.Size = new System.Drawing.Size(83, 12);
            this.m_DBUrl.TabIndex = 0;
            this.m_DBUrl.Text = "Database URL:";
            this.m_DBUrl.Click += new System.EventHandler(this.m_DBUrl_Click);
            // 
            // m_DBUrl_Value
            // 
            this.m_DBUrl_Value.Location = new System.Drawing.Point(101, 13);
            this.m_DBUrl_Value.Name = "m_DBUrl_Value";
            this.m_DBUrl_Value.Size = new System.Drawing.Size(171, 21);
            this.m_DBUrl_Value.TabIndex = 1;
            this.m_DBUrl_Value.Text = "127.0.0.1";
            this.m_DBUrl_Value.TextChanged += new System.EventHandler(this.m_DBUrl_Value_TextChanged);
            // 
            // m_DBUserLabel
            // 
            this.m_DBUserLabel.AutoSize = true;
            this.m_DBUserLabel.Location = new System.Drawing.Point(13, 86);
            this.m_DBUserLabel.Name = "m_DBUserLabel";
            this.m_DBUserLabel.Size = new System.Drawing.Size(59, 12);
            this.m_DBUserLabel.TabIndex = 2;
            this.m_DBUserLabel.Text = "UserName:";
            this.m_DBUserLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // m_DBUsername_Value
            // 
            this.m_DBUsername_Value.Location = new System.Drawing.Point(101, 81);
            this.m_DBUsername_Value.Name = "m_DBUsername_Value";
            this.m_DBUsername_Value.Size = new System.Drawing.Size(171, 21);
            this.m_DBUsername_Value.TabIndex = 3;
            this.m_DBUsername_Value.TextChanged += new System.EventHandler(this.m_DBUsername_Value_TextChanged);
            // 
            // m_DBPasswordLabel
            // 
            this.m_DBPasswordLabel.AutoSize = true;
            this.m_DBPasswordLabel.Location = new System.Drawing.Point(12, 119);
            this.m_DBPasswordLabel.Name = "m_DBPasswordLabel";
            this.m_DBPasswordLabel.Size = new System.Drawing.Size(59, 12);
            this.m_DBPasswordLabel.TabIndex = 4;
            this.m_DBPasswordLabel.Text = "Password:";
            this.m_DBPasswordLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // m_DBPassword_Value
            // 
            this.m_DBPassword_Value.Location = new System.Drawing.Point(101, 116);
            this.m_DBPassword_Value.Name = "m_DBPassword_Value";
            this.m_DBPassword_Value.Size = new System.Drawing.Size(171, 21);
            this.m_DBPassword_Value.TabIndex = 5;
            this.m_DBPassword_Value.TextChanged += new System.EventHandler(this.m_DBPassword_Value_TextChanged);
            // 
            // m_TestDBConnection_Button
            // 
            this.m_TestDBConnection_Button.Location = new System.Drawing.Point(67, 155);
            this.m_TestDBConnection_Button.Name = "m_TestDBConnection_Button";
            this.m_TestDBConnection_Button.Size = new System.Drawing.Size(145, 23);
            this.m_TestDBConnection_Button.TabIndex = 6;
            this.m_TestDBConnection_Button.Text = "Configure Done";
            this.m_TestDBConnection_Button.UseVisualStyleBackColor = true;
            this.m_TestDBConnection_Button.Click += new System.EventHandler(this.m_TestDBConnection_Button_Click);
            // 
            // m_DBName
            // 
            this.m_DBName.AutoSize = true;
            this.m_DBName.Location = new System.Drawing.Point(13, 50);
            this.m_DBName.Name = "m_DBName";
            this.m_DBName.Size = new System.Drawing.Size(89, 12);
            this.m_DBName.TabIndex = 7;
            this.m_DBName.Text = "Database Name:";
            // 
            // m_DBName_Value
            // 
            this.m_DBName_Value.Location = new System.Drawing.Point(101, 47);
            this.m_DBName_Value.Name = "m_DBName_Value";
            this.m_DBName_Value.Size = new System.Drawing.Size(171, 21);
            this.m_DBName_Value.TabIndex = 8;
            this.m_DBName_Value.Text = "OPCDB";
            this.m_DBName_Value.TextChanged += new System.EventHandler(this.m_DBName_Value_TextChanged);
            // 
            // SetDBConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 212);
            this.Controls.Add(this.m_DBName_Value);
            this.Controls.Add(this.m_DBName);
            this.Controls.Add(this.m_TestDBConnection_Button);
            this.Controls.Add(this.m_DBPassword_Value);
            this.Controls.Add(this.m_DBPasswordLabel);
            this.Controls.Add(this.m_DBUsername_Value);
            this.Controls.Add(this.m_DBUserLabel);
            this.Controls.Add(this.m_DBUrl_Value);
            this.Controls.Add(this.m_DBUrl);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.Name = "SetDBConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SetDBConnection";
            this.Load += new System.EventHandler(this.SetDBConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_DBUrl;
        private System.Windows.Forms.TextBox m_DBUrl_Value;
        private System.Windows.Forms.Label m_DBUserLabel;
        private System.Windows.Forms.TextBox m_DBUsername_Value;
        private System.Windows.Forms.Label m_DBPasswordLabel;
        private System.Windows.Forms.TextBox m_DBPassword_Value;
        private System.Windows.Forms.Button m_TestDBConnection_Button;
        private System.Windows.Forms.Label m_DBName;
        private System.Windows.Forms.TextBox m_DBName_Value;

    }
}