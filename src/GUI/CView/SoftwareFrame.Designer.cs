/*
 *  SoftwareFrame.Designer.cs
 *
 *  Copyright (C) 2010 Stephane Delapierre <stephane.delapierre@gmail.com>
 *
 *  Contributors : Julien Hannier <julien.hannier@gmail.com>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System.Windows.Forms;
namespace GUI.CView
{
    partial class SoftwareFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoftwareFrame));
            this.m_StatusBar = new System.Windows.Forms.StatusStrip();
            this.m_ActionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_CurrentActionProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.m_StateStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_MainMenu = new System.Windows.Forms.MenuStrip();
            this.m_FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FileMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.m_QuitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_LanguageMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ServersTabControl = new System.Windows.Forms.TabControl();
            this.m_ConnectionTab = new System.Windows.Forms.TabPage();
            this.m_TestConnectionButton = new System.Windows.Forms.Button();
            this.m_QuitButton = new System.Windows.Forms.Button();
            this.m_ListServersButton = new System.Windows.Forms.Button();
            this.m_ConnectionReport = new System.Windows.Forms.RichTextBox();
            this.m_RightMenuConnectionReport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ClearEntriesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_LoadLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ClearLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SrvList = new System.Windows.Forms.ListView();
            this.m_MachineColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_SrvNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_ProgIDColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_ClsidColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_RightMenuSrvList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ConnectToMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_ListSrvBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_ConnectSrvBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.m_StatusBar.SuspendLayout();
            this.m_MainMenu.SuspendLayout();
            this.m_ServersTabControl.SuspendLayout();
            this.m_ConnectionTab.SuspendLayout();
            this.m_RightMenuConnectionReport.SuspendLayout();
            this.m_RightMenuSrvList.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_StatusBar
            // 
            this.m_StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ActionStatusLabel,
            this.m_CurrentActionProgressBar,
            this.m_StateStatusLabel});
            this.m_StatusBar.Location = new System.Drawing.Point(0, 524);
            this.m_StatusBar.Name = "m_StatusBar";
            this.m_StatusBar.Size = new System.Drawing.Size(1116, 22);
            this.m_StatusBar.TabIndex = 0;
            this.m_StatusBar.Text = "StatusBar";
            // 
            // m_ActionStatusLabel
            // 
            this.m_ActionStatusLabel.Name = "m_ActionStatusLabel";
            this.m_ActionStatusLabel.Size = new System.Drawing.Size(399, 17);
            this.m_ActionStatusLabel.Spring = true;
            // 
            // m_CurrentActionProgressBar
            // 
            this.m_CurrentActionProgressBar.Name = "m_CurrentActionProgressBar";
            this.m_CurrentActionProgressBar.Size = new System.Drawing.Size(300, 16);
            // 
            // m_StateStatusLabel
            // 
            this.m_StateStatusLabel.Name = "m_StateStatusLabel";
            this.m_StateStatusLabel.Size = new System.Drawing.Size(399, 17);
            this.m_StateStatusLabel.Spring = true;
            // 
            // m_MainMenu
            // 
            this.m_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_FileMenu,
            this.m_LanguageMenu,
            this.m_HelpMenu});
            this.m_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.m_MainMenu.Name = "m_MainMenu";
            this.m_MainMenu.Size = new System.Drawing.Size(1116, 25);
            this.m_MainMenu.TabIndex = 1;
            this.m_MainMenu.Text = "MainMenu";
            this.m_MainMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.m_MainMenu_ItemClicked);
            // 
            // m_FileMenu
            // 
            this.m_FileMenu.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.m_FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_OpenMenuItem,
            this.m_SaveMenuItem,
            this.m_SaveAsMenuItem,
            this.m_FileMenuSeparator,
            this.m_QuitMenuItem});
            this.m_FileMenu.Name = "m_FileMenu";
            this.m_FileMenu.ShortcutKeyDisplayString = "";
            this.m_FileMenu.Size = new System.Drawing.Size(39, 21);
            this.m_FileMenu.Text = "File";
            // 
            // m_OpenMenuItem
            // 
            this.m_OpenMenuItem.Name = "m_OpenMenuItem";
            this.m_OpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.m_OpenMenuItem.Size = new System.Drawing.Size(155, 22);
            this.m_OpenMenuItem.Text = "Open";
            this.m_OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // m_SaveMenuItem
            // 
            this.m_SaveMenuItem.Name = "m_SaveMenuItem";
            this.m_SaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.m_SaveMenuItem.Size = new System.Drawing.Size(155, 22);
            this.m_SaveMenuItem.Text = "Save";
            this.m_SaveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // m_SaveAsMenuItem
            // 
            this.m_SaveAsMenuItem.Name = "m_SaveAsMenuItem";
            this.m_SaveAsMenuItem.Size = new System.Drawing.Size(155, 22);
            this.m_SaveAsMenuItem.Text = "Save As";
            this.m_SaveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // m_FileMenuSeparator
            // 
            this.m_FileMenuSeparator.Name = "m_FileMenuSeparator";
            this.m_FileMenuSeparator.Size = new System.Drawing.Size(152, 6);
            // 
            // m_QuitMenuItem
            // 
            this.m_QuitMenuItem.Name = "m_QuitMenuItem";
            this.m_QuitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.m_QuitMenuItem.Size = new System.Drawing.Size(155, 22);
            this.m_QuitMenuItem.Text = "Quit";
            this.m_QuitMenuItem.Click += new System.EventHandler(this.QuitMenuItem_Click);
            // 
            // m_LanguageMenu
            // 
            this.m_LanguageMenu.Checked = true;
            this.m_LanguageMenu.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.m_LanguageMenu.Name = "m_LanguageMenu";
            this.m_LanguageMenu.Size = new System.Drawing.Size(99, 21);
            this.m_LanguageMenu.Text = "Configuration";
            this.m_LanguageMenu.Click += new System.EventHandler(this.m_LanguageMenu_Click);
            // 
            // m_HelpMenu
            // 
            this.m_HelpMenu.Name = "m_HelpMenu";
            this.m_HelpMenu.Size = new System.Drawing.Size(47, 21);
            this.m_HelpMenu.Text = "Help";
            // 
            // m_ServersTabControl
            // 
            this.m_ServersTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ServersTabControl.Controls.Add(this.m_ConnectionTab);
            this.m_ServersTabControl.Location = new System.Drawing.Point(0, 25);
            this.m_ServersTabControl.Name = "m_ServersTabControl";
            this.m_ServersTabControl.SelectedIndex = 0;
            this.m_ServersTabControl.Size = new System.Drawing.Size(1116, 498);
            this.m_ServersTabControl.TabIndex = 2;
            // 
            // m_ConnectionTab
            // 
            this.m_ConnectionTab.Controls.Add(this.m_TestConnectionButton);
            this.m_ConnectionTab.Controls.Add(this.m_QuitButton);
            this.m_ConnectionTab.Controls.Add(this.m_ListServersButton);
            this.m_ConnectionTab.Controls.Add(this.m_ConnectionReport);
            this.m_ConnectionTab.Controls.Add(this.m_SrvList);
            this.m_ConnectionTab.Location = new System.Drawing.Point(4, 22);
            this.m_ConnectionTab.Name = "m_ConnectionTab";
            this.m_ConnectionTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_ConnectionTab.Size = new System.Drawing.Size(1108, 472);
            this.m_ConnectionTab.TabIndex = 0;
            this.m_ConnectionTab.Text = "Connection";
            this.m_ConnectionTab.UseVisualStyleBackColor = true;
            // 
            // m_TestConnectionButton
            // 
            this.m_TestConnectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_TestConnectionButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.m_TestConnectionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_TestConnectionButton.Location = new System.Drawing.Point(3, 444);
            this.m_TestConnectionButton.Name = "m_TestConnectionButton";
            this.m_TestConnectionButton.Size = new System.Drawing.Size(213, 28);
            this.m_TestConnectionButton.TabIndex = 5;
            this.m_TestConnectionButton.Text = "Configure Connection to Database";
            this.m_TestConnectionButton.UseVisualStyleBackColor = false;
            this.m_TestConnectionButton.Click += new System.EventHandler(this.m_TestConnectionButton_Click);
            // 
            // m_QuitButton
            // 
            this.m_QuitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_QuitButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.m_QuitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_QuitButton.Location = new System.Drawing.Point(905, 443);
            this.m_QuitButton.Name = "m_QuitButton";
            this.m_QuitButton.Size = new System.Drawing.Size(200, 28);
            this.m_QuitButton.TabIndex = 4;
            this.m_QuitButton.Text = "Quit Application";
            this.m_QuitButton.UseVisualStyleBackColor = false;
            this.m_QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // m_ListServersButton
            // 
            this.m_ListServersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_ListServersButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.m_ListServersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ListServersButton.Location = new System.Drawing.Point(239, 443);
            this.m_ListServersButton.Name = "m_ListServersButton";
            this.m_ListServersButton.Size = new System.Drawing.Size(200, 28);
            this.m_ListServersButton.TabIndex = 3;
            this.m_ListServersButton.Text = "List Local OPC Servers";
            this.m_ListServersButton.UseVisualStyleBackColor = false;
            this.m_ListServersButton.Click += new System.EventHandler(this.ListServersButton_Click);
            // 
            // m_ConnectionReport
            // 
            this.m_ConnectionReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ConnectionReport.ContextMenuStrip = this.m_RightMenuConnectionReport;
            this.m_ConnectionReport.Location = new System.Drawing.Point(3, 258);
            this.m_ConnectionReport.Name = "m_ConnectionReport";
            this.m_ConnectionReport.ReadOnly = true;
            this.m_ConnectionReport.Size = new System.Drawing.Size(1102, 179);
            this.m_ConnectionReport.TabIndex = 1;
            this.m_ConnectionReport.Text = "";
            // 
            // m_RightMenuConnectionReport
            // 
            this.m_RightMenuConnectionReport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ClearEntriesMenuItem,
            this.toolStripSeparator1,
            this.m_LoadLogMenuItem,
            this.m_ClearLogMenuItem});
            this.m_RightMenuConnectionReport.Name = "contextMenuStrip2";
            this.m_RightMenuConnectionReport.Size = new System.Drawing.Size(151, 76);
            // 
            // m_ClearEntriesMenuItem
            // 
            this.m_ClearEntriesMenuItem.Name = "m_ClearEntriesMenuItem";
            this.m_ClearEntriesMenuItem.Size = new System.Drawing.Size(150, 22);
            this.m_ClearEntriesMenuItem.Text = "Clear entries";
            this.m_ClearEntriesMenuItem.Click += new System.EventHandler(this.ClearEntriesMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // m_LoadLogMenuItem
            // 
            this.m_LoadLogMenuItem.Name = "m_LoadLogMenuItem";
            this.m_LoadLogMenuItem.Size = new System.Drawing.Size(150, 22);
            this.m_LoadLogMenuItem.Text = "Load log file";
            this.m_LoadLogMenuItem.Click += new System.EventHandler(this.LoadLogMenuItem_Click);
            // 
            // m_ClearLogMenuItem
            // 
            this.m_ClearLogMenuItem.Name = "m_ClearLogMenuItem";
            this.m_ClearLogMenuItem.Size = new System.Drawing.Size(150, 22);
            this.m_ClearLogMenuItem.Text = "Clear log file";
            this.m_ClearLogMenuItem.Click += new System.EventHandler(this.ClearLogMenuItem_Click);
            // 
            // m_SrvList
            // 
            this.m_SrvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_SrvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_MachineColumn,
            this.m_SrvNameColumn,
            this.m_ProgIDColumn,
            this.m_ClsidColumn});
            this.m_SrvList.ContextMenuStrip = this.m_RightMenuSrvList;
            this.m_SrvList.FullRowSelect = true;
            this.m_SrvList.GridLines = true;
            this.m_SrvList.HideSelection = false;
            this.m_SrvList.Location = new System.Drawing.Point(3, 3);
            this.m_SrvList.MultiSelect = false;
            this.m_SrvList.Name = "m_SrvList";
            this.m_SrvList.Size = new System.Drawing.Size(1102, 250);
            this.m_SrvList.TabIndex = 0;
            this.m_SrvList.UseCompatibleStateImageBehavior = false;
            this.m_SrvList.View = System.Windows.Forms.View.Details;
            this.m_SrvList.SelectedIndexChanged += new System.EventHandler(this.m_SrvList_SelectedIndexChanged);
            this.m_SrvList.DoubleClick += new System.EventHandler(this.SrvList_DoubleClick);
            // 
            // m_MachineColumn
            // 
            this.m_MachineColumn.Text = "Machine";
            this.m_MachineColumn.Width = 197;
            // 
            // m_SrvNameColumn
            // 
            this.m_SrvNameColumn.Text = "OPC Server Name";
            this.m_SrvNameColumn.Width = 295;
            // 
            // m_ProgIDColumn
            // 
            this.m_ProgIDColumn.Text = "Prog ID";
            this.m_ProgIDColumn.Width = 312;
            // 
            // m_ClsidColumn
            // 
            this.m_ClsidColumn.Text = "CLSID";
            this.m_ClsidColumn.Width = 290;
            // 
            // m_RightMenuSrvList
            // 
            this.m_RightMenuSrvList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ConnectToMenuItem});
            this.m_RightMenuSrvList.Name = "m_rightMenuSrvList";
            this.m_RightMenuSrvList.Size = new System.Drawing.Size(140, 26);
            // 
            // m_ConnectToMenuItem
            // 
            this.m_ConnectToMenuItem.Name = "m_ConnectToMenuItem";
            this.m_ConnectToMenuItem.Size = new System.Drawing.Size(139, 22);
            this.m_ConnectToMenuItem.Text = "Connect to";
            this.m_ConnectToMenuItem.Click += new System.EventHandler(this.ConnectToMenuItem_Click);
            // 
            // m_OpenFileDialog
            // 
            this.m_OpenFileDialog.Filter = "XML files (*.xml)|*.xml";
            this.m_OpenFileDialog.RestoreDirectory = true;
            // 
            // m_SaveFileDialog
            // 
            this.m_SaveFileDialog.DefaultExt = "xml";
            this.m_SaveFileDialog.Filter = "XML files (*.xml)|*.xml";
            this.m_SaveFileDialog.RestoreDirectory = true;
            // 
            // m_ListSrvBackgroundWorker
            // 
            this.m_ListSrvBackgroundWorker.WorkerReportsProgress = true;
            this.m_ListSrvBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ListSrvBackgroundWorker_DoWork);
            this.m_ListSrvBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.m_ListSrvBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // m_ConnectSrvBackgroundWorker
            // 
            this.m_ConnectSrvBackgroundWorker.WorkerReportsProgress = true;
            this.m_ConnectSrvBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ConnectSrvBackgroundWorker_DoWork);
            this.m_ConnectSrvBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.m_ConnectSrvBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "HyperOPClient";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // SoftwareFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1116, 546);
            this.Controls.Add(this.m_ServersTabControl);
            this.Controls.Add(this.m_StatusBar);
            this.Controls.Add(this.m_MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.m_MainMenu;
            this.MinimumSize = new System.Drawing.Size(1132, 584);
            this.Name = "SoftwareFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HyperOPClient V1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SoftwareFrame_FormClosed);
            this.Load += new System.EventHandler(this.SoftwareFrame_Load);
            this.SizeChanged += new System.EventHandler(this.SoftwareFrame_SizeChanged);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.FormClosing += new FormClosingEventHandler(this.FormMain_Closing);
            this.m_StatusBar.ResumeLayout(false);
            this.m_StatusBar.PerformLayout();
            this.m_MainMenu.ResumeLayout(false);
            this.m_MainMenu.PerformLayout();
            this.m_ServersTabControl.ResumeLayout(false);
            this.m_ConnectionTab.ResumeLayout(false);
            this.m_RightMenuConnectionReport.ResumeLayout(false);
            this.m_RightMenuSrvList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip m_StatusBar;
        private System.Windows.Forms.MenuStrip m_MainMenu;
        private System.Windows.Forms.ToolStripMenuItem m_FileMenu;
        private System.Windows.Forms.ToolStripMenuItem m_OpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SaveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_QuitMenuItem;
        private System.Windows.Forms.TabControl m_ServersTabControl;
        private System.Windows.Forms.TabPage m_ConnectionTab;
        private System.Windows.Forms.RichTextBox m_ConnectionReport;
        private System.Windows.Forms.ListView m_SrvList;
        private System.Windows.Forms.Button m_QuitButton;
        private System.Windows.Forms.Button m_ListServersButton;
        private System.Windows.Forms.ColumnHeader m_MachineColumn;
        private System.Windows.Forms.ColumnHeader m_SrvNameColumn;
        private System.Windows.Forms.ColumnHeader m_ProgIDColumn;
        private System.Windows.Forms.ColumnHeader m_ClsidColumn;
        private System.Windows.Forms.ToolStripStatusLabel m_ActionStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel m_StateStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar m_CurrentActionProgressBar;
        private System.Windows.Forms.ContextMenuStrip m_RightMenuSrvList;
        private System.Windows.Forms.ContextMenuStrip m_RightMenuConnectionReport;
        private System.Windows.Forms.ToolStripMenuItem m_ConnectToMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_LoadLogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ClearEntriesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ClearLogMenuItem;
        private System.Windows.Forms.OpenFileDialog m_OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog m_SaveFileDialog;
        private System.ComponentModel.BackgroundWorker m_ListSrvBackgroundWorker;
        private System.ComponentModel.BackgroundWorker m_ConnectSrvBackgroundWorker;
        private System.Windows.Forms.ToolStripMenuItem m_LanguageMenu;
        private System.Windows.Forms.ToolStripMenuItem m_HelpMenu;
        private System.Windows.Forms.ToolStripSeparator m_FileMenuSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button m_TestConnectionButton;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}
