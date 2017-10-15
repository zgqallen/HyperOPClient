using System;
using System.Runtime.InteropServices;

using COPC.Common;

namespace COPC.Datas
{
    public enum OPCDataSource
    {
        OPC_DS_CACHE = 1,
        OPC_DS_DEVICE = 2
    }

    public enum OPCBrowseType
    {
        OPC_BRANCH = 1,
        OPC_LEAF = 2,
        OPC_FLAT = 3
    }

    public enum OPCNamespaceType
    {
        OPC_NS_HIERARCHIAL = 1,
        OPC_NS_FLAT = 2
    }

    public enum OPCBrowseDirection
    {
        OPC_BROWSE_UP = 1,
        OPC_BROWSE_DOWN = 2,
        OPC_BROWSE_TO = 3
    }

    [Flags]
    public enum OPCAccessRights
    {
        OPC_READABLE = 1,
        OPC_WRITEABLE = 2
    }

    public enum OPCEuType
    {
        OPC_NOENUM = 0,
        OPC_ANALOG = 1,
        OPC_ENUMERATED = 2
    }

    public enum OPCServerState
    {
        OPC_STATUS_RUNNING = 1,
        OPC_STATUS_FAILED = 2,
        OPC_STATUS_NOCONFIG = 3,
        OPC_STATUS_SUSPENDED = 4,
        OPC_STATUS_TEST = 5
    }

    public enum OPCEnumScope
    {
        OPC_ENUM_PRIVATE_CONNECTIONS = 1,
        OPC_ENUM_PUBLIC_CONNECTIONS = 2,
        OPC_ENUM_ALL_CONNECTIONS = 3,
        OPC_ENUM_PRIVATE = 4,
        OPC_ENUM_PUBLIC = 5,
        OPC_ENUM_ALL = 6
    }

    [Flags]
    public enum OPCQualityMasks : short
    {
        LIMIT_MASK = 0x0003,
        STATUS_MASK = 0x00FC,
        MASTER_MASK = 0x00C0
    }

    [Flags]
    public enum OPCQualityMaster : short
    {
        QUALITY_BAD = 0x0000,
        QUALITY_UNCERTAIN = 0x0040,
        // Not standard
        ERROR_QUALITY_VALUE = 0x0080,
        QUALITY_GOOD = 0x00C0
    }

    [Flags]
    public enum OPCQualityStatus : short
    {
        // Values for Quality = BAD
        BAD = 0x0000,
        CONFIG_ERROR = 0x0004,
        NOT_CONNECTED = 0x0008,
        DEVICE_FAILURE = 0x000c,
        SENSOR_FAILURE = 0x0010,
        LAST_KNOWN = 0x0014,
        COMM_FAILURE = 0x0018,
        OUT_OF_SERVICE = 0x001C,

        // Values for Quality = UNCERTAIN
        UNCERTAIN = 0x0040,
        LAST_USABLE = 0x0044,
        SENSOR_CAL = 0x0050,
        EGU_EXCEEDED = 0x0054,
        SUB_NORMAL = 0x0058,

        // Value for Quality = GOOD
        OK = 0x00C0,
        LOCAL_OVERRIDE = 0x00D8
    }

    [Flags]
    public enum OPCQualityLimit
    {
        LIMIT_OK = 0x0000,
        LIMIT_LOW = 0x0001,
        LIMIT_HIGH = 0x0002,
        LIMIT_CONST = 0x0003
    }

    public enum OPCProps
    {
        OPC_PROP_CDT = 1,
        OPC_PROP_VALUE = 2,
        OPC_PROP_QUALITY = 3,
        OPC_PROP_TIME = 4,
        OPC_PROP_RIGHTS = 5,
        OPC_PROP_SCANRATE = 6,

        OPC_PROP_UNIT = 100,
        OPC_PROP_DESC = 101,
        OPC_PROP_HIEU = 102,
        OPC_PROP_LOEU = 103,
        OPC_PROP_HIRANGE = 104,
        OPC_PROP_LORANGE = 105,
        OPC_PROP_CLOSE = 106,
        OPC_PROP_OPEN = 107,
        OPC_PROP_TIMEZONE = 108,

        OPC_PROP_FGC = 200,
        OPC_PROP_BGC = 201,
        OPC_PROP_BLINK = 202,
        OPC_PROP_BMP = 203,
        OPC_PROP_SND = 204,
        OPC_PROP_HTML = 205,
        OPC_PROP_AVI = 206,

        OPC_PROP_ALMSTAT = 300,
        OPC_PROP_ALMHELP = 301,
        OPC_PROP_ALMAREAS = 302,
        OPC_PROP_ALMPRIMARYAREA = 303,
        OPC_PROP_ALMCONDITION = 304,
        OPC_PROP_ALMLIMIT = 305,
        OPC_PROP_ALMDB = 306,
        OPC_PROP_ALMHH = 307,
        OPC_PROP_ALMH = 308,
        OPC_PROP_ALML = 309,
        OPC_PROP_ALMLL = 310,
        OPC_PROP_ALMROC = 311,
        OPC_PROP_ALMDEV = 312
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Unicode)]
    public struct ServerStatus
    {
        public long StartTime;
        public long CurrentTime;
        public long LastUpdateTime;

        [MarshalAs(UnmanagedType.U4)]
        public OPCServerState ServerState;

        public int GroupCount;
        public int BandWidth;
        public short MajorVersion;
        public short MinorVersion;
        public short BuildNumber;
        public short Reserved;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string VendorInfo;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Unicode)]
    internal struct OPCItemDefIntern
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string AccessPath;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string ItemID;

        [MarshalAs(UnmanagedType.Bool)]
        public bool Active;

        public int Client;
        public int BlobSize;
        public IntPtr Blob;

        public short RequestedDataType;

        public short Reserved;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct OPCItemResultIntern
    {
        public int Server;
        public short CanonicalDataType;
        public short Reserved;

        [MarshalAs(UnmanagedType.U4)]
        public OPCAccessRights AccessRights;

        public int BlobSize;
        public int Blob;
    };

    [ComVisible(true), ComImport, Guid("39c13a4d-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCServer
    {
        void AddGroup(
            [In, MarshalAs(UnmanagedType.LPWStr)]                   string p_Name,
            [In, MarshalAs(UnmanagedType.Bool)]                     bool p_Active,
            [In]                                                    int p_RequestedUpdateRate,
            [In]                                                    int p_ClientGroup,
            [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)]   int[] p_TimeBias,
            [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)]   float[] p_PercentDeadband,
            [In]                                                    int p_LCID,
            [Out]                                                   out	int p_ServerGroup,
            [Out]                                                   out	int p_RevisedUpdateRate,
            [In]                                                    ref Guid p_Guid,
            [Out, MarshalAs(UnmanagedType.IUnknown)]                out	object p_Unk);

        void GetErrorString(
            [In]                                    int p_Error,
            [In]                                    int p_Locale,
            [Out, MarshalAs(UnmanagedType.LPWStr)]  out	string p_String);

        void GetGroupByName(
            [In, MarshalAs(UnmanagedType.LPWStr)]       string p_Name,
            [In]                                        ref Guid p_Guid,
            [Out, MarshalAs(UnmanagedType.IUnknown)]    out	object p_Unk);

        void GetStatus(
            [Out, MarshalAs(UnmanagedType.Struct)]    out ServerStatus p_ServerStatus);

        void RemoveGroup(
            [In]                                int p_ServerGroup,
            [In, MarshalAs(UnmanagedType.Bool)] bool p_Force);

        [PreserveSig]
        int CreateGroupEnumerator(
            [In]                                        int p_Scope,
            [In]                                        ref Guid p_Guid,
            [Out, MarshalAs(UnmanagedType.IUnknown)]    out	object p_Unk);
    }

    [ComVisible(true), ComImport, Guid("39c13a4e-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCServerPublicGroups
    {
        void GetPublicGroupByName(
            [In, MarshalAs(UnmanagedType.LPWStr)]       string p_Name,
            [In]                                        ref Guid p_Guid,
            [Out, MarshalAs(UnmanagedType.IUnknown)]    out	object p_Unk);

        void RemovePublicGroup(
            [In]                                int p_ServerGroup,
            [In, MarshalAs(UnmanagedType.Bool)] bool p_Force);
    }

    [ComVisible(true), ComImport, Guid("39c13a4f-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCBrowseServerAddressSpace
    {
        void QueryOrganization(
            [Out, MarshalAs(UnmanagedType.U4)]  out	OPCNamespaceType p_NameSpaceType);

        void ChangeBrowsePosition(
            [In, MarshalAs(UnmanagedType.U4)]       OPCBrowseDirection p_BrowseDirection,
            [In, MarshalAs(UnmanagedType.LPWStr)]   string p_Name);

        [PreserveSig]
        int BrowseOPCItemIDs(
            [In, MarshalAs(UnmanagedType.U4)]           OPCBrowseType p_BrowseFilterType,
            [In, MarshalAs(UnmanagedType.LPWStr)]       string p_FilterCriteria,
            [In, MarshalAs(UnmanagedType.U2)]           short p_DataTypeFilter,
            [In, MarshalAs(UnmanagedType.U4)]           OPCAccessRights p_AccessRightsFilter,
            [Out, MarshalAs(UnmanagedType.IUnknown)]    out	object p_Unk);

        void GetItemID(
            [In, MarshalAs(UnmanagedType.LPWStr)]   string p_ItemDataID,
            [Out, MarshalAs(UnmanagedType.LPWStr)]  out	string p_ItemID);

        [PreserveSig]
        int BrowseAccessPaths(
            [In, MarshalAs(UnmanagedType.LPWStr)]       string p_ItemID,
            [Out, MarshalAs(UnmanagedType.IUnknown)]    out	object p_Unk);
    }

    [ComVisible(true), ComImport, Guid("39c13a72-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCItemProperties
    {
        void QueryAvailableProperties(
            [In, MarshalAs(UnmanagedType.LPWStr)]   string p_ItemID,
            [Out]                                   out int p_Count,
            [Out]                                   out IntPtr p_PropertyIDs,
            [Out]                                   out IntPtr p_Descriptions,
            [Out]                                   out	IntPtr p_vtDataTypes);

        [PreserveSig]
        int GetItemProperties(
            [In, MarshalAs(UnmanagedType.LPWStr)]                       string p_ItemID,
            [In]                                                        int p_Count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]  int[] p_PropertyIDs,
            [Out]                                                       out IntPtr p_Data,
            [Out]                                                       out	IntPtr p_Errors);

        [PreserveSig]
        int LookupItemIDs(
            [In, MarshalAs(UnmanagedType.LPWStr)]                       string p_ItemID,
            [In]                                                        int p_Count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]  int[] p_PropertyIDs,
            [Out]                                                       out IntPtr p_NewItemIDs,
            [Out]                                                       out	IntPtr p_Errors);
    }

    [ComVisible(true), Guid("39c13a50-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCGroupStateMgt
    {
        void GetState(
            [Out]                                   out	int p_UpdateRate,
            [Out, MarshalAs(UnmanagedType.Bool)]    out	bool p_Active,
            [Out, MarshalAs(UnmanagedType.LPWStr)]  out	string p_Name,
            [Out]                                   out	int p_TimeBias,
            [Out]                                   out	float p_PercentDeadband,
            [Out]                                   out	int p_LCID,
            [Out]                                   out	int p_ClientGroup,
            [Out]                                   out	int p_ServerGroup);

        void SetState(
            [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)]                                       int[] p_RequestedUpdateRate,
            [Out]                                                                                       out	int p_RevisedUpdateRate,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Bool, SizeConst = 1)]    bool[] p_Active,
            [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)]                                       int[] p_TimeBias,
            [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)]                                       float[] p_PercentDeadband,
            [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)]                                       int[] p_LCID,
            [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)]                                       int[] p_ClientGroup);

        void SetName(
            [In, MarshalAs(UnmanagedType.LPWStr)]   string p_Name);

        void CloneGroup(
            [In, MarshalAs(UnmanagedType.LPWStr)]       string p_Name,
            [In]                                        ref Guid p_Guid,
            [Out, MarshalAs(UnmanagedType.IUnknown)]    out	object p_Unk);
    }

    [ComVisible(true), Guid("39c13a51-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCPublicGroupStateMgt
    {
        void GetState(
            [Out, MarshalAs(UnmanagedType.Bool)]    out	bool p_Public);

        void MoveToPublic();
    }

    [ComVisible(true), ComImport, Guid("39c13a54-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCItemMgt
    {
        [PreserveSig]
        int AddItems(
            [In]    int p_Count,
            [In]    IntPtr p_ItemArray,
            [Out]   out IntPtr p_AddResults,
            [Out]   out	IntPtr p_Errors);

        [PreserveSig]
        int ValidateItems(
            [In]                                int p_Count,
            [In]                                IntPtr p_ItemArray,
            [In, MarshalAs(UnmanagedType.Bool)] bool p_BlobUpdate,
            [Out]                               out	IntPtr p_ValidationResults,
            [Out]                               out	IntPtr p_Errors);

        [PreserveSig]
        int RemoveItems(
            [In]                                                        int p_Count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]  int[] p_Server,
            [Out]                                                       out	IntPtr p_Errors);

        [PreserveSig]
        int SetActiveState(
            [In]                                                        int p_Count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]  int[] p_Server,
            [In, MarshalAs(UnmanagedType.Bool)]                         bool p_Active,
            [Out]                                                       out	IntPtr p_Errors);

        [PreserveSig]
        int SetClientHandles(
            [In]                                                        int p_Count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]  int[] p_Server,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]  int[] p_Client,
            [Out]                                                       out	IntPtr p_Errors);

        [PreserveSig]
        int SetDatatypes(
            [In]                                                        int p_Count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]  int[] p_Server,
            [In]                                                        IntPtr p_RequestedDatatypes,
            [Out]                                                       out	IntPtr p_Errors);

        [PreserveSig]
        int CreateEnumerator(
            [In]                                        ref Guid p_Guid,
            [Out, MarshalAs(UnmanagedType.IUnknown)]    out	object p_Unk);
    }

    [ComVisible(true), ComImport, Guid("39c13a52-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCSyncIO
    {
        [PreserveSig]
        int Read(
            [In, MarshalAs(UnmanagedType.U4)]                           OPCDataSource p_Source,
            [In]                                                        int p_Count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]  int[] p_Server,
            [Out]                                                       out IntPtr p_ItemValues,
            [Out]                                                       out	IntPtr p_Errors);

        [PreserveSig]
        int Write(
            [In]                                                        int p_Count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]  int[] p_Server,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]  object[] p_ItemValues,
            [Out]                                                       out	IntPtr p_Errors);
    }

    [ComVisible(true), ComImport, Guid("39c13a71-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCAsyncIO2
    {
        [PreserveSig]
        int Read(
            [In]                                                        int p_Count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]  int[] p_Server,
            [In]                                                        int p_TransactionID,
            [Out]                                                       out int p_CancelID,
            [Out]                                                       out	IntPtr p_Errors);

        [PreserveSig]
        int Write(
            [In]                                                        int p_Count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]  int[] p_Server,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]  object[] p_ItemValues,
            [In]                                                        int p_TransactionID,
            [Out]                                                       out int p_CancelID,
            [Out]                                                       out	IntPtr p_Errors);

        void Refresh2(
            [In, MarshalAs(UnmanagedType.U4)]   OPCDataSource p_Source,
            [In]                                int p_TransactionID,
            [Out]                               out int p_CancelID);

        void Cancel2(
            [In]    int p_CancelID);

        void SetEnable(
            [In, MarshalAs(UnmanagedType.Bool)] bool p_Enable);

        void GetEnable(
            [Out, MarshalAs(UnmanagedType.Bool)]    out	bool p_Enable);
    }

    [ComVisible(true), ComImport, Guid("39c13a70-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCDataCallback
    {
        void OnDataChange(
            [In]    int p_Transid,
            [In]    int p_Group,
            [In]    int p_Masterquality,
            [In]    int p_Mastererror,
            [In]    int p_Count,
            [In]    IntPtr p_ClientItems,
            [In]    IntPtr p_Values,
            [In]    IntPtr p_Qualities,
            [In]    IntPtr p_TimeStamps,
            [In]    IntPtr p_Errors);

        void OnReadComplete(
            [In]    int p_Transid,
            [In]    int p_Group,
            [In]    int p_Masterquality,
            [In]    int p_Mastererror,
            [In]    int p_Count,
            [In]    IntPtr p_ClientItems,
            [In]    IntPtr p_Values,
            [In]    IntPtr p_Qualities,
            [In]    IntPtr p_TimeStamps,
            [In]    IntPtr p_Errors);

        void OnWriteComplete(
            [In]    int p_Transid,
            [In]    int p_Group,
            [In]    int p_MasterErr,
            [In]    int p_Count,
            [In]    IntPtr p_ClientHandles,
            [In]    IntPtr p_Errors);

        void OnCancelComplete(
            [In]    int p_Transid,
            [In]    int p_Group);
    }

    [ComVisible(true), ComImport, Guid("39c13a55-011e-11d0-9675-0020afd8adb3"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumOPCItemAttributes
    {
        void Next(
            [In]    int p_CElt,
            [Out]   out	IntPtr p_ItemArray,
            [Out]   out int p_CEltFetched);

        void Skip(
            [In]    int p_CElt);

        void Reset();

        void Clone(
            [Out, MarshalAs(UnmanagedType.IUnknown)]    out	object p_Unk);
    }
}
