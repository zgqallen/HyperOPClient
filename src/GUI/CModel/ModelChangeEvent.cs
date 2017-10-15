

using System;

namespace GUI.CModel
{
    public enum ModelChangeEventType
    {
        ADD, UPDATE
    }

    public class ModelChangeEventArgs : EventArgs
    {
        private ModelChangeEventType m_EventType;

        private string m_ID;
        private string m_Type;
        private string m_Value;
        private string m_Quality;
        private string m_Timestamp;

        public ModelChangeEventArgs(ModelChangeEventType p_EventType, string p_ID, string p_Type,
            string p_Value, string p_Quality, string p_Timestamp)
        {
            m_EventType = p_EventType;

            m_ID = p_ID;
            m_Type = p_Type;
            m_Value = p_Value;
            m_Quality = p_Quality;
            m_Timestamp = p_Timestamp;
        }

        public ModelChangeEventType EventType
        {
            get
            {
                return m_EventType;
            }
        }

        public string ID
        {
            get
            {
                return m_ID;
            }
        }

        public string Type
        {
            get
            {
                return m_Type;
            }
        }

        public string Value
        {
            get
            {
                return m_Value;
            }
        }

        public string Quality
        {
            get
            {
                return m_Quality;
            }
        }

        public string Timestamp
        {
            get
            {
                return m_Timestamp;
            }
        }
    }

    public delegate void ModelChangeEventHandler(object p_Sender, ModelChangeEventArgs p_Event);
}
