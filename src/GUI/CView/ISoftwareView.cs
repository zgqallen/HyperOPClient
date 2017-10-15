
namespace GUI.CView
{
    public interface ISoftwareView
    {
        void Display();

        void Shutdown();

        int CloseServerTabView(IServerView p_ServerView);
    }
}
