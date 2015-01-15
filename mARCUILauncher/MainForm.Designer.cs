namespace mARCUILauncher
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ServersgroupBox = new System.Windows.Forms.GroupBox();
            this.ServerslistView = new System.Windows.Forms.ListView();
            this.StatecolumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NamecolumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PathcolumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PortcolumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IpcolumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ServersListcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemovetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifytoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShutDowntoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StarttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unInstalltoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDetailstoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicatetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StategroupBox = new System.Windows.Forms.GroupBox();
            this.controlsgroupBox = new System.Windows.Forms.GroupBox();
            this.DRRgroupBox = new System.Windows.Forms.GroupBox();
            this.MSlabel = new System.Windows.Forms.Label();
            this.MStextBox = new System.Windows.Forms.TextBox();
            this.displayRefreshRatetrackBar = new System.Windows.Forms.TrackBar();
            this.displaycheckBox = new System.Windows.Forms.CheckBox();
            this.tasksgroupBox = new System.Windows.Forms.GroupBox();
            this.tasksrichTextBox = new System.Windows.Forms.RichTextBox();
            this.metricsgroupBox = new System.Windows.Forms.GroupBox();
            this.metricsrichTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAserverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyServerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ShutDownServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnInstallServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowServerDetailsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DuplicateServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoggroupBox = new System.Windows.Forms.GroupBox();
            this.LogrichTextBox = new System.Windows.Forms.RichTextBox();
            this.DisplayDetailsbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.duplicatebackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.AddServerbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ServersgroupBox.SuspendLayout();
            this.ServersListcontextMenuStrip.SuspendLayout();
            this.StategroupBox.SuspendLayout();
            this.controlsgroupBox.SuspendLayout();
            this.DRRgroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayRefreshRatetrackBar)).BeginInit();
            this.tasksgroupBox.SuspendLayout();
            this.metricsgroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.LoggroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ServersgroupBox
            // 
            this.ServersgroupBox.Controls.Add(this.ServerslistView);
            this.ServersgroupBox.Location = new System.Drawing.Point(2, 33);
            this.ServersgroupBox.Name = "ServersgroupBox";
            this.ServersgroupBox.Size = new System.Drawing.Size(489, 373);
            this.ServersgroupBox.TabIndex = 0;
            this.ServersgroupBox.TabStop = false;
            this.ServersgroupBox.Text = "Servers";
            // 
            // ServerslistView
            // 
            this.ServerslistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.StatecolumnHeader,
            this.NamecolumnHeader,
            this.PathcolumnHeader,
            this.PortcolumnHeader,
            this.IpcolumnHeader});
            this.ServerslistView.ContextMenuStrip = this.ServersListcontextMenuStrip;
            this.ServerslistView.FullRowSelect = true;
            this.ServerslistView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ServerslistView.Location = new System.Drawing.Point(5, 14);
            this.ServerslistView.MultiSelect = false;
            this.ServerslistView.Name = "ServerslistView";
            this.ServerslistView.Size = new System.Drawing.Size(480, 354);
            this.ServerslistView.TabIndex = 0;
            this.ServerslistView.UseCompatibleStateImageBehavior = false;
            this.ServerslistView.View = System.Windows.Forms.View.Details;
            this.ServerslistView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ServerslistView_MouseUp);
            // 
            // StatecolumnHeader
            // 
            this.StatecolumnHeader.Text = "state";
            // 
            // NamecolumnHeader
            // 
            this.NamecolumnHeader.Text = "Service name";
            this.NamecolumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PathcolumnHeader
            // 
            this.PathcolumnHeader.Text = "path";
            this.PathcolumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PortcolumnHeader
            // 
            this.PortcolumnHeader.Text = "port";
            this.PortcolumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PortcolumnHeader.Width = 77;
            // 
            // IpcolumnHeader
            // 
            this.IpcolumnHeader.Text = "ip";
            this.IpcolumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ServersListcontextMenuStrip
            // 
            this.ServersListcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddtoolStripMenuItem,
            this.RemovetoolStripMenuItem,
            this.ModifytoolStripMenuItem,
            this.ShutDowntoolStripMenuItem,
            this.StarttoolStripMenuItem,
            this.unInstalltoolStripMenuItem,
            this.showDetailstoolStripMenuItem,
            this.duplicatetoolStripMenuItem});
            this.ServersListcontextMenuStrip.Name = "ServersListcontextMenuStrip";
            this.ServersListcontextMenuStrip.Size = new System.Drawing.Size(142, 180);
            this.ServersListcontextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ServersListcontextMenuStrip_ItemClicked);
            this.ServersListcontextMenuStrip.Click += new System.EventHandler(this.ServersListcontextMenuStrip_Click);
            this.ServersListcontextMenuStrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ServersListcontextMenuStrip_MouseMove);
            this.ServersListcontextMenuStrip.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ServersListcontextMenuStrip_MouseUp);
            // 
            // AddtoolStripMenuItem
            // 
            this.AddtoolStripMenuItem.Name = "AddtoolStripMenuItem";
            this.AddtoolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.AddtoolStripMenuItem.Text = "Add";
            // 
            // RemovetoolStripMenuItem
            // 
            this.RemovetoolStripMenuItem.Name = "RemovetoolStripMenuItem";
            this.RemovetoolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.RemovetoolStripMenuItem.Text = "Remove";
            // 
            // ModifytoolStripMenuItem
            // 
            this.ModifytoolStripMenuItem.Name = "ModifytoolStripMenuItem";
            this.ModifytoolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.ModifytoolStripMenuItem.Text = "Modify";
            // 
            // ShutDowntoolStripMenuItem
            // 
            this.ShutDowntoolStripMenuItem.Name = "ShutDowntoolStripMenuItem";
            this.ShutDowntoolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.ShutDowntoolStripMenuItem.Text = "ShutDown";
            // 
            // StarttoolStripMenuItem
            // 
            this.StarttoolStripMenuItem.Name = "StarttoolStripMenuItem";
            this.StarttoolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.StarttoolStripMenuItem.Text = "Start";
            // 
            // unInstalltoolStripMenuItem
            // 
            this.unInstalltoolStripMenuItem.Name = "unInstalltoolStripMenuItem";
            this.unInstalltoolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.unInstalltoolStripMenuItem.Text = "UnInstall";
            // 
            // showDetailstoolStripMenuItem
            // 
            this.showDetailstoolStripMenuItem.Name = "showDetailstoolStripMenuItem";
            this.showDetailstoolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.showDetailstoolStripMenuItem.Text = "Show Details";
            // 
            // duplicatetoolStripMenuItem
            // 
            this.duplicatetoolStripMenuItem.Name = "duplicatetoolStripMenuItem";
            this.duplicatetoolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.duplicatetoolStripMenuItem.Text = "Duplicate";
            // 
            // StategroupBox
            // 
            this.StategroupBox.Controls.Add(this.controlsgroupBox);
            this.StategroupBox.Controls.Add(this.tasksgroupBox);
            this.StategroupBox.Controls.Add(this.metricsgroupBox);
            this.StategroupBox.Location = new System.Drawing.Point(498, 13);
            this.StategroupBox.Name = "StategroupBox";
            this.StategroupBox.Size = new System.Drawing.Size(554, 393);
            this.StategroupBox.TabIndex = 1;
            this.StategroupBox.TabStop = false;
            this.StategroupBox.Text = "State";
            // 
            // controlsgroupBox
            // 
            this.controlsgroupBox.Controls.Add(this.DRRgroupBox);
            this.controlsgroupBox.Controls.Add(this.displaycheckBox);
            this.controlsgroupBox.Location = new System.Drawing.Point(11, 14);
            this.controlsgroupBox.Name = "controlsgroupBox";
            this.controlsgroupBox.Size = new System.Drawing.Size(537, 82);
            this.controlsgroupBox.TabIndex = 2;
            this.controlsgroupBox.TabStop = false;
            this.controlsgroupBox.Text = "Controls";
            // 
            // DRRgroupBox
            // 
            this.DRRgroupBox.Controls.Add(this.MSlabel);
            this.DRRgroupBox.Controls.Add(this.MStextBox);
            this.DRRgroupBox.Controls.Add(this.displayRefreshRatetrackBar);
            this.DRRgroupBox.Location = new System.Drawing.Point(156, 12);
            this.DRRgroupBox.Name = "DRRgroupBox";
            this.DRRgroupBox.Size = new System.Drawing.Size(375, 67);
            this.DRRgroupBox.TabIndex = 2;
            this.DRRgroupBox.TabStop = false;
            this.DRRgroupBox.Text = "Display Refresh Rate";
            // 
            // MSlabel
            // 
            this.MSlabel.AutoSize = true;
            this.MSlabel.Location = new System.Drawing.Point(317, 32);
            this.MSlabel.Name = "MSlabel";
            this.MSlabel.Size = new System.Drawing.Size(20, 13);
            this.MSlabel.TabIndex = 3;
            this.MSlabel.Text = "ms";
            // 
            // MStextBox
            // 
            this.MStextBox.Location = new System.Drawing.Point(216, 29);
            this.MStextBox.Name = "MStextBox";
            this.MStextBox.Size = new System.Drawing.Size(95, 20);
            this.MStextBox.TabIndex = 2;
            this.MStextBox.Text = "100";
            // 
            // displayRefreshRatetrackBar
            // 
            this.displayRefreshRatetrackBar.Location = new System.Drawing.Point(10, 19);
            this.displayRefreshRatetrackBar.Maximum = 10000;
            this.displayRefreshRatetrackBar.Minimum = 10;
            this.displayRefreshRatetrackBar.Name = "displayRefreshRatetrackBar";
            this.displayRefreshRatetrackBar.Size = new System.Drawing.Size(200, 45);
            this.displayRefreshRatetrackBar.TabIndex = 1;
            this.displayRefreshRatetrackBar.TickFrequency = 1000;
            this.displayRefreshRatetrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.displayRefreshRatetrackBar.Value = 100;
            this.displayRefreshRatetrackBar.ValueChanged += new System.EventHandler(this.displayRefreshRatetrackBar_ValueChanged);
            this.displayRefreshRatetrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.displayRefreshRatetrackBar_MouseDown);
            this.displayRefreshRatetrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.displayRefreshRatetrackBar_MouseUp);
            // 
            // displaycheckBox
            // 
            this.displaycheckBox.AutoSize = true;
            this.displaycheckBox.Location = new System.Drawing.Point(19, 15);
            this.displaycheckBox.Name = "displaycheckBox";
            this.displaycheckBox.Size = new System.Drawing.Size(60, 17);
            this.displaycheckBox.TabIndex = 0;
            this.displaycheckBox.Text = "Display";
            this.displaycheckBox.UseVisualStyleBackColor = true;
            this.displaycheckBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.displaycheckBox_MouseUp);
            // 
            // tasksgroupBox
            // 
            this.tasksgroupBox.Controls.Add(this.tasksrichTextBox);
            this.tasksgroupBox.Location = new System.Drawing.Point(6, 232);
            this.tasksgroupBox.Name = "tasksgroupBox";
            this.tasksgroupBox.Size = new System.Drawing.Size(545, 158);
            this.tasksgroupBox.TabIndex = 1;
            this.tasksgroupBox.TabStop = false;
            this.tasksgroupBox.Text = "Tasks";
            // 
            // tasksrichTextBox
            // 
            this.tasksrichTextBox.Location = new System.Drawing.Point(3, 14);
            this.tasksrichTextBox.Name = "tasksrichTextBox";
            this.tasksrichTextBox.Size = new System.Drawing.Size(539, 140);
            this.tasksrichTextBox.TabIndex = 0;
            this.tasksrichTextBox.Text = "";
            // 
            // metricsgroupBox
            // 
            this.metricsgroupBox.Controls.Add(this.metricsrichTextBox);
            this.metricsgroupBox.Location = new System.Drawing.Point(7, 98);
            this.metricsgroupBox.Name = "metricsgroupBox";
            this.metricsgroupBox.Size = new System.Drawing.Size(547, 132);
            this.metricsgroupBox.TabIndex = 0;
            this.metricsgroupBox.TabStop = false;
            this.metricsgroupBox.Text = "Metrics";
            // 
            // metricsrichTextBox
            // 
            this.metricsrichTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.metricsrichTextBox.Location = new System.Drawing.Point(3, 13);
            this.metricsrichTextBox.Name = "metricsrichTextBox";
            this.metricsrichTextBox.ReadOnly = true;
            this.metricsrichTextBox.Size = new System.Drawing.Size(540, 114);
            this.metricsrichTextBox.TabIndex = 0;
            this.metricsrichTextBox.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigurationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1054, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ConfigurationToolStripMenuItem
            // 
            this.ConfigurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAserverToolStripMenuItem,
            this.RemoveServerToolStripMenuItem,
            this.ModifyServerToolStripMenuItem1,
            this.ShutDownServerToolStripMenuItem,
            this.StartServerToolStripMenuItem,
            this.UnInstallServerToolStripMenuItem,
            this.ShowServerDetailsToolStripMenuItem1,
            this.DuplicateServerToolStripMenuItem});
            this.ConfigurationToolStripMenuItem.Name = "ConfigurationToolStripMenuItem";
            this.ConfigurationToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.ConfigurationToolStripMenuItem.Text = "Serveurs";
            this.ConfigurationToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ConfigurationToolStripMenuItem_DropDownItemClicked);
            this.ConfigurationToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ConfigurationToolStripMenuItem_MouseUp);
            // 
            // addAserverToolStripMenuItem
            // 
            this.addAserverToolStripMenuItem.Name = "addAserverToolStripMenuItem";
            this.addAserverToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.addAserverToolStripMenuItem.Text = "Add";
            this.addAserverToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // RemoveServerToolStripMenuItem
            // 
            this.RemoveServerToolStripMenuItem.Name = "RemoveServerToolStripMenuItem";
            this.RemoveServerToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.RemoveServerToolStripMenuItem.Text = "Remove";
            // 
            // ModifyServerToolStripMenuItem1
            // 
            this.ModifyServerToolStripMenuItem1.Name = "ModifyServerToolStripMenuItem1";
            this.ModifyServerToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.ModifyServerToolStripMenuItem1.Text = "Modify";
            // 
            // ShutDownServerToolStripMenuItem
            // 
            this.ShutDownServerToolStripMenuItem.Name = "ShutDownServerToolStripMenuItem";
            this.ShutDownServerToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.ShutDownServerToolStripMenuItem.Text = "ShutDown";
            // 
            // StartServerToolStripMenuItem
            // 
            this.StartServerToolStripMenuItem.Name = "StartServerToolStripMenuItem";
            this.StartServerToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.StartServerToolStripMenuItem.Text = "Start";
            // 
            // UnInstallServerToolStripMenuItem
            // 
            this.UnInstallServerToolStripMenuItem.Name = "UnInstallServerToolStripMenuItem";
            this.UnInstallServerToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.UnInstallServerToolStripMenuItem.Text = "UnInstall";
            // 
            // ShowServerDetailsToolStripMenuItem1
            // 
            this.ShowServerDetailsToolStripMenuItem1.Name = "ShowServerDetailsToolStripMenuItem1";
            this.ShowServerDetailsToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.ShowServerDetailsToolStripMenuItem1.Text = "Show Details";
            // 
            // DuplicateServerToolStripMenuItem
            // 
            this.DuplicateServerToolStripMenuItem.Name = "DuplicateServerToolStripMenuItem";
            this.DuplicateServerToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.DuplicateServerToolStripMenuItem.Text = "Duplicate";
            // 
            // LoggroupBox
            // 
            this.LoggroupBox.Controls.Add(this.LogrichTextBox);
            this.LoggroupBox.Location = new System.Drawing.Point(2, 407);
            this.LoggroupBox.Name = "LoggroupBox";
            this.LoggroupBox.Size = new System.Drawing.Size(1050, 163);
            this.LoggroupBox.TabIndex = 3;
            this.LoggroupBox.TabStop = false;
            this.LoggroupBox.Text = "Log";
            // 
            // LogrichTextBox
            // 
            this.LogrichTextBox.Location = new System.Drawing.Point(3, 15);
            this.LogrichTextBox.Name = "LogrichTextBox";
            this.LogrichTextBox.Size = new System.Drawing.Size(1043, 145);
            this.LogrichTextBox.TabIndex = 0;
            this.LogrichTextBox.Text = "";
            // 
            // DisplayDetailsbackgroundWorker
            // 
            this.DisplayDetailsbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DisplayDetailsbackgroundWorker_DoWork);
            this.DisplayDetailsbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DisplayDeatilsbackgroundWorker_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // duplicatebackgroundWorker
            // 
            this.duplicatebackgroundWorker.WorkerReportsProgress = true;
            this.duplicatebackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.duplicatebackgroundWorker_DoWork);
            this.duplicatebackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.duplicatebackgroundWorker_ProgressChanged);
            this.duplicatebackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.duplicatebackgroundWorker_RunWorkerCompleted);
            // 
            // AddServerbackgroundWorker
            // 
            this.AddServerbackgroundWorker.WorkerReportsProgress = true;
            this.AddServerbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AddServerbackgroundWorker_DoWork);
            this.AddServerbackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AddServerbackgroundWorker_ProgressChanged);
            this.AddServerbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AddServerbackgroundWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 571);
            this.Controls.Add(this.LoggroupBox);
            this.Controls.Add(this.StategroupBox);
            this.Controls.Add(this.ServersgroupBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "mARC Servers UI Launcher";
            this.ServersgroupBox.ResumeLayout(false);
            this.ServersListcontextMenuStrip.ResumeLayout(false);
            this.StategroupBox.ResumeLayout(false);
            this.controlsgroupBox.ResumeLayout(false);
            this.controlsgroupBox.PerformLayout();
            this.DRRgroupBox.ResumeLayout(false);
            this.DRRgroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayRefreshRatetrackBar)).EndInit();
            this.tasksgroupBox.ResumeLayout(false);
            this.metricsgroupBox.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.LoggroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ServersgroupBox;
        private System.Windows.Forms.GroupBox StategroupBox;
        private System.Windows.Forms.GroupBox tasksgroupBox;
        private System.Windows.Forms.GroupBox metricsgroupBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAserverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveServerToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ServersListcontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem AddtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemovetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifytoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShutDowntoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StarttoolStripMenuItem;
        public System.Windows.Forms.ListView ServerslistView;
        private System.Windows.Forms.ColumnHeader StatecolumnHeader;
        private System.Windows.Forms.ColumnHeader NamecolumnHeader;
        private System.Windows.Forms.ColumnHeader PathcolumnHeader;
        private System.Windows.Forms.ColumnHeader PortcolumnHeader;
        private System.Windows.Forms.ColumnHeader IpcolumnHeader;
        private System.Windows.Forms.GroupBox LoggroupBox;
        public System.Windows.Forms.RichTextBox LogrichTextBox;
        private System.Windows.Forms.ToolStripMenuItem unInstalltoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDetailstoolStripMenuItem;
        private System.ComponentModel.BackgroundWorker DisplayDetailsbackgroundWorker;
        private System.Windows.Forms.GroupBox controlsgroupBox;
        private System.Windows.Forms.RichTextBox tasksrichTextBox;
        private System.Windows.Forms.RichTextBox metricsrichTextBox;
        private System.Windows.Forms.CheckBox displaycheckBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox DRRgroupBox;
        private System.Windows.Forms.Label MSlabel;
        private System.Windows.Forms.TextBox MStextBox;
        private System.Windows.Forms.TrackBar displayRefreshRatetrackBar;
        private System.Windows.Forms.ToolStripMenuItem duplicatetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyServerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ShutDownServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UnInstallServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowServerDetailsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem DuplicateServerToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker duplicatebackgroundWorker;
        private System.ComponentModel.BackgroundWorker AddServerbackgroundWorker;
    }
}

