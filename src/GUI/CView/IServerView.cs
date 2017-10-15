
using System.Windows.Forms;

using GUI.CModel;

namespace GUI.CView
{
    public interface IServerView
    {
        TabPage GetTabPage();

        void ServerModelChange(object p_Sender, ModelChangeEventArgs p_EvArgs);

        int GetUpdateRate();
    }
}
