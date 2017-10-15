/*
 *  SetValueFrame.Designer.cs
 *
 *  Copyright (C) 2015 Stephane Delapierre <stephane.delapierre@gmail.com>
 *
 *  Contributors : Julien Hannier <julien.hannier@gmail.com>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

namespace GUI.CView
{
    partial class SetValueFrame
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
            this.m_ValueLabel = new System.Windows.Forms.Label();
            this.m_ValueTextBox = new System.Windows.Forms.TextBox();
            this.m_OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_ValueLabel
            // 
            this.m_ValueLabel.AutoSize = true;
            this.m_ValueLabel.Location = new System.Drawing.Point(16, 16);
            this.m_ValueLabel.Name = "m_ValueLabel";
            this.m_ValueLabel.Size = new System.Drawing.Size(77, 12);
            this.m_ValueLabel.TabIndex = 0;
            this.m_ValueLabel.Text = "Write Value:";
            this.m_ValueLabel.Click += new System.EventHandler(this.m_ValueLabel_Click);
            // 
            // m_ValueTextBox
            // 
            this.m_ValueTextBox.Location = new System.Drawing.Point(116, 13);
            this.m_ValueTextBox.Name = "m_ValueTextBox";
            this.m_ValueTextBox.Size = new System.Drawing.Size(107, 21);
            this.m_ValueTextBox.TabIndex = 1;
            this.m_ValueTextBox.TextChanged += new System.EventHandler(this.m_ValueTextBox_TextChanged);
            // 
            // m_OkButton
            // 
            this.m_OkButton.Location = new System.Drawing.Point(84, 42);
            this.m_OkButton.Name = "m_OkButton";
            this.m_OkButton.Size = new System.Drawing.Size(75, 21);
            this.m_OkButton.TabIndex = 2;
            this.m_OkButton.Text = "Ok";
            this.m_OkButton.UseVisualStyleBackColor = true;
            this.m_OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // SetValueFrame
            // 
            this.AcceptButton = this.m_OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 66);
            this.Controls.Add(this.m_ValueLabel);
            this.Controls.Add(this.m_ValueTextBox);
            this.Controls.Add(this.m_OkButton);
            this.MaximumSize = new System.Drawing.Size(300, 104);
            this.MinimumSize = new System.Drawing.Size(300, 104);
            this.Name = "SetValueFrame";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Write Specific Value";
            this.Load += new System.EventHandler(this.SetValueFrame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_ValueLabel;
        private System.Windows.Forms.TextBox m_ValueTextBox;
        private System.Windows.Forms.Button m_OkButton;
    }
}