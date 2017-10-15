
using System.Collections.Generic;

namespace GUI.CModel
{
    public interface IServerModel
    {
        event ModelChangeEventHandler ModelChanged;

        List<int> GetServerHandleList();

        int GetServerHandleFromItemId(string p_ItemId);

        int GetClientHandleFromServerHandle(int p_ServerHandle);

        IOPCItem GetItemFromClientHandle(int p_ClientHandle);

        void AddItem(int p_ClientHandle, int p_ServerHandle, Dictionary<string, string> p_Properties);

        void UpdateItem(int p_ClientHandle, Dictionary<string, string> p_Properties);
    }
}
