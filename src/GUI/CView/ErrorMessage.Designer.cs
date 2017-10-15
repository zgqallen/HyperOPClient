namespace GUI.CView
{
    partial class ErrorMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorMessage));
            this.v_errorMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // v_errorMessage
            // 
            this.v_errorMessage.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v_errorMessage.Location = new System.Drawing.Point(12, 12);
            this.v_errorMessage.Multiline = true;
            this.v_errorMessage.Name = "v_errorMessage";
            this.v_errorMessage.Size = new System.Drawing.Size(378, 95);
            this.v_errorMessage.TabIndex = 0;
            this.v_errorMessage.TextChanged += new System.EventHandler(this.v_errorMessage_TextChanged);
            // 
            // ErrorMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 119);
            this.Controls.Add(this.v_errorMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ErrorMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ErrorMessage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox v_errorMessage;
    }
}