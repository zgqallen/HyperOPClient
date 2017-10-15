

using System;
using System.Collections.Generic;

using GUI.CUtility;

namespace GUI.CModel
{
    class OPCItemException : Exception
    {
        private string m_Id;

        public OPCItemException(string p_Message, string p_Id) : base(p_Message)
        {
            m_Id = p_Id;
        }

        public string Id
        {
            get
            {
                return m_Id;
            }
        }
    }

    class ServerModel : IServerModel
    {
        public const string ITEM_PROP_ID_KEY = "ID";
        public const string ITEM_PROP_TYPE_KEY = "TYPE";
        public const string ITEM_PROP_VALUE_KEY = "VALUE";
        public const string ITEM_PROP_QUALITY_KEY = "QUALITY";
        public const string ITEM_PROP_TIMESTAMP_KEY = "TIMESTAMP";

        public event ModelChangeEventHandler ModelChanged;

        private Dictionary<string, int> m_ItemIdToServerHandleDictionary;
        private Dictionary<int, IOPCItem> m_ClientHandleToItemDictionary;
        private Dictionary<int, int> m_ServerHandleToClientHandleDictionary;
        
        public ServerModel()
        {
            m_ItemIdToServerHandleDictionary = new Dictionary<string, int>();
            m_ClientHandleToItemDictionary = new Dictionary<int, IOPCItem>();
            m_ServerHandleToClientHandleDictionary = new Dictionary<int, int>();
        }

        public List<int> GetServerHandleList()
        {
            List<int> l_ServerHandleList = new List<int>();

            foreach (int l_Key in m_ServerHandleToClientHandleDictionary.Keys)
            {
                l_ServerHandleList.Add(l_Key);
            }

            return l_ServerHandleList;
        }

        public int GetServerHandleFromItemId(string p_ItemId)
        {
            int l_ServerHandle;

            if (!m_ItemIdToServerHandleDictionary.TryGetValue(p_ItemId, out l_ServerHandle))
            {
                throw new OPCItemException("The server handle doesn't exist in the model.", p_ItemId);
            }
            else
            {
                return l_ServerHandle;
            }
        }

        public int GetClientHandleFromServerHandle(int p_ServerHandle)
        {
            int l_ClientHandle;

            if (!m_ServerHandleToClientHandleDictionary.TryGetValue(p_ServerHandle, out l_ClientHandle))
            {
                throw new OPCItemException("The client handle doesn't exist in the model.", p_ServerHandle.ToString());
            }
            else
            {
                return l_ClientHandle;
            }
        }

        public IOPCItem GetItemFromClientHandle(int p_ClientHandle)
        {
            IOPCItem l_Item;

            if (!m_ClientHandleToItemDictionary.TryGetValue(p_ClientHandle, out l_Item))
            {
                throw new OPCItemException("The item doesn't exist in the model.", p_ClientHandle.ToString());
            }
            else
            {
                return l_Item;
            }
        }

        public void AddItem(int p_ClientHandle, int p_ServerHandle, Dictionary<string, string> p_Properties)
        {
            if (!p_Properties.ContainsKey(ITEM_PROP_ID_KEY) || !p_Properties.ContainsKey(ITEM_PROP_TYPE_KEY))
            {
                throw new OPCItemException("The item can't be added to the model.", p_ClientHandle.ToString());
            }
            else
            {
                string l_ID, l_Type, l_Value, l_Quality, l_Timestamp;
                p_Properties.TryGetValue(ITEM_PROP_ID_KEY, out l_ID);
                p_Properties.TryGetValue(ITEM_PROP_TYPE_KEY, out l_Type);

                if (!p_Properties.ContainsKey(ITEM_PROP_VALUE_KEY))
                {
                    l_Value = OPCUtility.ITEM_UNKNOWN;
                }
                else
                {
                    p_Properties.TryGetValue(ITEM_PROP_VALUE_KEY, out l_Value);
                }

                if (!p_Properties.ContainsKey(ITEM_PROP_QUALITY_KEY))
                {
                    l_Quality = OPCUtility.ITEM_UNKNOWN;
                }
                else
                {
                    p_Properties.TryGetValue(ITEM_PROP_QUALITY_KEY, out l_Quality);
                }

                if (!p_Properties.ContainsKey(ITEM_PROP_TIMESTAMP_KEY))
                {
                    l_Timestamp = OPCUtility.ITEM_UNKNOWN;
                }
                else
                {
                    p_Properties.TryGetValue(ITEM_PROP_TIMESTAMP_KEY, out l_Timestamp);
                }

                IOPCItem l_Item = new OPCItem(l_ID, l_Type);
                l_Item.Value = l_Value;
                l_Item.Quality = l_Quality;
                l_Item.Timestamp = l_Timestamp;

                m_ItemIdToServerHandleDictionary.Add(l_Item.ID, p_ServerHandle);
                m_ClientHandleToItemDictionary.Add(p_ClientHandle, l_Item);
                m_ServerHandleToClientHandleDictionary.Add(p_ServerHandle, p_ClientHandle);

                ModelChangeEventArgs l_Event = new ModelChangeEventArgs(ModelChangeEventType.ADD, l_ID, l_Type,
                    l_Value, l_Quality, l_Timestamp);
                if (ModelChanged != null)
                {
                    ModelChanged(this, l_Event);
                }
            }
        }

        public void UpdateItem(int p_ClientHandle, Dictionary<string, string> p_Properties)
        {
            if (!p_Properties.ContainsKey(ITEM_PROP_VALUE_KEY) ||
                !p_Properties.ContainsKey(ITEM_PROP_QUALITY_KEY) ||
                !p_Properties.ContainsKey(ITEM_PROP_TIMESTAMP_KEY))
            {
                throw new OPCItemException("The item can't be updated.", p_ClientHandle.ToString());
            }
            else
            {
                IOPCItem l_Item;

                if (m_ClientHandleToItemDictionary.TryGetValue(p_ClientHandle, out l_Item))
                {
                    string l_Value, l_Quality, l_Timestamp;
                    p_Properties.TryGetValue(ITEM_PROP_VALUE_KEY, out l_Value);
                    p_Properties.TryGetValue(ITEM_PROP_QUALITY_KEY, out l_Quality);
                    p_Properties.TryGetValue(ITEM_PROP_TIMESTAMP_KEY, out l_Timestamp);

                    l_Item.Value = l_Value;
                    l_Item.Quality = l_Quality;
                    l_Item.Timestamp = l_Timestamp;

                    ModelChangeEventArgs l_Event = new ModelChangeEventArgs(ModelChangeEventType.UPDATE,
                        l_Item.ID, l_Item.Type, l_Value, l_Quality, l_Timestamp);

                    if (ModelChanged != null)
                    {
                        ModelChanged(this, l_Event);
                    }
                }
            }
        }
    }
}
