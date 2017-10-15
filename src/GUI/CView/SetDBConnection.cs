using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace GUI.CView
{
    public partial class SetDBConnection : Form
    {
        private string DBUrl;
        private string DBName;
        private string DBUsername;
        private string DBPassword;

        public string getDBUrl()
        {
            return (DBUrl == null) ? "" : DBUrl;
        }

        public string getDBUsername()
        {
            return (DBUsername == null) ? "" : DBUsername;
        }

        public string getDBPassword()
        {
            return (DBPassword == null) ? "" : DBPassword;
        }

        public string getDBName()
        {
            return (DBName == null) ? "" : DBName;
        }

        public SetDBConnection()
        {
            InitializeComponent();
            m_DBUrl_Value.Text = Dns.GetHostName()+"\\SQLEXPRESS";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void m_DBUrl_Click(object sender, EventArgs e)
        {

        }

        private void m_DBUrl_Value_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SetDBConnection_Load(object sender, EventArgs e)
        {

        }

        private void m_TestDBConnection_Button_Click(object sender, EventArgs e)
        {
            DBUrl = m_DBUrl_Value.Text;
            DBUsername = m_DBUsername_Value.Text;
            DBPassword = m_DBPassword_Value.Text;
            DBName = m_DBName_Value.Text;

            this.Close();
        }

        private void m_DBUsername_Value_TextChanged(object sender, EventArgs e)
        {

        }

        private void m_DBName_Value_TextChanged(object sender, EventArgs e)
        {

        }

        private void m_DBPassword_Value_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
