
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI.CView
{
    public partial class SetValueFrame : Form
    {
        private string m_Value;

        public SetValueFrame()
        {
            InitializeComponent();
        }

        public string GetValue()
        {
            return (m_Value == null) ? "" : m_Value;
        }

        private void SetValueFrame_Load(object sender, EventArgs e)
        {
            m_ValueTextBox.Focus();
        }

        private void OkButton_Click(object p_Sender, EventArgs p_EvArgs)
        {
            m_Value = m_ValueTextBox.Text;
            this.Close();
        }

        private void m_ValueLabel_Click(object sender, EventArgs e)
        {

        }

        private void m_ValueTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
