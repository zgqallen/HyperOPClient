
using GUI.CController;

namespace GUI.CView
{
    public abstract class AbstractViewFactory
    {
        public abstract ISoftwareView CreateSoftwareView(ISoftwareController p_SoftwareController);

        public abstract IServerView CreateServerView(IServerController p_ServerController, string p_ServerName);
    }
}
