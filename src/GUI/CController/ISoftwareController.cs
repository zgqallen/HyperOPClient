

using System.Collections.Generic;

using GUI.CView;

namespace GUI.CController
{
    public interface ISoftwareController
    {
        void ShowView();

        void DisposeView();

        void CloseServerTabView(IServerView p_ServerView);

        List<string[]> SearchOPCServers(string p_MachineName);

        IServerView ConnectToOPCServer(string p_MachineName, string p_ServerID, string[] DBinfo);

        void DisconnectFromAllOPCServers();
    }
}
