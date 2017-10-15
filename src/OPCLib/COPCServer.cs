using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using COPC.Common;
using COPC.Datas;
using COPC.Groups;

namespace COPC.Server
{
    public struct OPCProperty
    {
        public int PropertyID;
        public string Description;
        public VarEnum DataType;

        public override string ToString()
        {
            return "ID:" + PropertyID + " '" + Description + "' T:" + DummyVariant.VarEnumToString(DataType);
        }
    }

    public struct OPCPropertyData
    {
        public int PropertyID;
        public int Error;
        public object Data;

        public override string ToString()
        {
            if (Error == HResults.S_OK)
            {
                return "ID:" + PropertyID + " Data:" + Data.ToString();
            }
            else
            {
                return "ID:" + PropertyID + " Error:" + Error.ToString();
            }
        }
    }

    public struct OPCPropertyItem
    {
        public int PropertyID;
        public int Error;
        public string NewItemID;

        public override string ToString()
        {
            if (Error == HResults.S_OK)
            {
                return "ID:" + PropertyID + " newID:" + NewItemID;
            }
            else
            {
                return "ID:" + PropertyID + " Error:" + Error.ToString();
            }
        }
    }

    public class ShutdownRequestEventArgs : EventArgs
    {
        private string m_ShutdownReason;

        public ShutdownRequestEventArgs(string p_ShutdownReason)
        {
            m_ShutdownReason = p_ShutdownReason;
        }

        public string ShutdownReason
        {
            get
            {
                return m_ShutdownReason;
            }
        }
    }

    public delegate void ShutdownRequestEventHandler(object p_Sender, ShutdownRequestEventArgs p_Event);

    [ComVisible(true)]
    public class OPCServer : IOPCShutdown
    {
        private object m_OPCServerObj = null;
        private IOPCServer m_IfServer = null;
        private IOPCCommon m_IfCommon = null;
        private IOPCBrowseServerAddressSpace m_IfBrowse = null;
        private IOPCItemProperties m_IfItmProps = null;

        private IConnectionPointContainer m_PointContainer = null;
        private IConnectionPoint m_ShutdownPoint = null;
        private int m_ShutdownCookie = 0;

        public event ShutdownRequestEventHandler ShutdownRequested = null;

        public OPCServer()
        {
        }

        ~OPCServer()
        {
            Disconnect();
        }

        public void LocalhostConnect(string p_ClsIDOPCServer)
        {
            Disconnect();

            Type l_TypeOfOPCServer = Type.GetTypeFromProgID(p_ClsIDOPCServer);
            if (l_TypeOfOPCServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.OPC_E_NOTFOUND);
            }

            m_OPCServerObj = Activator.CreateInstance(l_TypeOfOPCServer);
            m_IfServer = (IOPCServer) m_OPCServerObj;
            if (m_IfServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.CONNECT_E_NOCONNECTION);
            }

            // Connect all interfaces
            m_IfCommon = (IOPCCommon) m_OPCServerObj;
            m_IfBrowse = (IOPCBrowseServerAddressSpace) m_IfServer;
            m_IfItmProps = (IOPCItemProperties) m_IfServer;
            m_PointContainer = (IConnectionPointContainer) m_OPCServerObj;
            AdviseIOPCShutdown();
        }

        public void RemoteConnect(string p_ClsIDOPCServer, string p_MachineName)
        {
            Disconnect();

            Type l_TypeOfOPCServer = Type.GetTypeFromProgID(p_ClsIDOPCServer, p_MachineName);
            if (l_TypeOfOPCServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.OPC_E_NOTFOUND);
            }

            m_OPCServerObj = Activator.CreateInstance(l_TypeOfOPCServer);
            m_IfServer = (IOPCServer) m_OPCServerObj;
            if (m_IfServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.CONNECT_E_NOCONNECTION);
            }

            // Connect all interfaces
            m_IfCommon = (IOPCCommon) m_OPCServerObj;
            m_IfBrowse = (IOPCBrowseServerAddressSpace) m_IfServer;
            m_IfItmProps = (IOPCItemProperties) m_IfServer;
            m_PointContainer = (IConnectionPointContainer) m_OPCServerObj;
            AdviseIOPCShutdown();
        }

        public void Disconnect()
        {
            if (m_ShutdownPoint != null)
            {
                if (m_ShutdownCookie != 0)
                {
                    m_ShutdownPoint.Unadvise(m_ShutdownCookie);
                    m_ShutdownCookie = 0;
                }
                Marshal.ReleaseComObject(m_ShutdownPoint);
                m_ShutdownPoint = null;
            }

            m_PointContainer = null;
            m_IfBrowse = null;
            m_IfItmProps = null;
            m_IfCommon = null;
            m_IfServer = null;

            if (m_OPCServerObj != null)
            {
                Marshal.ReleaseComObject(m_OPCServerObj);
                m_OPCServerObj = null;
            }
        }

        public void GetStatus(out ServerStatus p_ServerStatus)
        {
                m_IfServer.GetStatus(out p_ServerStatus);
        }

        public bool GetIfStatus()
        {
            if (m_IfServer != null)
                return true;
            else
                return false;
        }

        public string GetErrorString(int p_ErrorCode, int p_LocaleID)
        {
            string l_ErrorRes;

            m_IfServer.GetErrorString(p_ErrorCode, p_LocaleID, out l_ErrorRes);

            return l_ErrorRes;
        }

        public OPCGroup AddGroup(string p_GroupName, bool p_SetActive, int p_RequestedUpdateRate)
        {
            return AddGroup(p_GroupName, p_SetActive, p_RequestedUpdateRate, null, null, 0);
        }

        public OPCGroup AddGroup(string p_GroupName, bool p_SetActive, int p_RequestedUpdateRate,
            int[] p_BiasTime, float[] p_PercentDeadband, int p_LocaleID)
        {
            if (m_IfServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            OPCGroup l_Grp = new OPCGroup(ref m_IfServer, false, p_GroupName, p_SetActive, p_RequestedUpdateRate);
            l_Grp.InternalAdd(p_BiasTime, p_PercentDeadband, p_LocaleID);

            return l_Grp;
        }

        public OPCGroup GetPublicGroup(string p_GroupName)
        {
            if (m_IfServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            OPCGroup l_Grp = new OPCGroup(ref m_IfServer, true, p_GroupName, false, 1000);
            l_Grp.InternalAdd(null, null, 0);

            return l_Grp;
        }

        public void SetLocaleID(int p_LocalID)
        {
            m_IfCommon.SetLocaleID(p_LocalID);
        }

        public void GetLocaleID(out int p_LocalID)
        {
            m_IfCommon.GetLocaleID(out p_LocalID);
        }

        public void QueryAvailableLocaleIDs(out int[] p_LocalIDs)
        {
            p_LocalIDs = null;
            int l_Count;
            IntPtr l_PtrIDs;
            int l_HResult = m_IfCommon.QueryAvailableLocaleIDs(out l_Count, out l_PtrIDs);

            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }
            if (((int) l_PtrIDs) == 0)
            {
                return;
            }
            if (l_Count < 1)
            {
                Marshal.FreeCoTaskMem(l_PtrIDs);
                return;
            }

            p_LocalIDs = new int[l_Count];
            Marshal.Copy(l_PtrIDs, p_LocalIDs, 0, l_Count);
            Marshal.FreeCoTaskMem(l_PtrIDs);
        }

        public void SetClientName(string p_Name)
        {
            m_IfCommon.SetClientName(p_Name);
        }

        public OPCNamespaceType QueryOrganization()
        {
            OPCNamespaceType l_NamespaceType;

            m_IfBrowse.QueryOrganization(out l_NamespaceType);

            return l_NamespaceType;
        }

        public void ChangeBrowsePosition(OPCBrowseDirection p_Direction, string p_Name)
        {
            m_IfBrowse.ChangeBrowsePosition(p_Direction, p_Name);
        }

        public void BrowseOPCItemIDs(OPCBrowseType p_FilterType, string p_FilterCriteria,
            VarEnum p_DataTypeFilter, OPCAccessRights p_AccessRightsFilter, out UCOMIEnumString p_StringEnumerator)
        {
            object l_EnumTemp;

            m_IfBrowse.BrowseOPCItemIDs(p_FilterType, p_FilterCriteria, (short) p_DataTypeFilter, p_AccessRightsFilter, out l_EnumTemp);
            p_StringEnumerator = (UCOMIEnumString) l_EnumTemp;
        }

        public string GetItemID(string p_ItemDataID)
        {
            string l_ItemID;

            m_IfBrowse.GetItemID(p_ItemDataID, out l_ItemID);

            return l_ItemID;
        }

        public void BrowseAccessPaths(string p_ItemID, out UCOMIEnumString p_StringEnumerator)
        {
            object l_EnumTemp;

            m_IfBrowse.BrowseAccessPaths(p_ItemID, out l_EnumTemp);
            p_StringEnumerator = (UCOMIEnumString) l_EnumTemp;
        }

        public void Browse(OPCBrowseType p_Type, out ArrayList p_Lst)
        {
            p_Lst = null;
            UCOMIEnumString l_Enumerator;

            BrowseOPCItemIDs(p_Type, "", VarEnum.VT_EMPTY, 0, out l_Enumerator);
            if (l_Enumerator == null)
            {
                return;
            }

            p_Lst = new ArrayList(500);
            int l_Cft;
            string[] l_StrF = new string[100];
            int l_HResult;
            do
            {
                l_Cft = 0;
                l_HResult = l_Enumerator.Next(100, l_StrF, out l_Cft);
                if (l_Cft > 0)
                {
                    for (int i = 0; i < l_Cft; ++i)
                    {
                        p_Lst.Add(l_StrF[i]);
                    }
                }
            }
            while (l_HResult == HResults.S_OK);

            Marshal.ReleaseComObject(l_Enumerator);
            l_Enumerator = null;
            p_Lst.TrimToSize();
        }

        public void QueryAvailableProperties(string p_ItemID, out OPCProperty[] p_OPCProperties)
        {
            p_OPCProperties = null;
            int l_Count = 0;
            IntPtr l_PtrID;
            IntPtr l_PtrDesc;
            IntPtr l_PtrTyp;

            m_IfItmProps.QueryAvailableProperties(p_ItemID, out l_Count, out l_PtrID, out l_PtrDesc, out l_PtrTyp);
            if ((l_Count == 0) || (l_Count > 10000))
            {
                return;
            }

            int l_RunID = (int) l_PtrID;
            int l_RunDesc = (int) l_PtrDesc;
            int l_RunTyp = (int) l_PtrTyp;
            if ((l_RunID == 0) || (l_RunDesc == 0) || (l_RunTyp == 0))
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            p_OPCProperties = new OPCProperty[l_Count];

            IntPtr l_PtrString;
            for (int i = 0; i < l_Count; ++i)
            {
                p_OPCProperties[i] = new OPCProperty();

                p_OPCProperties[i].PropertyID = Marshal.ReadInt32((IntPtr) l_RunID);
                l_RunID += 4;

                l_PtrString = (IntPtr) Marshal.ReadInt32((IntPtr) l_RunDesc);
                l_RunDesc += 4;
                p_OPCProperties[i].Description = Marshal.PtrToStringUni(l_PtrString);
                Marshal.FreeCoTaskMem(l_PtrString);

                p_OPCProperties[i].DataType = (VarEnum) Marshal.ReadInt16((IntPtr) l_RunTyp);
                l_RunTyp += 2;
            }

            Marshal.FreeCoTaskMem(l_PtrID);
            Marshal.FreeCoTaskMem(l_PtrDesc);
            Marshal.FreeCoTaskMem(l_PtrTyp);
        }

        public bool GetItemProperties(string p_ItemID, int[] p_PropertyIDs, out OPCPropertyData[] p_PropertiesData)
        {
            p_PropertiesData = null;
            int l_Count = p_PropertyIDs.Length;
            if (l_Count < 1)
            {
                return false;
            }

            IntPtr l_PtrDat;
            IntPtr l_PtrErr;
            int l_HResult = m_IfItmProps.GetItemProperties(p_ItemID, l_Count, p_PropertyIDs, out l_PtrDat, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            int l_RunDat = (int) l_PtrDat;
            int l_RunErr = (int) l_PtrErr;
            if ((l_RunDat == 0) || (l_RunErr == 0))
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            p_PropertiesData = new OPCPropertyData[l_Count];

            for (int i = 0; i < l_Count; ++i)
            {
                p_PropertiesData[i] = new OPCPropertyData();
                p_PropertiesData[i].PropertyID = p_PropertyIDs[i];

                p_PropertiesData[i].Error = Marshal.ReadInt32((IntPtr) l_RunErr);
                l_RunErr += 4;

                if (p_PropertiesData[i].Error == HResults.S_OK)
                {
                    p_PropertiesData[i].Data = Marshal.GetObjectForNativeVariant((IntPtr) l_RunDat);
                    DummyVariant.VariantClear((IntPtr) l_RunDat);
                }
                else
                {
                    p_PropertiesData[i].Data = null;
                }

                l_RunDat += DummyVariant.CONST_SIZE;
            }

            Marshal.FreeCoTaskMem(l_PtrDat);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public bool LookupItemIDs(string p_ItemID, int[] p_PropertyIDs, out OPCPropertyItem[] p_PropertyItems)
        {
            p_PropertyItems = null;
            int l_Count = p_PropertyIDs.Length;
            if (l_Count < 1)
            {
                return false;
            }

            IntPtr l_PtrErr;
            IntPtr l_PtrIDs;
            int l_HResult = m_IfItmProps.LookupItemIDs(p_ItemID, l_Count, p_PropertyIDs, out l_PtrIDs, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            int l_RunIDs = (int) l_PtrIDs;
            int l_RunErr = (int) l_PtrErr;
            if ((l_RunIDs == 0) || (l_RunErr == 0))
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            p_PropertyItems = new OPCPropertyItem[l_Count];

            IntPtr l_PtrString;
            for (int i = 0; i < l_Count; ++i)
            {
                p_PropertyItems[i] = new OPCPropertyItem();
                p_PropertyItems[i].PropertyID = p_PropertyIDs[i];

                p_PropertyItems[i].Error = Marshal.ReadInt32((IntPtr) l_RunErr);
                l_RunErr += 4;

                if (p_PropertyItems[i].Error == HResults.S_OK)
                {
                    l_PtrString = (IntPtr) Marshal.ReadInt32((IntPtr) l_RunIDs);
                    p_PropertyItems[i].NewItemID = Marshal.PtrToStringUni(l_PtrString);
                    Marshal.FreeCoTaskMem(l_PtrString);
                }
                else
                {
                    p_PropertyItems[i].NewItemID = null;
                }

                l_RunIDs += 4;
            }

            Marshal.FreeCoTaskMem(l_PtrIDs);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        void IOPCShutdown.ShutdownRequest(string p_ShutdownReason)
        {
            ShutdownRequestEventArgs l_Event = new ShutdownRequestEventArgs(p_ShutdownReason);
            if (ShutdownRequested != null)
            {
                ShutdownRequested(this, l_Event);
            }
        }

        private void AdviseIOPCShutdown()
        {
            Type l_SinkType = typeof(IOPCShutdown);
            Guid l_SinkGuid = l_SinkType.GUID;

            m_PointContainer.FindConnectionPoint(ref l_SinkGuid, out m_ShutdownPoint);
            if (m_ShutdownPoint == null)
            {
                return;
            }

            m_ShutdownPoint.Advise(this, out m_ShutdownCookie);
        }
    }
}
