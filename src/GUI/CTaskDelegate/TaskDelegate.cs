
using System;
using System.Reflection; 

using GUI.CErrorLog;

namespace GUI.CTaskDelegate
{
    public class TaskDelegate
    {
        public TaskDelegate(object p_Target, string p_MethodName, object[] p_Parameters)
        {
            try
            {
                Type l_Type = p_Target.GetType();
                MethodInfo l_MethodInfo = l_Type.GetMethod(p_MethodName);

                if (l_MethodInfo != null)
                {
                    l_MethodInfo.Invoke(p_Target, p_Parameters); 
                }
            }
            catch (Exception l_Ex)
            {
                ErrorLog l_ErrLog = ErrorLog.GetInstance();
                l_ErrLog.WriteToErrorLog(l_Ex.Message, l_Ex.StackTrace, "TaskDelegate");
            }
        }
    }
}
