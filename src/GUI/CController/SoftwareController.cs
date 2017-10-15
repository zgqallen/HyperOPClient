
using System.Collections.Generic;
using System.Linq;

using GUI.CView;
using COPC.Common;

namespace GUI.CController
{
    class SoftwareController : ISoftwareController
    {
        private const string CLIENT_NAME = "HyperOPClient";
        private const string GROUP_NAME = "ALLDataGroup";

        private AbstractViewFactory m_Factory;
        private ISoftwareView m_SoftwareView;
        
        private string[] m_DBinfo; 

        private List<IServerController> m_ServerControllerList;

        public SoftwareController(AbstractViewFactory p_Factory)
        {
            m_Factory = p_Factory;
            m_SoftwareView = m_Factory.CreateSoftwareView(this);

            m_ServerControllerList = new List<IServerController>();
            m_DBinfo = new string[3];
        }

        public void ShowView()
        {
            m_SoftwareView.Display();
        }

        public void DisposeView()
        {
            m_SoftwareView.Shutdown();
        }

        public void CloseServerTabView(IServerView p_ServerView)
        {
            int l_Index = m_SoftwareView.CloseServerTabView(p_ServerView);

            m_ServerControllerList.RemoveAt(l_Index - 1);
        }

        public List<string[]> SearchOPCServers(string p_MachineName)
        {
            OPCServerLister l_ServerLister = new OPCServerLister();
            List<OPCServerInfo> l_GlobalServerList = new List<OPCServerInfo>();
            OPCServerInfo[] l_ServerListV1 = null;
            OPCServerInfo[] l_ServerListV2 = null;
            OPCServerInfo[] l_ServerListV3 = null;

            l_ServerLister.ListAllServersOnMachineV1(p_MachineName, out l_ServerListV1);
            l_ServerLister.ListAllServersOnMachineV2(p_MachineName, out l_ServerListV2);
            l_ServerLister.ListAllServersOnMachineV3(p_MachineName, out l_ServerListV3);

            if (l_ServerListV1 != null)
            {
                l_GlobalServerList.AddRange(l_ServerListV1);
            }
            if (l_ServerListV2 != null)
            {
                l_GlobalServerList.AddRange(l_ServerListV2);
            }
            if (l_ServerListV3 != null)
            {
                l_GlobalServerList.AddRange(l_ServerListV3);
            }

            return FromOPCServerInfoListToStringTabList(p_MachineName, l_GlobalServerList.Distinct()); 
        }

        public IServerView ConnectToOPCServer(string p_MachineName, string p_ServerID, string[] DBinfo)
        {
            IServerController l_ServerController = new ServerController(this, m_Factory, p_MachineName, p_ServerID);

            if (l_ServerController.GetVeriteqOPCServer())
            {
                l_ServerController.ConnectDatabase(DBinfo);
            }
            l_ServerController.Connect(CLIENT_NAME);
            l_ServerController.CreateGroup(GROUP_NAME);
            l_ServerController.BrowseServerAndAddItems();
            l_ServerController.ReadItems();
            l_ServerController.ActivateItems();

            m_ServerControllerList.Add(l_ServerController);

            return l_ServerController.GetView();
        }

        public void DisconnectFromAllOPCServers()
        {
            foreach (IServerController l_ServerController in m_ServerControllerList)
            {
                if (l_ServerController.GetVeriteqOPCServer())
                {
                    l_ServerController.DisconnectDatabase();
                }
                l_ServerController.Disconnect();
            }
        }

        private List<string[]> FromOPCServerInfoListToStringTabList(string p_MachineName, IEnumerable<OPCServerInfo> p_ServerList)
        {
            List<string[]> l_ServerList = new List<string[]>();

            foreach (OPCServerInfo l_ServerInfo in p_ServerList)
            {
                string[] l_ServerTabInfo = new string[4];

                l_ServerTabInfo[0] = p_MachineName;
                l_ServerTabInfo[1] = l_ServerInfo.ServerName;
                l_ServerTabInfo[2] = l_ServerInfo.ProgID;
                l_ServerTabInfo[3] = l_ServerInfo.ClsID.ToString().ToUpper();

                l_ServerList.Add(l_ServerTabInfo);
            }

            return l_ServerList;
        }
    }
}
