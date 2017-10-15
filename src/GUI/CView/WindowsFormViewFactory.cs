
using GUI.CController;

namespace GUI.CView
{
    class WindowsFormViewFactory : AbstractViewFactory
    {
        private static WindowsFormViewFactory m_Instance = null;

        private WindowsFormViewFactory()
        {
        }

        public static WindowsFormViewFactory GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new WindowsFormViewFactory();
            }
            return m_Instance;
        }

        public override ISoftwareView CreateSoftwareView(ISoftwareController p_SoftwareController)
        {
            return new SoftwareFrame(p_SoftwareController);
        }

        public override IServerView CreateServerView(IServerController p_ServerController, string p_ServerName)
        {
            return new ServerTabUserControl(p_ServerController, p_ServerName);
        }
    }
}
