

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

using GUI.CView;
using GUI.CModel;
using GUI.CErrorLog;
using GUI.CUtility;
using COPC.Server;
using COPC.Datas;
using COPC.Groups;
using COPC.Common;



namespace GUI.CController
{
    class ServerController : IServerController
    {
        private ISoftwareController m_SoftwareController;

        private IServerModel m_ServerModel;
        private IServerView m_ServerView;

        private string m_MachineName;
        private string m_ServerID;

        private Mutex m_ItemAccessMutex;
        private OPCServer m_Server;
        private OPCGroup m_Group;

        private SqlConnection Database_con = null;
        private bool VeriteqOPCServer = false;

        private Dictionary<string, string> m_DescriptionDictionary;
        private Dictionary<string, string> m_UnitDictionary;

        public const string ITEM_PROP_ID_KEY = "ID";
        public const string ITEM_PROP_VALUE_KEY = "VALUE";
        public const string ITEM_PROP_QUALITY_KEY = "QUALITY";
        public const string ITEM_PROP_TIMESTAMP_KEY = "TIMESTAMP";
        private System.Timers.Timer timer;

        public ServerController(ISoftwareController p_SoftwareController, AbstractViewFactory p_Factory, string p_MachineName, string p_ServerID)
        {
            m_SoftwareController = p_SoftwareController;

            m_ServerModel = new ServerModel();
            m_ServerView = p_Factory.CreateServerView(this, p_ServerID);
            m_ServerModel.ModelChanged += new ModelChangeEventHandler(m_ServerView.ServerModelChange);

            m_MachineName = p_MachineName;
            m_ServerID = p_ServerID;

            string[] ID_Spilts = p_ServerID.Split(new char[] { '.' });
            if (ID_Spilts[0] == "Veriteq")
            {
                VeriteqOPCServer = true;
                System.Console.WriteLine("Connect to a Veriteq Server\n");
            }

            m_DescriptionDictionary = new Dictionary<string, string>();
            m_UnitDictionary = new Dictionary<string, string>();

            m_ItemAccessMutex = new Mutex();
            m_Server = null;
            m_Group = null;
        }

        public bool GetVeriteqOPCServer()
        {
            return VeriteqOPCServer;
        }

        public IServerView GetView()
        {
            return m_ServerView;
        }

        public void CloseView()
        {
            m_SoftwareController.CloseServerTabView(m_ServerView);
        }

        public void Connect(string p_ClientName)
        {
            m_Server = new OPCServer();

            m_Server.RemoteConnect(m_ServerID, m_MachineName);

            try
            {
                m_Server.SetClientName(p_ClientName);

                m_Server.ShutdownRequested += new ShutdownRequestEventHandler(Server_ShutdownRequest);
            }
            catch (Exception l_Ex)
            {
                Disconnect();

                throw l_Ex;
            }
            /* TODO: To test connection to Server*/
            timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 30000;//mi-seconds
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed); 
        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ServerStatus st;

            try
            {
                m_Server.GetStatus(out st);
            }
            catch (Exception)
            {
                Console.WriteLine("*******Error ##############  Server disconneted##########");
                timer.Stop();
                timer.Close();
                timer.Dispose();

                ErrorMessage emsg = new ErrorMessage();
                emsg.SetErrorMessage("                      ######Error ######## \r\nThe Connection To OPC Server" + "[ " + m_ServerID + " ]：should be DOWN!!! \r\nPlease start the service of the OPC Server, then click the button quit OPC server and re-connect.");
                emsg.ShowDialog();
            }
        }

        public void Disconnect()
        {
            List<int> l_ServerHandleList = m_ServerModel.GetServerHandleList();

            if (l_ServerHandleList.Count > 0)
            {
                try
                {
                    int[] l_RemoveRes;
                    m_Group.RemoveItems(l_ServerHandleList.ToArray(), out l_RemoveRes);
                }
                catch (Exception)
                {
                }
            }

            if (m_Group != null)
            {
                m_Group.DataChanged -= new DataChangeEventHandler(Group_DataChange);

                try
                {
                    m_Group.Remove(true);
                }
                catch (Exception)
                {
                }
            }

            if (m_Server != null)
            {
                m_Server.ShutdownRequested -= new ShutdownRequestEventHandler(Server_ShutdownRequest);

                try
                {
                    m_Server.Disconnect();
                }
                catch (Exception)
                {
                }
            }
        }

        public void ConnectDatabase(string[] DBinfo)
        {
            String FullDBPath = "server=" + DBinfo[0] + ";database="+DBinfo[1]+";uid=" + DBinfo[2] + ";pwd=" + DBinfo[3];
            try
            {
                if (Database_con == null)
                {
                    Database_con = new SqlConnection();
                    Database_con.ConnectionString = FullDBPath;
                    Database_con.Open();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DisconnectDatabase()
        {
            try
            {
                if (Database_con != null)
                    Database_con.Close();
            }
            catch (Exception)
            {
            }
        }

        public void CreateGroup(string p_GroupName)
        {
            try
            {
                m_Group = m_Server.AddGroup(p_GroupName, true, m_ServerView.GetUpdateRate());
                m_Group.DataChanged += new DataChangeEventHandler(Group_DataChange);
            }
            catch (Exception l_Ex)
            {
                Disconnect();

                throw l_Ex;
            }
        }

        public void BrowseServerAndAddItems()
        {
            m_ItemAccessMutex.WaitOne();

            try
            {
                List<string> l_ItemIds = new List<string>();
                OPCNamespaceType l_ServerOrganization = m_Server.QueryOrganization();

                // Browse to root
                m_Server.ChangeBrowsePosition(OPCBrowseDirection.OPC_BROWSE_TO, "");

                if (l_ServerOrganization == OPCNamespaceType.OPC_NS_HIERARCHIAL)
                {
                    RecursiveServerHierarchialBrowse(ref l_ItemIds);
                }
                else
                {
                    RecursiveServerFlatBrowse(ref l_ItemIds);
                }

                ValidateItemIds(ref l_ItemIds);
                AddItems(l_ItemIds);
            }
            catch (Exception l_Ex)
            {
                Disconnect();

                throw l_Ex;
            }
            finally
            {
                m_ItemAccessMutex.ReleaseMutex();
            }
        }

        public void WriteToDatabase(IOPCItem p_Items)
        {

                string l_Id, l_Value, l_Quality;
                l_Id = p_Items.ID;
                l_Value = p_Items.Value;
                l_Quality = p_Items.Quality;

                string[] ID_Spilts = l_Id.Split(new char[] { '.' });

                if(ID_Spilts.Length < 3)
                {
                       /* We will not record this message to the Database*/
                       return;
                }
                
                /* Update the <ID, Description> Dictionary if the data is Px.Cx.Description */
                if(ID_Spilts[2]!= null && ID_Spilts[2] == "Description")
                {
                    string Temp_ID = ID_Spilts[0]+ID_Spilts[1];
                    if (!m_DescriptionDictionary.ContainsKey(Temp_ID))
                    {
                        m_DescriptionDictionary.Add(Temp_ID, l_Value);
                    }
                    else
                    {
                        m_DescriptionDictionary.Remove(Temp_ID);
                        m_DescriptionDictionary.Add(Temp_ID, l_Value);
                    }
                }

                /* Update the <ID, Unit> Dictionary if the data is Px.Cx.Units */
                if(ID_Spilts[2]!= null && ID_Spilts[2] == "Units")
                {
                    string Temp_ID = ID_Spilts[0]+ID_Spilts[1];
                    if (!m_UnitDictionary.ContainsKey(Temp_ID))
                    {
                        m_UnitDictionary.Add(Temp_ID, l_Value);
                    }
                    else
                    {
                        m_UnitDictionary.Remove(Temp_ID);
                        m_UnitDictionary.Add(Temp_ID, l_Value);
                    }
                }


                if (l_Value.Length == 0 || l_Value == "" || l_Value == "0" ||
                   l_Quality == "BAD" || l_Quality == "bad")
                {
                    System.Console.WriteLine("The Value is not valid to wirte into database!!!!");
                    return;
                }

                /* Write Data into Database if the data is Px.Cx.Value */
                if(ID_Spilts[2]!= null && ID_Spilts[2] == "Value")
                {
                    string Value_ID, Value_Descrition, Value, Value_Unit, Value_TimeStamp, Rec_TimeStamp;

                    Value_ID = ID_Spilts[0]+ID_Spilts[1];
                    m_DescriptionDictionary.TryGetValue(Value_ID, out Value_Descrition);
                    m_UnitDictionary.TryGetValue(Value_ID, out Value_Unit);

                    Value_TimeStamp = p_Items.Timestamp;
                    Value = p_Items.Value;

                    DateTime recordtime = DateTime.Now;
                    Rec_TimeStamp = recordtime.ToLocalTime().ToString();

                    /* Get a statement to write data into SQL Server Database */
                    if (Database_con != null)
                    {
                        /* Schema 
                        T_OPCData( probeindex varchar(10)，
	                    probename varchar(36),
	                    value float,
	                    unit  varchar(10),
	                    capturetime dateTime,
	                    recordtime dateTime
                        )
                        */
                        string CommandText;
                        CommandText = "insert into T_OPCData values(";
                        CommandText += "'" + ID_Spilts[0] + "', ";
                        CommandText += "'" + ID_Spilts[1] + "', ";
                        CommandText += "'" + Value_Descrition + "', ";
                        switch (Value_Unit.Substring(Value_Unit.Length - 1, 1))
                        {
                            case "C":
                                CommandText += "Convert(decimal(18,2)," + Value + "), ";
                                break;
                            case "H":
                                CommandText += "Convert(decimal(18,1)," + Value + "), ";
                                break;
                            default:
                                CommandText += Value + ", ";
                                break;
                        }
                        
                        CommandText += "'" + Value_Unit + "', ";
                        CommandText += "'" + Value_TimeStamp + "', ";
                        CommandText += "'" + Rec_TimeStamp + "')";

                        System.Console.WriteLine("gary debug: SQL" + CommandText);

                        try
                        {
                            SqlCommand command = new SqlCommand(CommandText, Database_con);
                            command.ExecuteNonQuery();

                            command.Dispose();
                        }
                        catch (Exception Ex)
                        {
                            throw Ex;
                        }

                    }

                    

                }
        }

        public void ReadItems()
        {
            m_ItemAccessMutex.WaitOne();

            try
            {
                int l_Index = 0;
                OPCItemState[] l_ItemStateArray;
                List<int> l_ServerHandleList = m_ServerModel.GetServerHandleList();

                m_Group.Read(OPCDataSource.OPC_DS_DEVICE, l_ServerHandleList.ToArray(), out l_ItemStateArray);

                foreach (int l_ServerHandle in l_ServerHandleList)
                {
                    if (l_ItemStateArray[l_Index].Error == HResults.S_OK)
                    {
                        int l_ClientHandle = m_ServerModel.GetClientHandleFromServerHandle(l_ServerHandle);
                        Dictionary<string, string> l_ItemProps = new Dictionary<string, string>();
                        IOPCItem l_Item = m_ServerModel.GetItemFromClientHandle(l_ClientHandle);

                        l_ItemProps.Add(ServerModel.ITEM_PROP_VALUE_KEY, OPCUtility.ValueToString(l_Item.Type, l_ItemStateArray[l_Index].DataValue));
                        l_ItemProps.Add(ServerModel.ITEM_PROP_QUALITY_KEY, OPCUtility.QualityToString(l_ItemStateArray[l_Index].Quality));
                        l_ItemProps.Add(ServerModel.ITEM_PROP_TIMESTAMP_KEY, OPCUtility.TimeStampToString(l_ItemStateArray[l_Index].Timestamp));

                        m_ServerModel.UpdateItem(l_ClientHandle, l_ItemProps);

                        if (this.VeriteqOPCServer)
                        {
                            WriteToDatabase(l_Item);
                        }
                    }

                    l_Index++;
                }
            }
            catch (Exception l_Ex)
            {
                Disconnect();

                throw l_Ex;
            }
            finally
            {
                m_ItemAccessMutex.ReleaseMutex();
            }
        }

        public void ActivateItems()
        {
            m_ItemAccessMutex.WaitOne();

            try
            {
                int[] l_SetActiveResult;
                m_Group.SetActiveState(m_ServerModel.GetServerHandleList().ToArray(), true, out l_SetActiveResult);
            }
            catch (Exception l_Ex)
            {
                Disconnect();

                throw l_Ex;
            }
            finally
            {
                m_ItemAccessMutex.ReleaseMutex();
            }
        }

        public void WriteItems(List<string> p_ItemsId, string p_Value)
        {
            m_ItemAccessMutex.WaitOne();

            try
            {
                int l_ItemsToWriteCount = p_ItemsId.Count;

                int[] l_ServerHandleArray = new int[l_ItemsToWriteCount];
                object[] l_ValueArray = new object[l_ItemsToWriteCount];
                int[] l_ErrorArray;
                
                for (int i = 0; i < l_ItemsToWriteCount; ++i)
                {
                    l_ServerHandleArray[i] = m_ServerModel.GetServerHandleFromItemId(p_ItemsId[i]);

                    int l_ClientHandle = m_ServerModel.GetClientHandleFromServerHandle(l_ServerHandleArray[i]);
                    IOPCItem l_Item = m_ServerModel.GetItemFromClientHandle(l_ClientHandle);
                    l_ValueArray[i] = OPCUtility.StringToValue(l_Item.Type, p_Value);
                }
                m_Group.Write(l_ServerHandleArray, l_ValueArray, out l_ErrorArray);

                // TODO Check the error
            }
            catch (Exception l_Ex)
            {
                throw l_Ex;
            }
            finally
            {
                m_ItemAccessMutex.ReleaseMutex();
            }
        }

        private void Server_ShutdownRequest(object p_Sender, ShutdownRequestEventArgs p_EvArgs)
        {
            Disconnect();

            // TODO Display message and close server tab
        }

        private void Group_DataChange(object p_Sender, DataChangeEventArgs p_EvArgs)
        {
            m_ItemAccessMutex.WaitOne();

            for (int i = 0; i < p_EvArgs.Status.Length; ++i)
            {
                try
                {
                    Dictionary<string, string> l_ItemProps = new Dictionary<string, string>();
                    IOPCItem l_Item = m_ServerModel.GetItemFromClientHandle(p_EvArgs.Status[i].HandleClient);

                    l_ItemProps.Add(ServerModel.ITEM_PROP_VALUE_KEY, OPCUtility.ValueToString(l_Item.Type, p_EvArgs.Status[i].DataValue));
                    l_ItemProps.Add(ServerModel.ITEM_PROP_QUALITY_KEY, OPCUtility.QualityToString(p_EvArgs.Status[i].Quality));
                    l_ItemProps.Add(ServerModel.ITEM_PROP_TIMESTAMP_KEY, OPCUtility.TimeStampToString(p_EvArgs.Status[i].Timestamp));

                    m_ServerModel.UpdateItem(p_EvArgs.Status[i].HandleClient, l_ItemProps);

                    if (this.VeriteqOPCServer)
                    {
                        WriteToDatabase(l_Item);
                    }

                }
                catch (Exception l_Ex)
                {
                    ErrorLog l_ErrorLog = ErrorLog.GetInstance();
                    l_ErrorLog.WriteToErrorLog(l_Ex.Message, l_Ex.StackTrace, "Error during OPC group update");
                }
            }

            m_ItemAccessMutex.ReleaseMutex();
        }

        private void RecursiveServerHierarchialBrowse(ref List<string> p_ItemIds)
        {
            ArrayList l_ItemList;
            m_Server.Browse(OPCBrowseType.OPC_LEAF, out l_ItemList);
            foreach (string l_ItemName in l_ItemList)
            {
                p_ItemIds.Add(m_Server.GetItemID(l_ItemName));
            }

            ArrayList l_BranchList;
            m_Server.Browse(OPCBrowseType.OPC_BRANCH, out l_BranchList);
            foreach (string l_BranchName in l_BranchList)
            {
                m_Server.ChangeBrowsePosition(OPCBrowseDirection.OPC_BROWSE_DOWN, l_BranchName);
                RecursiveServerHierarchialBrowse(ref p_ItemIds);
                m_Server.ChangeBrowsePosition(OPCBrowseDirection.OPC_BROWSE_UP, "");
            }
        }

        private void RecursiveServerFlatBrowse(ref List<string> p_ItemIds)
        {
            // TODO
        }

        private void ValidateItemIds(ref List<string> p_ItemIds)
        {
            List<string> l_ItemIds = new List<string>(p_ItemIds);
            
            foreach (string l_ItemName in l_ItemIds)
            {
                OPCItemResult[] l_ItemResult;
                OPCItemDef[] l_ItemDef = new OPCItemDef[1];

                l_ItemDef[0] = new OPCItemDef(l_ItemName, true, 0, VarEnum.VT_BSTR);

                if (!m_Group.ValidateItems(l_ItemDef, false, out l_ItemResult))
                {
                    p_ItemIds.Remove(l_ItemName);
                }
            }
        }

        private void AddItems(List<string> p_ItemIds)
        {
            int l_ItemClientHandle = 0;
            OPCItemDef[] l_ItemDef = new OPCItemDef[p_ItemIds.Count];
            OPCItemResult[] l_AddRes;

            foreach (string l_ItemName in p_ItemIds)
            {
                l_ItemDef[l_ItemClientHandle] = new OPCItemDef(l_ItemName, false, l_ItemClientHandle, VarEnum.VT_BSTR);
                l_ItemClientHandle++;
            }

            m_Group.AddItems(l_ItemDef, out l_AddRes);

            l_ItemClientHandle = 0;
            foreach (string l_ItemName in p_ItemIds)
            {
                if (l_AddRes[l_ItemClientHandle].Error == HResults.S_OK)
                {
                    Dictionary<string, string> l_ItemProps = new Dictionary<string, string>();
                    l_ItemProps.Add(ServerModel.ITEM_PROP_ID_KEY, m_Server.GetItemID(l_ItemName));
                    l_ItemProps.Add(ServerModel.ITEM_PROP_TYPE_KEY, OPCUtility.TypeToString((int)l_AddRes[l_ItemClientHandle].CanonicalDataType));

                    m_ServerModel.AddItem(l_ItemClientHandle, l_AddRes[l_ItemClientHandle].HandleServer, l_ItemProps);
                }

                l_ItemClientHandle++;
            }
        }
    }
}
