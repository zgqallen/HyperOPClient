

namespace GUI.CModel
{
    class OPCItem : IOPCItem
    {
        private string m_ID;
        private string m_Type;
        private string m_Value;
        private string m_Quality;
        private string m_Timestamp;

        public OPCItem(string p_ID, string p_Type)
        {
            m_ID = p_ID;
            m_Type = p_Type;
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
            set
            {
                m_Value = value;
            }
        }

        public string Quality
        {
            get
            {
                return m_Quality;
            }
            set
            {
                m_Quality = value;
            }
        }

        public string Timestamp
        {
            get
            {
                return m_Timestamp;
            }
            set
            {
                m_Timestamp = value;
            }
        }
    }
}
