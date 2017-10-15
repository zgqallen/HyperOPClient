using System;
using System.Text;
using System.Runtime.InteropServices;

namespace COPC.Common
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OPCServerInfo
    {
        public string ProgID;
        public string ServerName;
        public Guid ClsID;

        public override string ToString()
        {
            StringBuilder l_Sb = new StringBuilder("OPCServer: ", 300);

            l_Sb.AppendFormat("'{0}' ID={1} [{2}]", ServerName, ProgID, ClsID);

            return l_Sb.ToString();
        }
    }

    [ComVisible(true)]
    public class OPCServerLister
    {
        private object m_OPCListObj;
        private IOPCServerList m_IfList;

        private object m_EnumObj;
        private IEnumGuid m_IfEnum;

        public OPCServerLister()
        {
            m_OPCListObj = null;
            m_IfList = null;
            m_EnumObj = null;
            m_IfEnum = null;
        }

        ~OPCServerLister()
        {
            Dispose();
        }

        public void ListAllServersOnMachineV1(string p_MachineName, out OPCServerInfo[] p_ServersList)
        {
            // CATID OPCDAServer10 == OPC Data Access Server Version 1.0
            ListAllOnMachine(new Guid("63D5F430-CFE4-11D1-B2C8-0060083BA1FB"), p_MachineName, out p_ServersList);
        }

        public void ListAllServersOnMachineV2(string p_MachineName, out OPCServerInfo[] p_ServersList)
        {
            // CATID OPCDAServer20 == OPC Data Access Server Version 2.0
            ListAllOnMachine(new Guid("63D5F432-CFE4-11D1-B2C8-0060083BA1FB"), p_MachineName, out p_ServersList);
        }

        public void ListAllServersOnMachineV3(string p_MachineName, out OPCServerInfo[] p_ServersList)
        {
            // CATID OPCDAServer30 == OPC Data Access Server Version 3.0
            ListAllOnMachine(new Guid("CC603642-66D7-48F1-B69A-B625E73652D7"), p_MachineName, out p_ServersList);
        }

        public void ListAllOnMachine(Guid p_CatID, string p_MachineName, out OPCServerInfo[] p_ServersList)
        {
            p_ServersList = null;

            Dispose();

            Guid l_Guid = new Guid("13486D51-4821-11D2-A494-3CB306C10000");
            Type l_TypeOfList = Type.GetTypeFromCLSID(l_Guid, p_MachineName);
            m_OPCListObj = Activator.CreateInstance(l_TypeOfList);

            m_IfList = (IOPCServerList) m_OPCListObj;
            if (m_IfList == null)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            m_IfList.EnumClassesOfCategories(1, ref p_CatID, 0, ref p_CatID, out m_EnumObj);
            if (m_EnumObj == null)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            m_IfEnum = (IEnumGuid) m_EnumObj;
            if (m_IfEnum == null)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            int l_MaxCount = 300;
            IntPtr l_PtrGuid = Marshal.AllocCoTaskMem(l_MaxCount * 16);
            int l_Count = 0;
            m_IfEnum.Next(l_MaxCount, l_PtrGuid, out l_Count);
            if (l_Count < 1)
            {
                Marshal.FreeCoTaskMem(l_PtrGuid);
                return;
            }

            p_ServersList = new OPCServerInfo[l_Count];

            byte[] l_GuidBin = new byte[16];
            int l_RunGuid = (int) l_PtrGuid;
            for (int i = 0; i < l_Count; ++i)
            {
                p_ServersList[i] = new OPCServerInfo();
                Marshal.Copy((IntPtr) l_RunGuid, l_GuidBin, 0, 16);
                p_ServersList[i].ClsID = new Guid(l_GuidBin);
                m_IfList.GetClassDetails(ref p_ServersList[i].ClsID, out p_ServersList[i].ProgID,
                    out p_ServersList[i].ServerName);
                l_RunGuid += 16;
            }

            Marshal.FreeCoTaskMem(l_PtrGuid);
            Dispose();
        }

        public void Dispose()
        {
            m_IfEnum = null;
            if (m_EnumObj != null)
            {
                Marshal.ReleaseComObject(m_EnumObj);
                m_EnumObj = null;
            }

            m_IfList = null;
            if (m_OPCListObj != null)
            {
                Marshal.ReleaseComObject(m_OPCListObj);
                m_OPCListObj = null;
            }
        }
    }

    public struct HResults
    {
        public const int S_OK = 0x00000000;
        public const int S_FALSE = 0x00000001;

        // WinError.h
        public const int E_NOTIMPL = unchecked((int)0x80004001);
        public const int E_NOINTERFACE = unchecked((int)0x80004002);
        public const int E_ABORT = unchecked((int)0x80004004);
        public const int E_FAIL = unchecked((int)0x80004005);
        public const int E_OUTOFMEMORY = unchecked((int)0x8007000E);
        public const int E_INVALIDARG = unchecked((int)0x80070057);

        // OleCtl.h
        public const int CONNECT_E_NOCONNECTION = unchecked((int)0x80040200);
        public const int CONNECT_E_ADVISELIMIT = unchecked((int)0x80040201);

        // OpcError.h
        public const int OPC_E_INVALIDHANDLE = unchecked((int)0xC0040001);
        public const int OPC_E_BADTYPE = unchecked((int)0xC0040004);
        public const int OPC_E_PUBLIC = unchecked((int)0xC0040005);
        public const int OPC_E_BADRIGHTS = unchecked((int)0xC0040006);
        public const int OPC_E_UNKNOWNITEMID = unchecked((int)0xC0040007);
        public const int OPC_E_INVALIDITEMID = unchecked((int)0xC0040008);
        public const int OPC_E_INVALIDFILTER = unchecked((int)0xC0040009);
        public const int OPC_E_UNKNOWNPATH = unchecked((int)0xC004000A);
        public const int OPC_E_RANGE = unchecked((int)0xC004000B);
        public const int OPC_E_DUPLICATENAME = unchecked((int)0xC004000C);
        public const int OPC_S_UNSUPPORTEDRATE = unchecked((int)0x0004000D);
        public const int OPC_S_CLAMP = unchecked((int)0x0004000E);
        public const int OPC_S_INUSE = unchecked((int)0x0004000F);
        public const int OPC_E_INVALIDCONFIGFILE = unchecked((int)0xC0040010);
        public const int OPC_E_NOTFOUND = unchecked((int)0xC0040011);
        public const int OPC_E_INVALID_PID = unchecked((int)0xC0040203);

        public static bool Failed(int p_HResultCode)
        {
            return (p_HResultCode < 0);
        }

        public static bool Succeeded(int p_HResultCode)
        {
            return (p_HResultCode >= 0);
        }
    }

    public enum VarEnumArray
    {
        SHORT_ARRAY = 8194,
        LONG_ARRAY = 8195,
        FLOAT_ARRAY = 8196,
        DOUBLE_ARRAY = 8197
    }

    [ComVisible(true), StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class DummyVariant
    {
        private const short VT_TYPEMASK = 0x0fff;
        private const short VT_VECTOR = 0x1000;
        private const short VT_ARRAY = 0x2000;
        private const short VT_BYREF = 0x4000;
        private const short VT_ILLEGAL = unchecked((short)0xffff);

        public const int CONST_SIZE = 16;

        [DllImport("oleaut32.dll")]
        public static extern void VariantInit(IntPtr p_AddrOfVariant);

        [DllImport("oleaut32.dll")]
        public static extern int VariantClear(IntPtr p_AddrOfVariant);

        public static string VarEnumToString(VarEnum p_VT)
        {
            string l_StrVT = "";
            short l_VTShort = (short) p_VT;

            if (l_VTShort == VT_ILLEGAL)
            {
                return "VT_ILLEGAL";
            }
            if ((l_VTShort & VT_ARRAY) != 0)
            {
                l_StrVT += "VT_ARRAY | ";
            }
            if ((l_VTShort & VT_BYREF) != 0)
            {
                l_StrVT += "VT_BYREF | ";
            }
            if ((l_VTShort & VT_VECTOR) != 0)
            {
                l_StrVT += "VT_VECTOR | ";
            }

            VarEnum l_VTBase = (VarEnum) (l_VTShort & VT_TYPEMASK);
            l_StrVT += l_VTBase.ToString();

            return l_StrVT;
        }
    }

    [ComVisible(true), ComImport, Guid("F31DFDE2-07B6-11d2-B2D8-0060083BA1FB"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCCommon
    {
        void SetLocaleID(
            [In]    int p_Lcid);

        void GetLocaleID(
            [Out]   out int p_Lcid);

        [PreserveSig]
        int QueryAvailableLocaleIDs(
            [Out]   out int p_Count,
            [Out]   out	IntPtr p_Lcid);

        void GetErrorString(
            [In]                                    int p_Error,
            [Out, MarshalAs(UnmanagedType.LPWStr)]  out	string p_String);

        void SetClientName(
            [In, MarshalAs(UnmanagedType.LPWStr)]   string p_Name);
    }

    [ComVisible(true), ComImport, Guid("F31DFDE1-07B6-11d2-B2D8-0060083BA1FB"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCShutdown
    {
        void ShutdownRequest(
            [In, MarshalAs(UnmanagedType.LPWStr)]   string p_Reason);
    }

    [ComVisible(true), ComImport, Guid("13486D50-4821-11D2-A494-3CB306C10000"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOPCServerList
    {
        void EnumClassesOfCategories(
            [In]                                        int p_Implemented,
            [In]                                        ref Guid p_CatidImpl,
            [In]                                        int p_Required,
            [In]                                        ref Guid p_CatidReq,
            [Out, MarshalAs(UnmanagedType.IUnknown)]    out	object p_Unk);

        void GetClassDetails(
            [In]                                    ref Guid p_Clsid,
            [Out, MarshalAs(UnmanagedType.LPWStr)]  out	string p_ProgID,
            [Out, MarshalAs(UnmanagedType.LPWStr)]  out	string p_UserType);

        void CLSIDFromProgID(
            [In, MarshalAs(UnmanagedType.LPWStr)]   string p_ProgID,
            [Out]                                   out Guid p_Clsid);
    }

    [ComVisible(true), ComImport, Guid("0002E000-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumGuid
    {
        void Next(
            [In]    int p_CElt,
            [In]    IntPtr p_RgElt,
            [Out]   out int p_CEltFetched);

        void Skip(
            [In]    int p_CElt);

        void Reset();

        void Clone(
            [Out, MarshalAs(UnmanagedType.IUnknown)]    out	object p_Unk);
    }
}
