

using System.Collections.Generic;

using GUI.CView;

namespace GUI.CController
{
    public interface IServerController
    {
        IServerView GetView();

        void CloseView();

        bool GetVeriteqOPCServer();

        void Connect(string p_ClientName);

        void Disconnect();

        void ConnectDatabase(string[] DBinfo);

        void DisconnectDatabase();
        
        void CreateGroup(string p_GroupName);

        void BrowseServerAndAddItems();

        void ReadItems();

        void ActivateItems();

        void WriteItems(List<string> p_ItemsId, string p_Value);
    }
}
