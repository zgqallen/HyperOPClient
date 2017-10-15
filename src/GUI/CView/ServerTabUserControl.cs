
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using GUI.CController;
using GUI.CModel;
using GUI.CErrorLog;
using GUI.CUtility;

namespace GUI.CView
{
    partial class ServerTabUserControl : UserControl, IServerView
    {
        private TabPage m_ServerTabPage;
        private DataTable m_ItemsDataTable;

        private Dictionary<string, System.Drawing.Color> m_ItemsBackColorDictionary;
        private IServerController m_ServerController;

        public ServerTabUserControl(IServerController p_ServerController, string p_ServerName)
        {
            InitializeComponent();
            InitializeServerTabPage(p_ServerName);
            InitializeServerDataTable();

            m_ItemsTreeView.Nodes.Add(p_ServerName);
            m_ItemsBindingSource.DataSource = m_ItemsDataTable;


            m_ItemsBackColorDictionary = new Dictionary<string, System.Drawing.Color>();
            m_ServerController = p_ServerController;
        }

        private void InitializeServerTabPage(string p_ServerName)
        {
            m_ServerTabPage = new TabPage();

            this.Dock = DockStyle.Fill;

            m_ServerTabPage.Text = p_ServerName;
            m_ServerTabPage.Controls.Add(this);
            m_ServerTabPage.UseVisualStyleBackColor = true;
            m_ServerTabPage.Padding = new System.Windows.Forms.Padding(3);
        }

        private void InitializeServerDataTable()
        {
            m_ItemsDataTable = new DataTable("Items");

            DataColumn l_Column;
            l_Column = new DataColumn();
            l_Column.DataType = System.Type.GetType("System.String");
            l_Column.ColumnName = "ID";
            l_Column.ReadOnly = true;
            m_ItemsDataTable.Columns.Add(l_Column);
            DataColumn[] l_Keys = new DataColumn[1];
            l_Keys[0] = l_Column;
            m_ItemsDataTable.PrimaryKey = l_Keys;
            l_Column = new DataColumn();
            l_Column.DataType = System.Type.GetType("System.String");
            l_Column.ColumnName = "Type";
            l_Column.ReadOnly = true;
            m_ItemsDataTable.Columns.Add(l_Column);
            l_Column = new DataColumn();
            l_Column.DataType = System.Type.GetType("System.String");
            l_Column.ColumnName = "Value";
            l_Column.ReadOnly = false;
            m_ItemsDataTable.Columns.Add(l_Column);
            l_Column = new DataColumn();
            l_Column.DataType = System.Type.GetType("System.String");
            l_Column.ColumnName = "Quality";
            l_Column.ReadOnly = false;
            m_ItemsDataTable.Columns.Add(l_Column);
            l_Column = new DataColumn();
            l_Column.DataType = System.Type.GetType("System.String");
            l_Column.ColumnName = "Timestamp";
            l_Column.ReadOnly = false;
            m_ItemsDataTable.Columns.Add(l_Column);
        }

        public TabPage GetTabPage()
        {
            return m_ServerTabPage;
        }

        public void ServerModelChange(object p_Sender, ModelChangeEventArgs p_EvArgs)
        {
            if (p_EvArgs.EventType == ModelChangeEventType.ADD)
            {
                DataRow l_Row = m_ItemsDataTable.NewRow();
                l_Row["ID"] = p_EvArgs.ID;
                l_Row["Type"] = p_EvArgs.Type;
                l_Row["Value"] = p_EvArgs.Value;
                l_Row["Quality"] = p_EvArgs.Quality;
                l_Row["Timestamp"] = p_EvArgs.Timestamp;
                m_ItemsDataTable.Rows.Add(l_Row);

                System.Drawing.Color l_BackColor;
                if (p_EvArgs.Quality.Equals(OPCUtility.ITEM_QUALITY_BAD))
                {
                    l_BackColor = System.Drawing.Color.LightGray;
                }
                else
                {
                    l_BackColor = System.Drawing.Color.White;
                }
                m_ItemsBackColorDictionary.Add(p_EvArgs.ID, l_BackColor);

                m_ItemsCountLabelValue.Text = m_ItemsDataTable.Rows.Count.ToString();
                UpdateTreeView(p_EvArgs.ID);
            }
            else
            {
                DataRow l_Row = m_ItemsDataTable.Rows.Find(p_EvArgs.ID);
                if (l_Row != null)
                {
                    if (!l_Row.ItemArray[2].Equals(p_EvArgs.Value) ||
                        !l_Row.ItemArray[3].Equals(p_EvArgs.Quality) ||
                        !l_Row.ItemArray[4].Equals(p_EvArgs.Timestamp))
                    {
                        l_Row["Value"] = p_EvArgs.Value;
                        l_Row["Quality"] = p_EvArgs.Quality;
                        l_Row["Timestamp"] = p_EvArgs.Timestamp;

                        m_ItemsBackColorDictionary.Remove(p_EvArgs.ID);
                        m_ItemsBackColorDictionary.Add(p_EvArgs.ID, System.Drawing.Color.LightGreen);
                    }
                }
            }
        }

        private void UpdateTreeView(string p_ItemID)
        {
            string[] l_Path = p_ItemID.Split(OPCUtility.ITEM_PATH_SEPARATOR);
            TreeNode l_Parent = m_ItemsTreeView.Nodes[0];

            for (int i = 0; i < l_Path.Length - 1; ++i)
            {
                TreeNode l_Node = null;

                for (int j = 0; j < l_Parent.Nodes.Count; ++j)
                {
                    if (l_Parent.Nodes[j].Text.Equals(l_Path[i]))
                    {
                        l_Node = l_Parent.Nodes[j];
                        break;
                    }
                }

                if (l_Node == null)
                {
                    l_Node = new TreeNode(l_Path[i]);
                    l_Parent.Nodes.Add(l_Node);
                }
                l_Parent = l_Node;
            }

            m_ItemsTreeView.ExpandAll();
        }

         public int GetUpdateRate()
       {
           return m_UpdateRateTrackBar.Value * 1000;
        }

        private void QuitServerButton_Click(object p_Sender, EventArgs p_EvArgs)
        {
            if (m_ServerController.GetVeriteqOPCServer())
            {
                m_ServerController.DisconnectDatabase();
            }
            m_ServerController.Disconnect();
            m_ServerController.CloseView();

        }

        private void UpdateRateTrackBar_Scroll(object p_Sender, EventArgs p_EvArgs)
        {
            
        }

        private void CaseSensitiveCheckBox_Click(object p_Sender, EventArgs p_EvArgs)
        {

        }

        private void FilterTypeComboBox_TextChanged(object p_Sender, EventArgs p_EvArgs)
        {

        }

        private void FilterValueTextBox_TextChanged(object p_Sender, EventArgs p_EvArgs)
        {

        }

        private void ClearFilterButton_Click(object p_Sender, EventArgs p_EvArgs)
        {

        }

        private void AddItemsButton_Click(object p_Sender, EventArgs p_EvArgs)
        {

        }

        private void ItemsTreeView_NodeMouseClick(object p_Sender, TreeNodeMouseClickEventArgs p_EvArgs)
        {

        }

        private void ItemsDataGridView_DoubleClick(object p_Sender, EventArgs p_EvArgs)
        {

        }

        private void ItemsDataGridView_RowPrePaint(object p_Sender, DataGridViewRowPrePaintEventArgs p_EvArgs)
        {

        }

        private void WriteSelectedItemsMenuItem_Click(object p_Sender, EventArgs p_EvArgs)
        {
            if (m_ItemsDataGridView.SelectedRows.Count > 0)
            {
                SetValueFrame l_ValueFrame = new SetValueFrame();
                l_ValueFrame.ShowDialog();

                string l_ValueToWrite = l_ValueFrame.GetValue();
                if (!l_ValueToWrite.Equals(""))
                {
                    List<string> l_ItemsId = new List<string>();

                    foreach (DataGridViewRow l_Row in m_ItemsDataGridView.SelectedRows)
                    {
                        l_ItemsId.Add(l_Row.Cells[0].Value.ToString());
                    }

                    try
                    {
                        m_ServerController.WriteItems(l_ItemsId, l_ValueToWrite);
                    }
                    catch (Exception l_Ex)
                    {
                        ErrorLog l_ErrorLog = ErrorLog.GetInstance();
                        l_ErrorLog.WriteToErrorLog(l_Ex.Message, l_Ex.StackTrace, "Error during write items");

                        // TODO Display message
                    }
                }
            }
        }

        private void AcquitSelectedItemsMenuItem_Click(object p_Sender, EventArgs p_EvArgs)
        {
            foreach (DataGridViewRow l_Row in m_ItemsDataGridView.SelectedRows)
            {
                if (l_Row.DefaultCellStyle.BackColor == System.Drawing.Color.LightGreen)
                {
                    string l_ID = l_Row.Cells[0].Value.ToString();

                    m_ItemsBackColorDictionary.Remove(l_ID);
                    m_ItemsBackColorDictionary.Add(l_ID, System.Drawing.Color.White);

                    l_Row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private void m_ItemsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void m_ItemsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
