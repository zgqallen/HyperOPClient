

namespace GUI.CModel
{
    public interface IOPCItem
    {
        string ID
        {
            get;
        }

        string Type
        {
            get;
        }

        string Value
        {
            get;
            set;
        }

        string Quality
        {
            get;
            set;
        }

        string Timestamp
        {
            get;
            set;
        }
    }
}
