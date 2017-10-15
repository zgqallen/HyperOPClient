using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

using COPC.Common;
using COPC.Datas;

namespace COPC.Groups
{
    public struct OPCItemDef
    {
        public string AccessPath;
        public string ItemID;
        public bool Active;
        public int HandleClient;
        public VarEnum RequestedDataType;
        public byte[] Blob;

        public OPCItemDef(string p_ID, bool p_Activ, int p_HClt, VarEnum p_Vt)
        {
            AccessPath = "";
            ItemID = p_ID;
            Active = p_Activ;
            HandleClient = p_HClt;
            RequestedDataType = p_Vt;
            Blob = null;
        }
    };

    public struct OPCItemResult
    {
        // Content below only valid if m_Error = S_OK
        public int Error;
        public int HandleServer;
        public VarEnum CanonicalDataType;
        public OPCAccessRights AccessRights;
        public byte[] Blob;
    }

    public struct OPCItemState
    {
        // Content below only valid if m_Error = S_OK
        public int Error;
        public int HandleClient;
        public object DataValue;
        public long Timestamp;
        public short Quality;

        public override string ToString()
        {
            StringBuilder l_Sb = new StringBuilder("OPCIST: ", 256);

            l_Sb.AppendFormat("error=0x{0:x} hclt=0x{1:x}", Error, HandleClient);
            if (Error == HResults.S_OK)
            {
                l_Sb.AppendFormat(" val={0} time={1} qual=", DataValue, Timestamp);
                l_Sb.Append(OPCGroup.QualityToString(Quality));
            }

            return l_Sb.ToString();
        }
    }

    public struct OPCWriteResult
    {
        public int Error;
        public int HandleClient;
    }

    public struct OPCItemAttributes
    {
        public string AccessPath;
        public string ItemID;
        public bool Active;
        public int HandleClient;
        public int HandleServer;
        public OPCAccessRights AccessRights;
        public VarEnum RequestedDataType;
        public VarEnum CanonicalDataType;
        public OPCEuType EUType;
        public object EUInfo;
        public byte[] Blob;

        public override string ToString()
        {
            StringBuilder l_Sb = new StringBuilder("OPCIAT: '", 512);

            l_Sb.Append(ItemID); l_Sb.Append("' ('"); l_Sb.Append(AccessPath);
            l_Sb.AppendFormat("') hc=0x{0:x} hs=0x{1:x} act={2}", HandleClient, HandleServer, Active);
            l_Sb.AppendFormat("\r\n\tacc={0} typr={1} typc={2}", AccessRights, RequestedDataType, CanonicalDataType);
            l_Sb.AppendFormat("\r\n\teut={0} eui={1}", EUType, EUInfo);
            if (Blob != null)
            {
                l_Sb.AppendFormat(" blob size={0}", Blob.Length);
            }

            return l_Sb.ToString();
        }
    }

    public struct OPCGroupState
    {
        public string Name;
        public bool Public;
        public int UpdateRate;
        public bool Active;
        public int TimeBias;
        public float PercentDeadband;
        public int LocaleID;
        public int HandleClient;
        public int HandleServer;
    }

    public class DataChangeEventArgs : EventArgs
    {
        private int m_TransactionID;
        private int m_GroupHandleClient;
        private int m_MasterQuality;
        private int m_MasterError;

        private OPCItemState[] m_Sts;

        public DataChangeEventArgs(int p_TransactionID, int p_GroupHandleClient, int p_MasterQuality,
            int p_MasterError, OPCItemState[] p_Sts)
        {
            m_TransactionID = p_TransactionID;
            m_GroupHandleClient = p_GroupHandleClient;
            m_MasterQuality = p_MasterQuality;
            m_MasterError = p_MasterError;
            m_Sts = p_Sts;
        }

        public int TransactionID
        {
            get
            {
                return m_TransactionID;
            }
        }

        public int GroupHandleClient
        {
            get
            {
                return m_GroupHandleClient;
            }
        }

        public int MasterQuality
        {
            get
            {
                return m_MasterQuality;
            }
        }

        public int MasterError
        {
            get
            {
                return m_MasterError;
            }
        }

        public OPCItemState[] Status
        {
            get
            {
                return m_Sts;
            }
        }
    }

    public delegate void DataChangeEventHandler(object p_Sender, DataChangeEventArgs p_Event);

    public class ReadCompleteEventArgs : EventArgs
    {
        private int m_TransactionID;
        private int m_GroupHandleClient;
        private int m_MasterQuality;
        private int m_MasterError;

        private OPCItemState[] m_Sts;

        public ReadCompleteEventArgs(int p_TransactionID, int p_GroupHandleClient, int p_MasterQuality,
            int p_MasterError, OPCItemState[] p_Sts)
        {
            m_TransactionID = p_TransactionID;
            m_GroupHandleClient = p_GroupHandleClient;
            m_MasterQuality = p_MasterQuality;
            m_MasterError = p_MasterError;
            m_Sts = p_Sts;
        }

        public int TransactionID
        {
            get
            {
                return m_TransactionID;
            }
        }

        public int GroupHandleClient
        {
            get
            {
                return m_GroupHandleClient;
            }
        }

        public int MasterQuality
        {
            get
            {
                return m_MasterQuality;
            }
        }

        public int MasterError
        {
            get
            {
                return m_MasterError;
            }
        }

        public OPCItemState[] Status
        {
            get
            {
                return m_Sts;
            }
        }
    }

    public delegate void ReadCompleteEventHandler(object p_Sender, ReadCompleteEventArgs p_Event);

    public class WriteCompleteEventArgs : EventArgs
    {
        private int m_TransactionID;
        private int m_GroupHandleClient;
        private int m_MasterError;

        private OPCWriteResult[] m_Res;

        public WriteCompleteEventArgs(int p_TransactionID, int p_GroupHandleClient, int p_MasterError,
            OPCWriteResult[] p_Res)
        {
            m_TransactionID = p_TransactionID;
            m_GroupHandleClient = p_GroupHandleClient;
            m_MasterError = p_MasterError;
            m_Res = p_Res;
        }

        public int TransactionID
        {
            get
            {
                return m_TransactionID;
            }
        }

        public int GroupHandleClient
        {
            get
            {
                return m_GroupHandleClient;
            }
        }

        public int MasterError
        {
            get
            {
                return m_MasterError;
            }
        }

        public OPCWriteResult[] Results
        {
            get
            {
                return m_Res;
            }
        }
    }

    public delegate void WriteCompleteEventHandler(object p_Sender, WriteCompleteEventArgs p_Event);

    public class CancelCompleteEventArgs : EventArgs
    {
        private int m_TransactionID;
        private int m_GroupHandleClient;

        public CancelCompleteEventArgs(int p_TransactionID, int p_GoupHandleClient)
        {
            m_TransactionID = p_TransactionID;
            m_GroupHandleClient = p_GoupHandleClient;
        }

        public int TransactionID
        {
            get
            {
                return m_TransactionID;
            }
        }

        public int GroupHandleClient
        {
            get
            {
                return m_GroupHandleClient;
            }
        }
    }

    public delegate void CancelCompleteEventHandler(object p_Sender, CancelCompleteEventArgs p_Event);

    public class OPCGroup : IOPCDataCallback
    {
        private OPCGroupState m_State;

        private IOPCServer m_IfServer = null;
        private IOPCGroupStateMgt m_IfMgt = null;
        private IOPCItemMgt m_IfItems = null;
        private IOPCSyncIO m_IfSync = null;
        private IOPCAsyncIO2 m_IfAsync = null;

        private System.Runtime.InteropServices.ComTypes.IConnectionPointContainer m_PointContainer = null;
        private System.Runtime.InteropServices.ComTypes.IConnectionPoint m_CallbackPoint = null;
        private int m_CallbackCookie = 0;

        // Marshaling helpers
        private readonly Type m_TypeOPCItemDef;
        private readonly int m_SizeOPCItemDef;
        private readonly Type m_TypeOPCItemResult;
        private readonly int m_SizeOPCItemResult;

        public event DataChangeEventHandler DataChanged;
        public event ReadCompleteEventHandler ReadCompleted;
        public event WriteCompleteEventHandler WriteCompleted;
        public event CancelCompleteEventHandler CancelCompleted;

        internal OPCGroup(ref IOPCServer p_IfServerLink, bool p_IsPublic, string p_GroupName, bool p_SetActive,
            int p_RequestedUpdateRate)
        {
            m_IfServer = p_IfServerLink;

            m_State.Name = p_GroupName;
            m_State.Public = p_IsPublic;
            m_State.UpdateRate = p_RequestedUpdateRate;
            m_State.Active = p_SetActive;
            m_State.TimeBias = 0;
            m_State.PercentDeadband = 0.0f;
            m_State.LocaleID = 0;
            m_State.HandleClient = GetHashCode();
            m_State.HandleServer = 0;

            // Marshaling helpers
            m_TypeOPCItemDef = typeof(OPCItemDefIntern);
            m_SizeOPCItemDef = Marshal.SizeOf(m_TypeOPCItemDef);
            m_TypeOPCItemResult = typeof(OPCItemResultIntern);
            m_SizeOPCItemResult = Marshal.SizeOf(m_TypeOPCItemResult);
        }

        ~OPCGroup()
        {
            Remove(false);
        }

        public string Name
        {
            get
            {
                return m_State.Name;
            }
            set
            {
                SetName(value);
            }
        }

        public bool Active
        {
            get
            {
                return m_State.Active;
            }
            set
            {
                m_IfMgt.SetState(null, out m_State.UpdateRate, new bool[1] { value }, null, null, null, null);
                m_State.Active = value;
            }
        }

        public bool Public
        {
            get
            {
                return m_State.Public;
            }
        }

        public int UpdateRate
        {
            get
            {
                return m_State.UpdateRate;
            }
            set
            {
                m_IfMgt.SetState(new int[1] { value }, out m_State.UpdateRate, null, null, null, null, null);
            }
        }

        public int TimeBias
        {
            get
            {
                return m_State.TimeBias;
            }
            set
            {
                m_IfMgt.SetState(null, out m_State.UpdateRate, null, new int[1] { value }, null, null, null);
                m_State.TimeBias = value;
            }
        }

        public float PercentDeadband
        {
            get
            {
                return m_State.PercentDeadband;
            }
            set
            {
                m_IfMgt.SetState(null, out m_State.UpdateRate, null, null, new float[1] { value }, null, null);
                m_State.PercentDeadband = value;
            }
        }

        public int LocaleID
        {
            get
            {
                return m_State.LocaleID;
            }
            set
            {
                m_IfMgt.SetState(null, out m_State.UpdateRate, null, null, null, new int[1] { value }, null);
                m_State.LocaleID = value;
            }
        }

        public int HandleClient
        {
            get
            {
                return m_State.HandleClient;
            }
            set
            {
                m_IfMgt.SetState(null, out m_State.UpdateRate, null, null, null, null, new int[1] { value });
                m_State.HandleClient = value;
            }
        }

        public int HandleServer
        {
            get
            {
                return m_State.HandleServer;
            }
        }

        internal void InternalAdd(int[] p_BiasTime, float[] p_PercentDeadband, int p_LocaleID)
        {
            Type l_TypGrpMgt = typeof(IOPCGroupStateMgt);
            Guid l_GuidGrpTst = l_TypGrpMgt.GUID;
            object l_ObjTemp;

            if (m_State.Public)
            {
                IOPCServerPublicGroups l_IfPubGrps = null;
                l_IfPubGrps = (IOPCServerPublicGroups) m_IfServer;
                if (l_IfPubGrps == null)
                {
                    Marshal.ThrowExceptionForHR(HResults.E_NOINTERFACE);
                }

                l_IfPubGrps.GetPublicGroupByName(m_State.Name, ref l_GuidGrpTst, out l_ObjTemp);
                l_IfPubGrps = null;
            }
            else
            {
                m_IfServer.AddGroup(m_State.Name, m_State.Active, m_State.UpdateRate,
                    m_State.HandleClient, p_BiasTime, p_PercentDeadband, m_State.LocaleID,
                    out m_State.HandleServer, out m_State.UpdateRate, ref l_GuidGrpTst, out l_ObjTemp);
            }
            if (l_ObjTemp == null)
            {
                Marshal.ThrowExceptionForHR(HResults.E_NOINTERFACE);
            }
            m_IfMgt = (IOPCGroupStateMgt) l_ObjTemp;
            l_ObjTemp = null;

            GetStates();
            GetInterfaces();
            AdviseIOPCDataCallback();
        }

        public void Remove(bool p_Force)
        {
            if (m_CallbackPoint != null)
            {
                if (m_CallbackCookie != 0)
                {
                    m_CallbackPoint.Unadvise(m_CallbackCookie);
                    m_CallbackCookie = 0;
                }
                Marshal.ReleaseComObject(m_CallbackPoint);
                m_CallbackPoint = null;
            }

            m_PointContainer = null;
            m_IfItems = null;
            m_IfSync = null;
            m_IfAsync = null;

            if (m_IfMgt != null)
            {
                Marshal.ReleaseComObject(m_IfMgt);
                m_IfMgt = null;
            }

            if (m_IfServer != null)
            {
                if (!m_State.Public)
                {
                    m_IfServer.RemoveGroup(m_State.HandleServer, p_Force);
                }
                m_IfServer = null;
            }

            m_State.HandleServer = 0;
        }

        public void DeletePublic(bool p_Force)
        {
            if (!m_State.Public)
            {
                Marshal.ThrowExceptionForHR(HResults.E_FAIL);
            }

            IOPCServerPublicGroups l_IfPubGrps = null;
            l_IfPubGrps = (IOPCServerPublicGroups) m_IfServer;
            if (l_IfPubGrps == null)
            {
                Marshal.ThrowExceptionForHR(HResults.E_NOINTERFACE);
            }
            Remove(false);
            l_IfPubGrps.RemovePublicGroup(m_State.HandleServer, p_Force);
            l_IfPubGrps = null;
        }

        public void MoveToPublic()
        {
            if (m_State.Public)
            {
                Marshal.ThrowExceptionForHR(HResults.E_FAIL);
            }

            IOPCPublicGroupStateMgt l_IfPubMgt = null;
            l_IfPubMgt = (IOPCPublicGroupStateMgt) m_IfMgt;
            if (l_IfPubMgt == null)
            {
                Marshal.ThrowExceptionForHR(HResults.E_NOINTERFACE);
            }
            l_IfPubMgt.MoveToPublic();
            l_IfPubMgt.GetState(out m_State.Public);
            l_IfPubMgt = null;
        }

        public void SetName(string p_NewName)
        {
            m_IfMgt.SetName(p_NewName);
            m_State.Name = p_NewName;
        }

        public void GetStates()
        {
            m_IfMgt.GetState(out m_State.UpdateRate, out m_State.Active, out m_State.Name,
                out m_State.TimeBias, out m_State.PercentDeadband, out m_State.LocaleID,
                out m_State.HandleClient, out m_State.HandleServer);
        }

        public bool AddItems(OPCItemDef[] p_ArrDef, out OPCItemResult[] p_ArrRes)
        {
            p_ArrRes = null;
            bool l_HasBlobs = false;
            int l_Count = p_ArrDef.Length;

            IntPtr l_PtrDef = Marshal.AllocCoTaskMem(l_Count * m_SizeOPCItemDef);
            int l_RunDef = (int) l_PtrDef;
            OPCItemDefIntern l_ItmDefIntrn = new OPCItemDefIntern();
            l_ItmDefIntrn.Reserved = 0;

            for (int i = 0; i < p_ArrDef.Length; ++i)
            {
                OPCItemDef l_ItmDef = p_ArrDef[i];

                l_ItmDefIntrn.AccessPath = l_ItmDef.AccessPath;
                l_ItmDefIntrn.ItemID = l_ItmDef.ItemID;
                l_ItmDefIntrn.Active = l_ItmDef.Active;
                l_ItmDefIntrn.Client = l_ItmDef.HandleClient;
                l_ItmDefIntrn.RequestedDataType = (short) l_ItmDef.RequestedDataType;
                l_ItmDefIntrn.BlobSize = 0;
                l_ItmDefIntrn.Blob = IntPtr.Zero;

                if (l_ItmDef.Blob != null)
                {
                    l_ItmDefIntrn.BlobSize = l_ItmDef.Blob.Length;
                    if (l_ItmDefIntrn.BlobSize > 0)
                    {
                        l_HasBlobs = true;
                        l_ItmDefIntrn.Blob = Marshal.AllocCoTaskMem(l_ItmDefIntrn.BlobSize);
                        Marshal.Copy(l_ItmDef.Blob, 0, l_ItmDefIntrn.Blob, l_ItmDefIntrn.BlobSize);
                    }
                }

                Marshal.StructureToPtr(l_ItmDefIntrn, (IntPtr) l_RunDef, false);
                l_RunDef += m_SizeOPCItemDef;
            }

            IntPtr l_PtrRes;
            IntPtr l_PtrErr;
            int l_HResult = m_IfItems.AddItems(l_Count, l_PtrDef, out l_PtrRes, out l_PtrErr);
            l_RunDef = (int) l_PtrDef;

            if (l_HasBlobs)
            {
                for (int i = 0; i < l_Count; ++i)
                {
                    IntPtr l_Blob = (IntPtr) Marshal.ReadInt32((IntPtr) (l_RunDef + 20));
                    if (l_Blob != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(l_Blob);
                    }
                    Marshal.DestroyStructure((IntPtr) l_RunDef, m_TypeOPCItemDef);
                    l_RunDef += m_SizeOPCItemDef;
                }
            }
            else
            {
                for (int i = 0; i < l_Count; ++i)
                {
                    Marshal.DestroyStructure((IntPtr) l_RunDef, m_TypeOPCItemDef);
                    l_RunDef += m_SizeOPCItemDef;
                }
            }
            Marshal.FreeCoTaskMem(l_PtrDef);

            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            int l_RunRes = (int) l_PtrRes;
            int l_RunErr = (int) l_PtrErr;
            if ((l_RunRes == 0) || (l_RunErr == 0))
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            p_ArrRes = new OPCItemResult[l_Count];
            for (int i = 0; i < l_Count; ++i)
            {
                p_ArrRes[i] = new OPCItemResult();
                p_ArrRes[i].Error = Marshal.ReadInt32((IntPtr) l_RunErr);
                if (HResults.Failed(p_ArrRes[i].Error))
                {
                    continue;
                }

                p_ArrRes[i].HandleServer = Marshal.ReadInt32((IntPtr) l_RunRes);
                p_ArrRes[i].CanonicalDataType = (VarEnum) (int) Marshal.ReadInt16((IntPtr) (l_RunRes + 4));
                p_ArrRes[i].AccessRights = (OPCAccessRights) Marshal.ReadInt32((IntPtr) (l_RunRes + 8));

                int l_PtrBlob = Marshal.ReadInt32((IntPtr) (l_RunRes + 16));
                if (l_PtrBlob != 0)
                {
                    int l_BlobSize = Marshal.ReadInt32((IntPtr) (l_RunRes + 12));
                    if (l_BlobSize > 0)
                    {
                        p_ArrRes[i].Blob = new byte[l_BlobSize];
                        Marshal.Copy((IntPtr) l_PtrBlob, p_ArrRes[i].Blob, 0, l_BlobSize);
                    }
                    Marshal.FreeCoTaskMem((IntPtr) l_PtrBlob);
                }

                l_RunRes += m_SizeOPCItemResult;
                l_RunErr += 4;
            }

            Marshal.FreeCoTaskMem(l_PtrRes);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public bool ValidateItems(OPCItemDef[] p_ArrDef, bool p_BlobUpd, out OPCItemResult[] p_ArrRes)
        {
            p_ArrRes = null;
            bool l_HasBlobs = false;
            int l_Count = p_ArrDef.Length;
            IntPtr l_PtrDef = Marshal.AllocCoTaskMem(l_Count * m_SizeOPCItemDef);
            int l_RunDef = (int) l_PtrDef;
            OPCItemDefIntern l_ItmDefIntrn = new OPCItemDefIntern();
            l_ItmDefIntrn.Reserved = 0;

            for (int i = 0; i < p_ArrDef.Length; ++i)
            {
                OPCItemDef l_ItmDef = p_ArrDef[i];

                l_ItmDefIntrn.AccessPath = l_ItmDef.AccessPath;
                l_ItmDefIntrn.ItemID = l_ItmDef.ItemID;
                l_ItmDefIntrn.Active = l_ItmDef.Active;
                l_ItmDefIntrn.Client = l_ItmDef.HandleClient;
                l_ItmDefIntrn.RequestedDataType = (short) l_ItmDef.RequestedDataType;
                l_ItmDefIntrn.BlobSize = 0;
                l_ItmDefIntrn.Blob = IntPtr.Zero;

                if (l_ItmDef.Blob != null)
                {
                    l_ItmDefIntrn.BlobSize = l_ItmDef.Blob.Length;
                    if (l_ItmDefIntrn.BlobSize > 0)
                    {
                        l_HasBlobs = true;
                        l_ItmDefIntrn.Blob = Marshal.AllocCoTaskMem(l_ItmDefIntrn.BlobSize);
                        Marshal.Copy(l_ItmDef.Blob, 0, l_ItmDefIntrn.Blob, l_ItmDefIntrn.BlobSize);
                    }
                }

                Marshal.StructureToPtr(l_ItmDefIntrn, (IntPtr) l_RunDef, false);
                l_RunDef += m_SizeOPCItemDef;
            }

            IntPtr l_PtrRes;
            IntPtr l_PtrErr;
            int l_HResult = m_IfItems.ValidateItems(l_Count, l_PtrDef, p_BlobUpd, out l_PtrRes, out l_PtrErr);

            l_RunDef = (int) l_PtrDef;
            if (l_HasBlobs)
            {
                for (int i = 0; i < l_Count; ++i)
                {
                    IntPtr l_Blob = (IntPtr) Marshal.ReadInt32((IntPtr) (l_RunDef + 20));
                    if (l_Blob != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(l_Blob);
                    }
                    Marshal.DestroyStructure((IntPtr)l_RunDef, m_TypeOPCItemDef);
                    l_RunDef += m_SizeOPCItemDef;
                }
            }
            else
            {
                for (int i = 0; i < l_Count; ++i)
                {
                    Marshal.DestroyStructure((IntPtr) l_RunDef, m_TypeOPCItemDef);
                    l_RunDef += m_SizeOPCItemDef;
                }
            }
            Marshal.FreeCoTaskMem(l_PtrDef);

            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            int l_RunRes = (int) l_PtrRes;
            int l_RunErr = (int) l_PtrErr;
            if ((l_RunRes == 0) || (l_RunErr == 0))
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            p_ArrRes = new OPCItemResult[l_Count];
            for (int i = 0; i < l_Count; ++i)
            {
                p_ArrRes[i] = new OPCItemResult();
                p_ArrRes[i].Error = Marshal.ReadInt32((IntPtr)l_RunErr);
                if (HResults.Failed(p_ArrRes[i].Error))
                {
                    continue;
                }

                p_ArrRes[i].HandleServer = Marshal.ReadInt32((IntPtr) l_RunRes);
                p_ArrRes[i].CanonicalDataType = (VarEnum) (int) Marshal.ReadInt16((IntPtr) (l_RunRes + 4));
                p_ArrRes[i].AccessRights = (OPCAccessRights) Marshal.ReadInt32((IntPtr) (l_RunRes + 8));

                int l_PtrBlob = Marshal.ReadInt32((IntPtr) (l_RunRes + 16));
                if (l_PtrBlob != 0)
                {
                    int l_BlobSize = Marshal.ReadInt32((IntPtr) (l_RunRes + 12));
                    if (l_BlobSize > 0)
                    {
                        p_ArrRes[i].Blob = new byte[l_BlobSize];
                        Marshal.Copy((IntPtr) l_PtrBlob, p_ArrRes[i].Blob, 0, l_BlobSize);
                    }
                    Marshal.FreeCoTaskMem((IntPtr) l_PtrBlob);
                }

                l_RunRes += m_SizeOPCItemResult;
                l_RunErr += 4;
            }

            Marshal.FreeCoTaskMem(l_PtrRes);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public bool RemoveItems(int[] p_ArrHSrv, out int[] p_ArrErr)
        {
            p_ArrErr = null;
            int l_Count = p_ArrHSrv.Length;
            IntPtr l_PtrErr;

            int l_HResult = m_IfItems.RemoveItems(l_Count, p_ArrHSrv, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            p_ArrErr = new int[l_Count];
            Marshal.Copy(l_PtrErr, p_ArrErr, 0, l_Count);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public bool SetActiveState(int[] p_ArrHSrv, bool p_Activate, out int[] p_ArrErr)
        {
            p_ArrErr = null;
            int l_Count = p_ArrHSrv.Length;
            IntPtr l_PtrErr;

            int l_HResult = m_IfItems.SetActiveState(l_Count, p_ArrHSrv, p_Activate, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            p_ArrErr = new int[l_Count];
            Marshal.Copy(l_PtrErr, p_ArrErr, 0, l_Count);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public bool SetClientHandles(int[] p_ArrHSrv, int[] p_ArrHClt, out int[] p_ArrErr)
        {
            p_ArrErr = null;
            int l_Count = p_ArrHSrv.Length;
            if (l_Count != p_ArrHClt.Length)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            IntPtr l_PtrErr;
            int l_HResult = m_IfItems.SetClientHandles(l_Count, p_ArrHSrv, p_ArrHClt, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            p_ArrErr = new int[l_Count];
            Marshal.Copy(l_PtrErr, p_ArrErr, 0, l_Count);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public bool SetDatatypes(int[] p_ArrHSrv, VarEnum[] p_ArrVT, out int[] p_ArrErr)
        {
            p_ArrErr = null;
            int l_Count = p_ArrHSrv.Length;
            if (l_Count != p_ArrVT.Length)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            IntPtr l_PtrVT = Marshal.AllocCoTaskMem(l_Count * 2);
            int l_RunVT = (int) l_PtrVT;
            for (int i = 0; i < p_ArrVT.Length; ++i)
            {
                VarEnum l_VarEnm = p_ArrVT[i];

                Marshal.WriteInt16((IntPtr) l_RunVT, (short) l_VarEnm);
                l_RunVT += 2;
            }

            IntPtr l_PtrErr;
            int l_HResult = m_IfItems.SetDatatypes(l_Count, p_ArrHSrv, l_PtrVT, out l_PtrErr);
            Marshal.FreeCoTaskMem(l_PtrVT);

            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            p_ArrErr = new int[l_Count];
            Marshal.Copy(l_PtrErr, p_ArrErr, 0, l_Count);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public OPCEnumItemAttributes CreateAttrEnumerator()
        {
            Type l_TypEnuAtt = typeof(IEnumOPCItemAttributes);
            Guid l_GuidEnuAtt = l_TypEnuAtt.GUID;
            object l_ObjTemp;

            int l_HResult = m_IfItems.CreateEnumerator(ref l_GuidEnuAtt, out l_ObjTemp);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }
            if ((l_HResult == HResults.S_FALSE) || (l_ObjTemp == null))
            {
                return null;
            }

            IEnumOPCItemAttributes l_IfEnu = (IEnumOPCItemAttributes) l_ObjTemp;
            l_ObjTemp = null;
            OPCEnumItemAttributes l_Enu = new OPCEnumItemAttributes(l_IfEnu);

            return l_Enu;
        }

        public bool Read(OPCDataSource p_Src, int[] p_ArrHSrv, out OPCItemState[] p_ArrStat)
        {
            p_ArrStat = null;
            int l_Count = p_ArrHSrv.Length;
            IntPtr l_PtrStat;
            IntPtr l_PtrErr;

            int l_HResult = m_IfSync.Read(p_Src, l_Count, p_ArrHSrv, out l_PtrStat, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            int l_RunErr = (int) l_PtrErr;
            int l_RunStat = (int) l_PtrStat;
            if ((l_RunErr == 0) || (l_RunStat == 0))
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            p_ArrStat = new OPCItemState[l_Count];

            for (int i = 0; i < l_Count; ++i)
            {
                p_ArrStat[i] = new OPCItemState();

                p_ArrStat[i].Error = Marshal.ReadInt32((IntPtr) l_RunErr);
                l_RunErr += 4;

                p_ArrStat[i].HandleClient = Marshal.ReadInt32((IntPtr) l_RunStat);

                if (HResults.Succeeded(p_ArrStat[i].Error))
                {
                    short l_VT = Marshal.ReadInt16((IntPtr) (l_RunStat + 16));
                    if (l_VT == (short) VarEnum.VT_ERROR)
                    {
                        p_ArrStat[i].Error = Marshal.ReadInt32((IntPtr) (l_RunStat + 24));
                    }

                    p_ArrStat[i].Timestamp = Marshal.ReadInt64((IntPtr) (l_RunStat + 4));
                    p_ArrStat[i].Quality = Marshal.ReadInt16((IntPtr) (l_RunStat + 12));
                    p_ArrStat[i].DataValue = Marshal.GetObjectForNativeVariant((IntPtr) (l_RunStat + 16));
                    DummyVariant.VariantClear((IntPtr) (l_RunStat + 16));
                }
                else
                {
                    p_ArrStat[i].DataValue = null;
                }

                l_RunStat += 32;
            }

            Marshal.FreeCoTaskMem(l_PtrStat);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public bool Write(int[] p_ArrHSrv, object[] p_ArrVal, out int[] p_ArrErr)
        {
            p_ArrErr = null;
            int l_Count = p_ArrHSrv.Length;
            if (l_Count != p_ArrVal.Length)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            IntPtr l_PtrErr;
            int l_HResult = m_IfSync.Write(l_Count, p_ArrHSrv, p_ArrVal, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            p_ArrErr = new int[l_Count];
            Marshal.Copy(l_PtrErr, p_ArrErr, 0, l_Count);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public bool Read(int[] p_ArrHSrv, int p_TransactionID, out int p_CancelID, out int[] p_ArrErr)
        {
            p_ArrErr = null;
            p_CancelID = 0;
            int l_Count = p_ArrHSrv.Length;

            IntPtr l_PtrErr;
            int l_HResult = m_IfAsync.Read(l_Count, p_ArrHSrv, p_TransactionID, out p_CancelID, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            p_ArrErr = new int[l_Count];
            Marshal.Copy(l_PtrErr, p_ArrErr, 0, l_Count);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public bool Write(int[] p_ArrHSrv, object[] p_ArrVal, int p_TransactionID, out int p_CancelID, out int[] p_ArrErr)
        {
            p_ArrErr = null;
            p_CancelID = 0;
            int l_Count = p_ArrHSrv.Length;
            if (l_Count != p_ArrVal.Length)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            IntPtr l_PtrErr;
            int l_HResult = m_IfAsync.Write(l_Count, p_ArrHSrv, p_ArrVal, p_TransactionID, out p_CancelID, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            p_ArrErr = new int[l_Count];
            Marshal.Copy(l_PtrErr, p_ArrErr, 0, l_Count);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public void Refresh2(OPCDataSource p_SourceMode, int p_TransactionID, out int p_CancelID)
        {
            m_IfAsync.Refresh2(p_SourceMode, p_TransactionID, out p_CancelID);
        }

        public void Cancel2(int p_CancelID)
        {
            m_IfAsync.Cancel2(p_CancelID);
        }

        public void SetEnable(bool p_DoEnable)
        {
            m_IfAsync.SetEnable(p_DoEnable);
        }

        public void GetEnable(out bool p_IsEnabled)
        {
            m_IfAsync.GetEnable(out p_IsEnabled);
        }

        void IOPCDataCallback.OnDataChange(int p_TransID, int p_Group, int p_MasterQuality,
            int p_MasterError, int p_Count, IntPtr p_ClientItems, IntPtr p_Values,
            IntPtr p_Qualities, IntPtr p_TimeStamps, IntPtr p_Errors)
        {
            Trace.WriteLine("OPCGroup.OnDataChange");
            if ((p_Count == 0) || (p_Group != m_State.HandleClient))
            {
                return;
            }

            int l_Count = (int) p_Count;
            int l_RunH = (int) p_ClientItems;
            int l_RunV = (int) p_Values;
            int l_RunQ = (int) p_Qualities;
            int l_RunT = (int) p_TimeStamps;
            int l_RunE = (int) p_Errors;

            OPCItemState[] l_Sts = new OPCItemState[l_Count];

            for (int i = 0; i < l_Count; ++i)
            {
                l_Sts[i] = new OPCItemState();
                l_Sts[i].Error = Marshal.ReadInt32((IntPtr)l_RunE);
                l_RunE += 4;

                l_Sts[i].HandleClient = Marshal.ReadInt32((IntPtr)l_RunH);
                l_RunH += 4;

                if (HResults.Succeeded(l_Sts[i].Error))
                {
                    short l_VT = Marshal.ReadInt16((IntPtr) l_RunV);
                    if (l_VT == (short) VarEnum.VT_ERROR)
                    {
                        l_Sts[i].Error = Marshal.ReadInt32((IntPtr)(l_RunV + 8));
                    }

                    l_Sts[i].DataValue = Marshal.GetObjectForNativeVariant((IntPtr)l_RunV);
                    l_Sts[i].Quality = Marshal.ReadInt16((IntPtr) l_RunQ);
                    l_Sts[i].Timestamp = Marshal.ReadInt64((IntPtr)l_RunT);
                }

                l_RunV += DummyVariant.CONST_SIZE;
                l_RunQ += 2;
                l_RunT += 8;
            }
            DataChangeEventArgs l_Event = new DataChangeEventArgs(p_TransID, p_Group, p_MasterQuality, p_MasterError, l_Sts);

            if (DataChanged != null)
            {
                DataChanged(this, l_Event);
            }
        }

        void IOPCDataCallback.OnReadComplete(int p_TransID, int p_Group, int p_MasterQuality,
            int p_MasterError, int p_Count, IntPtr p_ClientItems, IntPtr p_Values,
            IntPtr p_Qualities, IntPtr p_TimeStamps, IntPtr p_Errors)
        {
            Trace.WriteLine("OPCGroup.OnReadComplete");
            if ((p_Count == 0) || (p_Group != m_State.HandleClient))
            {
                return;
            }

            int l_Count = (int) p_Count;
            int l_RunH = (int) p_ClientItems;
            int l_RunV = (int) p_Values;
            int l_RunQ = (int) p_Qualities;
            int l_RunT = (int) p_TimeStamps;
            int l_RunE = (int) p_Errors;

            OPCItemState[] l_Sts = new OPCItemState[l_Count];

            for (int i = 0; i < l_Count; ++i)
            {
                l_Sts[i] = new OPCItemState();
                l_Sts[i].Error = Marshal.ReadInt32((IntPtr) l_RunE);
                l_RunE += 4;

                l_Sts[i].HandleClient = Marshal.ReadInt32((IntPtr) l_RunH);
                l_RunH += 4;

                if (HResults.Succeeded(l_Sts[i].Error))
                {
                    short l_VT = Marshal.ReadInt16((IntPtr) l_RunV);
                    if (l_VT == (short) VarEnum.VT_ERROR)
                    {
                        l_Sts[i].Error = Marshal.ReadInt32((IntPtr) (l_RunV + 8));
                    }

                    l_Sts[i].DataValue = Marshal.GetObjectForNativeVariant((IntPtr) l_RunV);
                    l_Sts[i].Quality = Marshal.ReadInt16((IntPtr) l_RunQ);
                    l_Sts[i].Timestamp = Marshal.ReadInt64((IntPtr) l_RunT);
                }

                l_RunV += DummyVariant.CONST_SIZE;
                l_RunQ += 2;
                l_RunT += 8;
            }
            ReadCompleteEventArgs l_Event = new ReadCompleteEventArgs(p_TransID, p_Group, p_MasterQuality, p_MasterError, l_Sts);

            if (ReadCompleted != null)
            {
                ReadCompleted(this, l_Event);
            }
        }

        void IOPCDataCallback.OnWriteComplete(int p_TransID, int p_Group, int p_MasterErr,
            int p_Count, IntPtr p_Clienthandles, IntPtr p_Errors)
        {
            Trace.WriteLine("OPCGroup.OnWriteComplete");
            if ((p_Count == 0) || (p_Group != m_State.HandleClient))
            {
                return;
            }

            int l_Count = (int) p_Count;
            int l_RunH = (int) p_Clienthandles;
            int l_RunE = (int) p_Errors;

            OPCWriteResult[] l_Res = new OPCWriteResult[l_Count];

            for (int i = 0; i < l_Count; ++i)
            {
                l_Res[i] = new OPCWriteResult();

                l_Res[i].Error = Marshal.ReadInt32((IntPtr) l_RunE);
                l_RunE += 4;

                l_Res[i].HandleClient = Marshal.ReadInt32((IntPtr) l_RunH);
                l_RunH += 4;
            }
            WriteCompleteEventArgs l_Event = new WriteCompleteEventArgs(p_TransID, p_Group, p_MasterErr, l_Res);

            if (WriteCompleted != null)
            {
                WriteCompleted(this, l_Event);
            }
        }

        void IOPCDataCallback.OnCancelComplete(int p_TransID, int p_Group)
        {
            Trace.WriteLine("OPCGroup.OnCancelComplete");
            if (p_Group != m_State.HandleClient)
            {
                return;
            }

            CancelCompleteEventArgs l_Event = new CancelCompleteEventArgs(p_TransID, p_Group);
            if (CancelCompleted != null)
            {
                CancelCompleted(this, l_Event);
            }
        }

        private void GetInterfaces()
        {
            m_IfItems = (IOPCItemMgt) m_IfMgt;
            m_IfSync = (IOPCSyncIO) m_IfMgt;
            m_IfAsync = (IOPCAsyncIO2) m_IfMgt;

            m_PointContainer = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer) m_IfMgt;
        }

        private void AdviseIOPCDataCallback()
        {
            Type l_SinkType = typeof(IOPCDataCallback);
            Guid l_SinkGuid = l_SinkType.GUID;

            m_PointContainer.FindConnectionPoint(ref l_SinkGuid, out m_CallbackPoint);
            if (m_CallbackPoint == null)
            {
                return;
            }

            m_CallbackPoint.Advise(this, out m_CallbackCookie);
        }

        public static string QualityToString(short p_Quality)
        {
            StringBuilder l_Sb = new StringBuilder(256);

            OPCQualityMaster l_QualMaster = (OPCQualityMaster)(p_Quality & (short)OPCQualityMasks.MASTER_MASK);
            OPCQualityStatus l_QualStatus = (OPCQualityStatus)(p_Quality & (short)OPCQualityMasks.STATUS_MASK);
            OPCQualityLimit l_QualLimit = (OPCQualityLimit)(p_Quality & (short)OPCQualityMasks.LIMIT_MASK);
            l_Sb.AppendFormat("{0}+{1}+{2}", l_QualMaster, l_QualStatus, l_QualLimit);

            return l_Sb.ToString();
        }
    }

    public class OPCEnumItemAttributes
    {
        private IEnumOPCItemAttributes m_IfEnum;

        internal OPCEnumItemAttributes(IEnumOPCItemAttributes p_IfEnum)
        {
            m_IfEnum = p_IfEnum;
        }

        ~OPCEnumItemAttributes()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (m_IfEnum != null)
            {
                Marshal.ReleaseComObject(m_IfEnum);
                m_IfEnum = null;
            }
        }

        public void Next(int l_EnumCountMax, out OPCItemAttributes[] l_Attributes)
        {
            l_Attributes = null;
            IntPtr l_PtrAtt;
            int l_Count;

            m_IfEnum.Next(l_EnumCountMax, out l_PtrAtt, out l_Count);
            int l_RunAtt = (int) l_PtrAtt;
            if ((l_RunAtt == 0) || (l_Count <= 0) || (l_Count > l_EnumCountMax))
            {
                return;
            }

            l_Attributes = new OPCItemAttributes[l_Count];
            IntPtr l_PtrString;

            for (int i = 0; i < l_Count; ++i)
            {
                l_Attributes[i] = new OPCItemAttributes();

                l_PtrString = (IntPtr) Marshal.ReadInt32((IntPtr) l_RunAtt);
                l_Attributes[i].AccessPath = Marshal.PtrToStringUni(l_PtrString);
                Marshal.FreeCoTaskMem(l_PtrString);

                l_PtrString = (IntPtr) Marshal.ReadInt32((IntPtr) (l_RunAtt + 4));
                l_Attributes[i].ItemID = Marshal.PtrToStringUni(l_PtrString);
                Marshal.FreeCoTaskMem(l_PtrString);

                l_Attributes[i].Active = (Marshal.ReadInt32((IntPtr) (l_RunAtt + 8)) != 0);
                l_Attributes[i].HandleClient = Marshal.ReadInt32((IntPtr) (l_RunAtt + 12));
                l_Attributes[i].HandleServer = Marshal.ReadInt32((IntPtr) (l_RunAtt + 16));
                l_Attributes[i].AccessRights = (OPCAccessRights) Marshal.ReadInt32((IntPtr) (l_RunAtt + 20));
                l_Attributes[i].RequestedDataType = (VarEnum) Marshal.ReadInt16((IntPtr) (l_RunAtt + 32));
                l_Attributes[i].CanonicalDataType = (VarEnum) Marshal.ReadInt16((IntPtr) (l_RunAtt + 34));

                l_Attributes[i].EUType = (OPCEuType) Marshal.ReadInt32((IntPtr) (l_RunAtt + 36));
                l_Attributes[i].EUInfo = Marshal.GetObjectForNativeVariant((IntPtr) (l_RunAtt + 40));
                DummyVariant.VariantClear((IntPtr) (l_RunAtt + 40));

                int ptrblob = Marshal.ReadInt32((IntPtr) (l_RunAtt + 28));
                if (ptrblob != 0)
                {
                    int l_BlobSize = Marshal.ReadInt32((IntPtr) (l_RunAtt + 24));
                    if (l_BlobSize > 0)
                    {
                        l_Attributes[i].Blob = new byte[l_BlobSize];
                        Marshal.Copy((IntPtr) ptrblob, l_Attributes[i].Blob, 0, l_BlobSize);
                    }
                    Marshal.FreeCoTaskMem((IntPtr) ptrblob);
                }

                l_RunAtt += 56;
            }

            Marshal.FreeCoTaskMem(l_PtrAtt);
        }

        public void Skip(int p_CElt)
        {
            m_IfEnum.Skip(p_CElt);
        }

        public void Reset()
        {
            m_IfEnum.Reset();
        }
    }
}
