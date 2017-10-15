using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI.CView
{
    public partial class ErrorMessage : Form
    {
        public ErrorMessage()
        {
            InitializeComponent();
        }

        public void SetErrorMessage(string msg)
        {
            this.v_errorMessage.Text = msg;
        }

        private void v_errorMessage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
