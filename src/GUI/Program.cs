using System;
using System.Windows.Forms;

using GUI.CView;
using GUI.CController;

namespace CGUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AbstractViewFactory l_Factory = WindowsFormViewFactory.GetInstance();
            ISoftwareController l_Controller = new SoftwareController(l_Factory);
            l_Controller.ShowView();
        }
    }
}
