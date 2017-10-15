
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

using GUI.CController;
using GUI.CErrorLog;

namespace GUI.CView
{
    partial class SoftwareFrame : Form, ISoftwareView
    {
        private ISoftwareController m_SoftwareController;

        private string m_CurrentActionDescription;
        private int m_CurrentActionErrorCount;
        private bool m_IsActionRunning;
        private string m_SelectedMachineName;
        private string m_SelectedServerID;

        private string m_Database_URL = "127.0.0.1";
        private string m_Database_Username = "sa";
        private string m_Database_Password = "test123";
        private string m_Database_Name = "OPCDB";
        private SqlConnection Database_con;

        private UpdateServerListDelegate m_UpdateServerListDelegate;
        private UpdateTabListDelegate m_UpdateTabListDelegate;
        private UpdateReportDelegate m_UpdateReportDelegate;
        private UpdateStatusDelegate m_UpdateStatusDelegate;

        private delegate void UpdateServerListDelegate(ListView p_ListView, string[] p_ServerInfo);
        private delegate void UpdateTabListDelegate(TabControl p_TabControl, TabPage p_ServerTab);
        private delegate void UpdateReportDelegate(RichTextBox p_TextBox, string p_Message);
        private delegate void UpdateStatusDelegate(ToolStripStatusLabel p_StatusLabel, string p_Message);

        public SoftwareFrame(ISoftwareController p_SoftwareController)
        {
            InitializeComponent();

            m_SoftwareController = p_SoftwareController;

            ErrorLog l_ErrorLog = ErrorLog.GetInstance();
            l_ErrorLog.WriteToErrorLog("Initialize Successfully software Version V1.0. Build Date：2016-01-24.", "[SoftwareFrame]", "[Main]");
        }

        public void Display()
        {
            Application.Run(this);
        }

        public void Shutdown()
        {
            Dispose();
        }

        public int CloseServerTabView(IServerView p_ServerView)
        {
            int l_Index = m_ServersTabControl.TabPages.IndexOf(p_ServerView.GetTabPage());

            m_ServersTabControl.SelectedIndex = m_ServersTabControl.SelectedIndex - 1;
            m_ServersTabControl.TabPages.Remove(p_ServerView.GetTabPage());

            return l_Index;
        }

        private void UpdateServerListFunc(ListView p_ListView, string[] p_ServerInfo)
        {
            p_ListView.Items.Add(new ListViewItem(p_ServerInfo));
        }

        private void UpdateTabListFunc(TabControl p_TabControl, TabPage p_ServerTab)
        {
            p_TabControl.Controls.Add(p_ServerTab);
            p_TabControl.SelectedIndex = p_TabControl.TabPages.Count - 1;
        }

        private void UpdateReportFunc(RichTextBox p_TextBox, string p_Message)
        {
            p_TextBox.AppendText(p_Message);
            p_TextBox.ScrollToCaret();
        }

        private void UpdateStatusFunc(ToolStripStatusLabel p_StatusLabel, string p_Message)
        {
            p_StatusLabel.Text = p_Message;
        }

        private void UpdateServerListView(string[] p_ServerInfo)
        {
            Invoke(m_UpdateServerListDelegate, new object[] { m_SrvList, p_ServerInfo });
        }

        private void UpdateServerTabList(TabPage p_ServerTab)
        {
            Invoke(m_UpdateTabListDelegate, new object[] { m_ServersTabControl, p_ServerTab });
        }

        private void DisplayMessageInReport(string p_Message)
        {
            Invoke(m_UpdateReportDelegate, new object[] { m_ConnectionReport, p_Message });
        }

        private void DisplayMessageInActionStatus(string p_Message)
        {
            Invoke(m_UpdateStatusDelegate, new object[] { m_ActionStatusLabel, p_Message });
        }

        private void DisplayMessageInStateStatus(string p_Message)
        {
            Invoke(m_UpdateStatusDelegate, new object[] { m_StateStatusLabel, p_Message });
        }

        private void ConnectToOPCServer()
        {
            if (m_SrvList.SelectedItems.Count > 0)
            {
                if (m_IsActionRunning)
                {
                    MessageBox.Show(this, "An action is already running. You have to wait until it is finished.", "Action on OPC servers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ListViewItem l_SelectedItem = m_SrvList.SelectedItems[0];
                    m_SelectedMachineName = l_SelectedItem.SubItems[0].Text;
                    m_SelectedServerID = l_SelectedItem.SubItems[2].Text;

                    m_ConnectSrvBackgroundWorker.RunWorkerAsync();
                }
            }
        }

        private void SoftwareFrame_Load(object p_Sender, EventArgs p_EvArgs)
        {
            m_CurrentActionDescription = "HyperOPClient load";
            m_CurrentActionErrorCount = 0;
            m_IsActionRunning = false;
            m_SelectedMachineName = "";
            m_SelectedServerID = "";

            m_UpdateServerListDelegate = new UpdateServerListDelegate(UpdateServerListFunc);
            m_UpdateTabListDelegate = new UpdateTabListDelegate(UpdateTabListFunc);
            m_UpdateReportDelegate = new UpdateReportDelegate(UpdateReportFunc);
            m_UpdateStatusDelegate = new UpdateStatusDelegate(UpdateStatusFunc);

#if NULL
            m_LocalSearchCheckBox.Checked = true;
            m_RemoteSearchCheckBox.Checked = false;

            m_RemoteMachineText.Text = "";
            m_RemoteMachineText.Enabled = false;
#endif
            DisplayMessageInActionStatus(m_CurrentActionDescription + " finished");
            DisplayMessageInStateStatus(Convert.ToString(m_CurrentActionErrorCount) + " error(s)");
        }

        private void SoftwareFrame_FormClosed(object p_Sender, FormClosedEventArgs p_EvArgs)
        {
            m_SoftwareController.DisconnectFromAllOPCServers();
        }

        private void OpenMenuItem_Click(object p_Sender, EventArgs p_EvArgs)
        {

        }

        private void SaveMenuItem_Click(object p_Sender, EventArgs p_EvArgs)
        {

        }

        private void SaveAsMenuItem_Click(object p_Sender, EventArgs p_EvArgs)
        {

        }

        private void QuitMenuItem_Click(object p_Sender, EventArgs p_EvArgs)
        {
            m_SoftwareController.DisposeView();
            m_SoftwareController.DisconnectFromAllOPCServers();
        }

        private void ListServersButton_Click(object p_Sender, EventArgs p_EvArgs)
        {
#if NULL
            if (m_RemoteSearchCheckBox.Checked && (m_RemoteMachineText.Text == ""))
            {
                MessageBox.Show(this, "You have to specify an IP address or a machine name.", "Search of OPC servers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (m_IsActionRunning)
                {
                    MessageBox.Show(this, "An action is already running. You have to wait until it is finished.", "Action on OPC servers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    m_SrvList.Items.Clear();
                    m_ListSrvBackgroundWorker.RunWorkerAsync();
                }
            }
#endif
            if (m_IsActionRunning)
            {
                MessageBox.Show(this, "An action is already running. You have to wait until it is finished.", "Action on OPC servers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                m_SrvList.Items.Clear();
                m_ListSrvBackgroundWorker.RunWorkerAsync();
            }
        }

        private void ListSrvBackgroundWorker_DoWork(object p_Sender, DoWorkEventArgs p_EvArgs)
        {
            m_IsActionRunning = true;
            m_ListSrvBackgroundWorker.ReportProgress(0);

            try
            {
                List<string[]> l_ServerList = null;

                m_CurrentActionErrorCount = 0;
                m_ListSrvBackgroundWorker.ReportProgress(50);

#if NULL
                if (m_LocalSearchCheckBox.Checked)
                {
                    m_CurrentActionDescription = "Local search of OPC servers";

                    DisplayMessageInReport("Search of local OPC servers\n");
                    DisplayMessageInActionStatus(m_CurrentActionDescription);
                    DisplayMessageInStateStatus("Running");

                    l_ServerList = m_SoftwareController.SearchOPCServers("Localhost");
                }
                else
                {
                    string l_RemoteMachineName = m_RemoteMachineText.Text;
                    m_CurrentActionDescription = "Remote search of OPC servers";
                    
                    DisplayMessageInReport("Search of remote OPC servers on the machine : " + l_RemoteMachineName + "\n");
                    DisplayMessageInActionStatus(m_CurrentActionDescription);
                    DisplayMessageInStateStatus("Running");
                    
                    l_ServerList = m_SoftwareController.SearchOPCServers(l_RemoteMachineName);
                }
#endif
                m_CurrentActionDescription = "Local search of OPC servers";

                DisplayMessageInReport("Search of local OPC servers\n");
                DisplayMessageInActionStatus(m_CurrentActionDescription);
                DisplayMessageInStateStatus("Running");

                l_ServerList = m_SoftwareController.SearchOPCServers("Localhost");

                if (l_ServerList != null)
                {
                    foreach (string[] l_ServerInfo in l_ServerList)
                    {
                        UpdateServerListView(l_ServerInfo);
                    }
                }
            }
            catch (Exception l_Ex)
            {
                ErrorLog l_ErrorLog = ErrorLog.GetInstance();
                l_ErrorLog.WriteToErrorLog(l_Ex.Message, l_Ex.StackTrace, "Error during OPC server search");
                
                m_CurrentActionErrorCount++;

                DisplayMessageInReport(l_Ex.Message + "\n");
            }

            m_ListSrvBackgroundWorker.ReportProgress(100);
        }

        private void ConnectSrvBackgroundWorker_DoWork(object p_Sender, DoWorkEventArgs p_EvArgs)
        {
            m_IsActionRunning = true;
            m_ConnectSrvBackgroundWorker.ReportProgress(0);
            m_CurrentActionErrorCount = 0;

            m_CurrentActionDescription = "Connection to an OPC server";

            DisplayMessageInReport("Connection to the server : " + m_SelectedServerID + "\n");
            DisplayMessageInActionStatus(m_CurrentActionDescription);
            DisplayMessageInStateStatus("Running");

            try
            {
                m_ConnectSrvBackgroundWorker.ReportProgress(50);

                string[] DBinfo = new string[4];
                DBinfo[0] = m_Database_URL;
                DBinfo[1] = m_Database_Name;
                DBinfo[2] = m_Database_Username;
                DBinfo[3] = m_Database_Password;

                IServerView l_ServerView = m_SoftwareController.ConnectToOPCServer(m_SelectedMachineName, m_SelectedServerID, DBinfo);
                UpdateServerTabList(l_ServerView.GetTabPage());
                
            }
            catch (Exception l_Ex)
            {
                ErrorLog l_ErrorLog = ErrorLog.GetInstance();
                l_ErrorLog.WriteToErrorLog(l_Ex.Message, l_Ex.StackTrace, "Error during OPC server connection");
                
                m_CurrentActionErrorCount++;

                DisplayMessageInReport(l_Ex.Message + "\n");
            }

            m_ConnectSrvBackgroundWorker.ReportProgress(100);
        }

        private void BackgroundWorker_ProgressChanged(object p_Sender, ProgressChangedEventArgs p_EvArgs)
        {
            m_CurrentActionProgressBar.Value = p_EvArgs.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object p_Sender, RunWorkerCompletedEventArgs p_EvArgs)
        {
            DisplayMessageInActionStatus(m_CurrentActionDescription + " finished");
            DisplayMessageInStateStatus(Convert.ToString(m_CurrentActionErrorCount) + " error(s)");

            m_IsActionRunning = false;
        }

#if NULL
        private void LocalSearchCheckBox_Click(object p_Sender, EventArgs p_EvArgs)
        {
            m_LocalSearchCheckBox.Checked = true;
            m_RemoteSearchCheckBox.Checked = false;
            
            m_RemoteMachineText.Enabled = false;
        }

        private void RemoteSearchCheckBox_Click(object p_Sender, EventArgs p_EvArgs)
        {
            m_RemoteSearchCheckBox.Checked = true;
            m_LocalSearchCheckBox.Checked = false;

            m_RemoteMachineText.Enabled = true;
        }
#endif

        private void QuitButton_Click(object p_Sender, EventArgs p_EvArgs)
        {
            m_SoftwareController.DisposeView();
            m_SoftwareController.DisconnectFromAllOPCServers();
        }

        private void SrvList_DoubleClick(object p_Sender, EventArgs p_EvArgs)
        {
            ConnectToOPCServer();
        }

        private void ConnectToMenuItem_Click(object p_Sender, EventArgs p_EvArgs)
        {
            ConnectToOPCServer();
        }

        private void LoadLogMenuItem_Click(object p_Sender, EventArgs p_EvArgs)
        {
            string l_ErrorLogContent;
            ErrorLog l_ErrorLog = ErrorLog.GetInstance();

            l_ErrorLogContent = l_ErrorLog.ReadErrorLog();

            if (l_ErrorLogContent == "")
            {
                DisplayMessageInReport("The error log is empty\n");
            }
            else
            {
                DisplayMessageInReport("\n\n" + l_ErrorLogContent);
            }
        }

        private void ClearLogMenuItem_Click(object p_Sender, EventArgs p_EvArgs)
        {
            ErrorLog l_ErrorLog = ErrorLog.GetInstance();
            l_ErrorLog.ClearErrorLog();
        }

        private void ClearEntriesMenuItem_Click(object p_Sender, EventArgs p_EvArgs)
        {
            m_ConnectionReport.Clear();
        }

        private void m_LocalSearchCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void m_RemoteSearchCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void m_ConnectionOptions_Enter(object sender, EventArgs e)
        {

        }

        private void m_LanguageMenu_Click(object sender, EventArgs e)
        {

        }

        private void m_TestConnectionButton_Click(object sender, EventArgs e)
        {
            String FullDBPath;

            SetDBConnection DB_con = new SetDBConnection();
            DB_con.ShowDialog();

            m_Database_URL = DB_con.getDBUrl();
            if (m_Database_URL == "")
            {
                m_Database_URL = "127.0.0.1";
            }

            m_Database_Name = DB_con.getDBName();
            if (m_Database_Name == "")
            {
                m_Database_Name = "OPCDB";
            }

            m_Database_Username = DB_con.getDBUsername();
            if (m_Database_Username == "")
            {
                m_Database_Username = "sa";
            }
            m_Database_Password = DB_con.getDBPassword();
            if (m_Database_Password == "")
            {
                m_Database_Password = "test123";
            }

            Database_con = new SqlConnection();
            FullDBPath = "server=" + m_Database_URL + ";database=" + m_Database_Name + ";uid=" + m_Database_Username + ";pwd=" + m_Database_Password;

            try
            {
                Database_con.ConnectionString = FullDBPath;
                Database_con.Open();

                SqlCommand com = new SqlCommand();
                com.Connection = Database_con;
                com.CommandType = CommandType.Text;
                com.CommandText = "select * from testconnection";
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    //DisplayMessageInReport(reader[0] + ", " + reader[1] + "\n");
                    System.Console.WriteLine(reader[0] + ", " + reader[1]);
                }

                reader.Close();
                Database_con.Close();
            }
            catch (Exception l_Ex)
            {
                ErrorLog l_ErrorLog = ErrorLog.GetInstance();
                l_ErrorLog.WriteToErrorLog(l_Ex.Message, l_Ex.StackTrace, "Error during OPC server connection");
                throw l_Ex;
            }

            DisplayMessageInReport("Connection to Database Successfully. FullPath: " + FullDBPath + "\n");
        }

        private void m_MainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void m_SrvList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
            WindowState = FormWindowState.Normal;
            this.Focus();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)    
            {
                notifyIcon1.Visible = true;    
                this.Hide();    
            }
        }

        private void FormMain_Closing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure to quit？", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
        

        private void SoftwareFrame_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}
