/*
 * Copyright (C) 2015 Marvinbot S.A.S
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Configuration.Install;
using System.IO;

namespace mARCUILauncher
{

    public partial class MainForm : Form
    {
        static public mARC.Connector connector = new mARC.Connector(false);


        public MainForm()
        {
            InitializeComponent();
            ServerslistView.SmallImageList = new ImageList();
            ServerslistView.SmallImageList.Images.AddRange(new Image[] { Properties.Resources.black_led, Properties.Resources.redled, Properties.Resources.greenled });
            timer1.Interval = int.Parse(MStextBox.Text);
            timer1.Enabled = false;
        }

        private void ServerslistView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point p = new Point();
                p.X = this.Location.X + e.Location.X + ServerslistView.Location.X;
                p.Y = this.Location.Y + e.Location.Y + ServerslistView.Location.Y;

                ServersListcontextMenuStrip.Show(p);
                ServersListcontextMenuStrip.Enabled = true;

            }
        }

        // on agit sur le menu contextuel sur la liste des serveurs
        private void ServersListcontextMenuStrip_MouseUp(object sender, MouseEventArgs e)
        {

        }


        public Boolean ScanPort(string port, string ip)
        {
            Socket s = null;
            try
            {
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                s.Connect(new IPEndPoint(IPAddress.Parse(ip), int.Parse(port)));
            }
            catch (Exception eee)
            {
                LogrichTextBox.AppendText("mARC server is not reachable. \n");
                return false;
            }


            if (s != null && s.Connected)
            {
                connector.Lock();
                connector.Connect();
                if (!connector.isConnected)
                {
                    LogrichTextBox.AppendText("port is connected. No mARC server is connected to this port. \n");
                    return false;
                }
                connector.disConnect();

                return true;
            }
            else
            {
                LogrichTextBox.AppendText("port is not in use. mARC server is not reachable. \n");
                return false;
            }

        }

        private void ServersListcontextMenuStrip_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void ServersListcontextMenuStrip_Click(object sender, EventArgs e)
        {

        }

        public void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists, if not, create it.
            string s;
            if (Directory.Exists(target.FullName) == false)
            {
                s = "Creating directory '" + target.FullName + "\n";
                duplicatebackgroundWorker.ReportProgress(0, s);
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                s = "Copying : "+ target.FullName+"\\"+ fi.Name+" \n";

                duplicatebackgroundWorker.ReportProgress(0, s);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void DuplicateServer(ListViewItem ite)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(d.SelectedPath))
                {
                    LogrichTextBox.AppendText("No directory selected. Aborting ...");
                    return;
                }

                ServiceController service = null;
                ServiceControllerStatus status = ServiceControllerStatus.Stopped;

                if (GetServiceStatus(ite.SubItems[1].Text, ite.SubItems[3].Text, ite.SubItems[4].Text, ref status, ref service))
                {
                    if (service.Status == ServiceControllerStatus.Running)
                    {
                        LogrichTextBox.AppendText("Stopping service to duplicate...");


                        try
                        {
                            service.Stop();
                            service.WaitForStatus(ServiceControllerStatus.Stopped);
                        }
                        catch (InvalidOperationException exception)
                        {
                            LogrichTextBox.AppendText("could not stop the service. Exception Occured : " + exception.InnerException.Message + "\n");
                            return;
                        }


                        LogrichTextBox.AppendText("OK \n");
                    }

                    if (!Directory.Exists(d.SelectedPath))
                    {
                        Directory.CreateDirectory(d.SelectedPath);
                    }

                    DirectoryInfo diSource = new DirectoryInfo(Path.GetDirectoryName(ite.SubItems[2].Text) );
                    DirectoryInfo diTarget = new DirectoryInfo(d.SelectedPath);

                    if ( duplicatebackgroundWorker.IsBusy )
                    {
                        return;
                    }
                    DirectoryInfo[] p = {diSource, diTarget};
                    duplicatebackgroundWorker.RunWorkerAsync(p);
                    
                }

            }

        }

        private void AddServer()
        {
            newServerForm serverForm = new newServerForm();
            serverForm.textBoxIp.Enabled = true;
            serverForm.textBoxPort.Enabled = false;
            serverForm.port = "";
        Retry:


            DialogResult r = serverForm.ShowDialog();

            if (r == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(serverForm.path) && !string.IsNullOrEmpty(serverForm.name) )
                {
                    if (string.IsNullOrEmpty(serverForm.port))
                    {
                        DirectoryInfo targetDir = new DirectoryInfo(serverForm.path);
                        string path = targetDir.FullName;
                        if (!File.Exists(path))
                        {
                            string m = "Specified path '" + path + "' is not a file. Aborting. \n";
                            AppendColoredText(LogrichTextBox, Color.Red, m);
                            return;
                        }
                        string extension = targetDir.Extension;
                        int extensionPos = path.LastIndexOf(extension);
                        int l = extensionPos - (path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                        string prgName = path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1, l);
                        string directory = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
                        string cfgPrg = directory + Path.DirectorySeparatorChar + prgName + ".cfg";
                        FileInfo prg = new FileInfo(cfgPrg);
                        if (!File.Exists(prg.FullName))
                        {
                            string m = "Did not find a config file '" + prg.FullName + "'. Please specify port. \n";
                            AppendColoredText(LogrichTextBox, Color.Black, m);
                            //AddServerbackgroundWorker.ReportProgress(0, m);
                            serverForm.textBoxPort.Enabled = false;
                            serverForm.port = "1254";
                            goto testIp;
                        }
                        else
                        {// on ouvre le fichier cfg pour trouver le port
                            string m = "Found a config file '" + prg.FullName + "'. Reading it to identify Port. \n";
                            AppendColoredText(LogrichTextBox, Color.Black, m);
                            //AddServerbackgroundWorker.ReportProgress(0, m);
                            StreamReader sReader = prg.OpenText();
                            string s = "";
                            while ((s = sReader.ReadLine()) != null)
                            {
                                s = s.ToLower();
                                if (s.Contains("port =") && !s.Contains("cluster"))
                                {
                                    int equalPos = s.LastIndexOf("=");
                                    serverForm.port = s.Substring(equalPos + 1);
                                    serverForm.port = serverForm.port.TrimEnd(' ');
                                    serverForm.port = serverForm.port.TrimStart(' ');
                                    m = "Identified Server Port to be '" + serverForm.port + "' \n";
                                    AppendColoredText(LogrichTextBox, Color.Black, m);
                                    // AddServerbackgroundWorker.ReportProgress(0, m);
                                    break;
                                }
                            }
                        }
                    }
                testIp:
                    if (string.IsNullOrEmpty(serverForm.Ip))
                    {
                        string m = "server IP is not specified. Please Specify it.\n";
                        AppendColoredText(LogrichTextBox, Color.Red, m);
                        goto Retry;
                    }
                        // on scanne le port
                        int index = 0;
                        if (ScanPort(serverForm.Ip, serverForm.port))
                        {

                            connector.Lock();

                            if (connector.isConnected)
                            {
                                connector.disConnect();
                            }

                            connector.Port = int.Parse(serverForm.port);
                            connector.IP = serverForm.Ip;
                            connector.Connect();
                            if (connector.isConnected)
                            {
                                string s = "mARC server found and responding: '" + connector.Received + "' \n";
                                AppendColoredText(LogrichTextBox, Color.Black, s);
                                //AddServerbackgroundWorker.ReportProgress(0, s);
                                //LogrichTextBox.AppendText("mARC server found and responding: '" + connector.Received + "' \n");
                                index = 2;
                                connector.disConnect();
                            }
                            else
                            {
                                string s = "port is in use and mARC Server did not respond. \n";
                                AppendColoredText(LogrichTextBox, Color.Black, s);
                                //AddServerbackgroundWorker.ReportProgress(0, s);
                                //LogrichTextBox.AppendText("port is in use and mARC Server did not respond. \n");
                                ServiceControllerStatus status = ServiceControllerStatus.Stopped;
                                ServiceController service = null;
                                if (GetServiceStatus(serverForm.name, serverForm.port, serverForm.Ip, ref status, ref service))
                                {
                                    if (status == ServiceControllerStatus.Stopped)
                                        index = 1;
                                    if (status == ServiceControllerStatus.Running)
                                        index = 2;
                                }
                            }
                            connector.UnLock();

                        }
                        ListViewItem ite = new ListViewItem("", index);
                        ite.SubItems.Add(serverForm.name);
                        ite.SubItems.Add(serverForm.path);
                        ite.SubItems.Add(serverForm.port);
                        ite.SubItems.Add(serverForm.Ip);
                        ServerslistView.Items.Add(ite);
                        Program.Config.Add(new Misc.Config(serverForm.port, serverForm.name, serverForm.path, serverForm.Ip));
                        ServerslistView.Refresh();
                        Program.SaveConfig();
                    }

                }

        }

        public void ScanServerCfgFile(string pathCfg, ref string port)
        {

            DirectoryInfo targetDir = new DirectoryInfo(pathCfg);
            string path = targetDir.FullName;
            if (!File.Exists(path))
            {
                string m = "Specified path '" + path + "' is not a file. Aborting. \n";
                AppendColoredText(LogrichTextBox, Color.Red, m);
                port = "1254";
                return;
            }
            string extension = targetDir.Extension;
            int extensionPos = path.LastIndexOf(extension);
            int l = extensionPos - (path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            string prgName = path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1, l);
            string directory = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
            string cfgPrg = directory + Path.DirectorySeparatorChar + prgName + ".cfg";
            FileInfo prg = new FileInfo(cfgPrg);
            if (!File.Exists(prg.FullName))
            {
                string m = "Did not find a config file '" + prg.FullName + "'. \n";
                AppendColoredText(LogrichTextBox, Color.Black, m);
                port = "1254";
                return;
            }
            else
            {// on ouvre le fichier cfg pour trouver le port
                string m = "Found a config file '" + prg.FullName + "'. Reading it to identify Port. \n";
                AppendColoredText(LogrichTextBox, Color.Black, m);
                //AddServerbackgroundWorker.ReportProgress(0, m);
                StreamReader sReader = prg.OpenText();
                string s = "";
                while ((s = sReader.ReadLine()) != null)
                {
                    s = s.ToLower();
                    if (s.Contains("port =") && !s.Contains("cluster"))
                    {
                        int equalPos = s.LastIndexOf("=");
                        port = s.Substring(equalPos + 1);
                        port = port.TrimEnd(' ');
                        port = port.TrimStart(' ');
                        m = "Identified Server Port to be '" + port + "' \n";
                        AppendColoredText(LogrichTextBox, Color.Black, m);
                        // AddServerbackgroundWorker.ReportProgress(0, m);
                        break;
                    }
                }
            }


        }
        private void RemoveServer(ListViewItem ite)
        {
            if (ServerslistView.SelectedItems == null)
            {
                LogrichTextBox.AppendText(" No server selected. Nothing to Remove \n");
                return;
            }
            if (ServerslistView.Items.Count == 0)
            {
                LogrichTextBox.AppendText(" No server. Nothing to Remove \n");
                return;
            }
            
            for (int i = 0; i < ServerslistView.SelectedIndices.Count; i++ )
            {
                Program.Config.RemoveAt(ServerslistView.SelectedIndices[i] );
                ServerslistView.Items.Remove(ite);
                ServerslistView.Refresh();
                Program.SaveConfig();
                LogrichTextBox.AppendText(" Removing server '"+ite.SubItems[1].Text+"' \n");
                ShutDownAServiceFromName(ite.SubItems[1].Text);
            }
            return;
        }

        private void ModifyServer(ListViewItem ite)
        {
            
            if (ServerslistView.SelectedItems == null)
            {
                LogrichTextBox.AppendText(" No server selected. Nothing to Modify \n");
                return;
            }
            newServerForm serverForm = new newServerForm();
            serverForm.name = ite.SubItems[1].Text;
            serverForm.path = ite.SubItems[2].Text;
            serverForm.port = ite.SubItems[3].Text;
            serverForm.Ip = ite.SubItems[4].Text;

            serverForm.textBoxIp.Enabled = false;
            serverForm.textBoxPort.Enabled = false;

            serverForm.textBox3.Text = serverForm.name;
            serverForm.textBox1.Text = serverForm.path;
            serverForm.textBoxPort.Text = serverForm.port;
            serverForm.textBoxIp.Text = serverForm.Ip;

            if (serverForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(serverForm.path) && !string.IsNullOrEmpty(serverForm.port))
                {
                    if (!string.IsNullOrEmpty(serverForm.Ip))
                    {
                        // on scanne le port
                        int index = 0;
                        if (ScanPort(serverForm.Ip, serverForm.port))
                        {
                            LogrichTextBox.AppendText("the required Port is already in use. Aborting...\n");
                            return;
                        }
                        else
                        {
                            // on regarde le statut du service
                            ServiceControllerStatus status = ServiceControllerStatus.Stopped;
                            ServiceController service = null;
                            if (!GetServiceStatus(serverForm.name, serverForm.port, serverForm.Ip, ref status, ref service))
                            {
                                if (status == ServiceControllerStatus.Running)
                                    index = 2;
                                if (status == ServiceControllerStatus.Stopped)
                                    index = 1;

                            }
                            else
                            {
                                LogrichTextBox.AppendText("A service with the same name is already installed. Aborting...\n");
                                return;
                            }
                        }

                        // le port a changé ?
                        if (!serverForm.port.Equals(ite.SubItems[3].Text) || !serverForm.name.Equals(ite.SubItems[1].Text))
                        {
                            ModifyService(ite.SubItems[1].Text, serverForm.name, ite.SubItems[3].Text, serverForm.port, ite.SubItems[2].Text, serverForm.path, ite.SubItems[4].Text, ref index);
                        }
                        ite.ImageIndex = index;
                        ite.SubItems[1].Text = serverForm.name;
                        ite.SubItems[2].Text = serverForm.path;
                        ite.SubItems[3].Text = serverForm.port;
                        ServerslistView.Refresh();
                        Program.Config[ite.Index].Port = serverForm.port;
                        Program.Config[ite.Index].Name = serverForm.name;
                        Program.Config[ite.Index].Path = serverForm.path;

                        Program.SaveConfig();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No Ip address provided");
                        return;
                    }

                }
                ServersListcontextMenuStrip.Hide();
            }
        }

        private void StartServer(ListViewItem ite)
        {
            if (ServerslistView.SelectedItems == null || ServerslistView.SelectedItems.Count == 0)
            {
                LogrichTextBox.AppendText("no server selected. select one. \n");
                return;
            }
            //on scanne son fichier cfg pour verifier que le port specifie est bien celui noté dans le manager
            // si c'est pas le meme on maj la config
            // et on lance sur le port specifie dans le cfg
            string port = "";
            ScanServerCfgFile(ite.SubItems[2].Text, ref port);
            if ( !port.Equals(ite.SubItems[3].Text) )
            {
                ite.SubItems[3].Text = port;
                ServerslistView.Update();
                Program.SaveConfig();
            }
            ServiceController service = null;
            if (InstallAServiceFromPathAndName(ite.SubItems[2].Text, ite.SubItems[1].Text, ite.SubItems[4].Text, ite.SubItems[3].Text, ref service))
            {
                ite.ImageIndex = 2;
            }

            return;
        }

        private void ServersListcontextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            ServersListcontextMenuStrip.Hide();
            if (item != null)
            {
                if (item == AddtoolStripMenuItem)
                {
                    if ( AddServerbackgroundWorker.IsBusy )
                    {
                        return;
                    }
                    //AddServerbackgroundWorker.RunWorkerAsync();
                    AddServer();
                    return;
                }

                if ( ServerslistView.SelectedItems == null )
                {
                    LogrichTextBox.AppendText("no server selected. select one first. Aborting. \n");
                    return;
                }

                PlatformID p =Environment.OSVersion.Platform;

                ListViewItem ite = ServerslistView.SelectedItems[0];
                if (item == duplicatetoolStripMenuItem)
                {
                    DuplicateServer(ite);
                    return;
                }

                else
                    if (item == RemovetoolStripMenuItem)
                    {
                        RemoveServer(ite);
                    }
                    else
                    if (item == ModifytoolStripMenuItem)
                        {
                            ModifyServer(ite);
                        }
                        else
                            if (item == StarttoolStripMenuItem)
                            {
                                StartServer(ite);

                            }
                        else
                            if (item == ShutDowntoolStripMenuItem)
                            {
                                if (ShutDownAServiceFromName(ite.SubItems[1].Text, ite.SubItems[2].Text))
                                {
                                    ite.ImageIndex = 1;
                                }
                            }
                        else
                        if (item == unInstalltoolStripMenuItem)
                        {
                            UnInstallAService(ite.SubItems[0].Text, ite.SubItems[1].Text);
                        }
                        else
                            if (item == showDetailstoolStripMenuItem)
                            {
                                    connector.Lock();
                                    if (connector.isConnected)
                                    {
                                        connector.disConnect();
                                    }
                                    connector.Port = int.Parse(ite.SubItems[3].Text);
                                    connector.IP = ite.SubItems[4].Text;
                                    connector.Connect();
                                    if (!connector.isConnected)
                                    {
                                        LogrichTextBox.AppendText("show Details : could not connect to the server. not available.\n");
                                        connector.UnLock();
                                        return;
                                    }
                                    connector.UnLock();
                                
                            }
            }


        }

        public bool  ModifyService( string oldname, string newname, string oldport, string newport, string oldpath, string newpath, string ip, ref int index)
        {
            ServiceControllerStatus status = ServiceControllerStatus.Stopped;
            ServiceController service = null;

            if ( GetServiceStatus(oldname, oldport, ip, ref status, ref service) )
            {
                if (status == ServiceControllerStatus.Stopped)
                {
                    // on le lance d'abord
                    try
                    {
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running);
                        LogrichTextBox.AppendText("service '" + newname + "' has started. \n");
                    }
                    catch (InvalidOperationException exception)
                    {
                        LogrichTextBox.AppendText("could not start the service with old name '" + oldname + "'. Exception Occured : " + exception.InnerException.Message + "\n");
                        return false;
                    }
                }
                // pas dispo pour beta commerciale !!!!
                    //if (!newport.Equals(oldport))
                    //{
                    //    // on modifie le port
                    //    connector.Lock();
                    //    if (connector.isConnected)
                    //    {
                    //        connector.disConnect();
                    //    }
                    //    connector.IP = ip;
                    //    connector.Port = int.Parse(oldport);
                    //    connector.Connect();
                    //    if (!connector.isConnected)
                    //    {
                    //        connector.UnLock();
                    //        LogrichTextBox.AppendText("Error occured when modifying Port. server is not reachable at this port. Aborting ...");
                    //        return false;
                    //    }
                        
                    //    connector.Server().SetPort(newport);
                    //    if ( !connector.ErrorMsg.Equals("ok") )
                    //    {
                    //        LogrichTextBox.AppendText("Error occured when modifying Port. server Responded '" + connector.Received + "'. \n");
                    //        return false;
                    //    }
                    //    connector.disConnect();

                    //}
                
                
                    // puis on le stoppe
                    try
                    {
                        LogrichTextBox.AppendText("Stopping service '" + oldname + "'... \n");
                        service.Stop();
                        service.WaitForStatus(ServiceControllerStatus.Stopped);
                        LogrichTextBox.AppendText("service '"+oldname+"' has stopped. \n");
                    }
                    catch (InvalidOperationException exception)
                    {
                        LogrichTextBox.AppendText("could not stop the service. Exception Occured : " + exception.InnerException.Message + "\n");
                        return false;
                    }
                    service.ServiceName = newname;

                    if (!oldpath.Equals(newpath) || !oldname.Equals(newname) )
                    {
                        UnInstallAService(oldname, oldpath);
                        InstallAServiceFromPathAndName(newpath, newname, ip, newport, ref service);
                    }

                    if (service.Status == ServiceControllerStatus.Running)
                    {
                        index = 2;
                        LogrichTextBox.AppendText("service '" + newname + "' has started. \n");
                        return true;
                    }
                    // on le relance
                    try
                    {
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running);
                        LogrichTextBox.AppendText("service '" + newname + "' has started. \n");
                        index = 2;
                    }
                    catch (InvalidOperationException exception)
                    {
                        LogrichTextBox.AppendText("could not start the service with new name '"+newname+"'. Exception Occured : " + exception.InnerException.Message + "\n");
                        index = 1;
                        return false;
                    }
                
            }

            return true;
        }



        public bool GetServiceStatus(string name, string port, string ip, ref ServiceControllerStatus status, ref ServiceController service)
        {
            // check if service is already installed

            ServiceController[] services = ServiceController.GetServices();

            ServiceController serviceC;

            service = null;

            foreach (ServiceController s in services)
            {
                if (s.ServiceName.Equals(name))
                {
                    LogrichTextBox.AppendText(" service named '" + name + "' is installed. Looking for its state. \n");
                    serviceC = s;
                    service = serviceC;
                    goto getstatus;

                }
            }

            LogrichTextBox.AppendText(" service named '" + name + "' is not installed. No status Available \n");
            return false;

        getstatus:

            status = serviceC.Status;

            if (status == ServiceControllerStatus.Running)
            {
                connector.Lock();

                if (connector.isConnected)
                {
                    connector.disConnect();
                }
                    connector.Port = int.Parse(port);
                    connector.IP = ip;
                    connector.Connect();
                    if (connector.isConnected)
                    {
                        AppendColoredText(LogrichTextBox,Color.Green,"mARC server is Up and Running");
                        LogrichTextBox.AppendText(" and responded '" + connector.challengeMessage + "' \n.");
                        connector.disConnect();
                    }
                connector.UnLock();
            }
            else
            {
                AppendColoredText(LogrichTextBox, Color.Red, "mARC server is Down \n.");
            }
                

            
        return true;
        }

        public void AppendColoredText(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear


            start = box.TextLength;
            box.AppendText(" ");
            end = box.TextLength;
            box.Select(start, end - start);
            box.SelectionColor = Color.Black;
            box.ForeColor = Color.Black;
        }

        public bool ShutDownAServiceFromName(string name, string path)
        {
            // check if service is already installed

            ServiceController[] services = ServiceController.GetServices();

            ServiceController serviceToStart;

            foreach (ServiceController s in services)
            {
                if (s.ServiceName.Equals(name))
                {
                    LogrichTextBox.AppendText(" service named '" + name + "' is installed. Look its state. \n");
                    serviceToStart = s;
                    goto shutdownService;

                }
            }

            LogrichTextBox.AppendText(" service named '" + name + "' is not installed. Aborting Shut Down. \n");
            return false;

            shutdownService:

            ServiceControllerStatus status =   serviceToStart.Status;

            if (status == ServiceControllerStatus.Stopped )
            {
                LogrichTextBox.AppendText(" service named '" + name + "' is already stopped. Aborting Shut Down... \n");
                return true;
            }

            if (status == ServiceControllerStatus.StopPending)
            {
                try
                {
                    serviceToStart.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                catch (InvalidOperationException exception)
                {
                   LogrichTextBox.AppendText("could not stop the service. Exception Occured : " + exception.InnerException.Message + "\n");
                   return false;
                }

            }

            try
            {
                serviceToStart.Stop();
                serviceToStart.WaitForStatus(ServiceControllerStatus.Stopped);
                LogrichTextBox.AppendText("the service is stopped. \n");

            }
            catch (InvalidOperationException exception)
            {
                LogrichTextBox.AppendText("could not stop the service. Exception Occured : " + exception.InnerException.Message + "\n");
                return false;
            }

            return true;

        }

        public bool UnInstallAService(string name, string path)
        {
            // check if service is already installed

            ServiceController[] services = ServiceController.GetServices();

            ServiceController serviceToStart;

            foreach (ServiceController s in services)
            {
                if (s.ServiceName.Equals(name))
                {
                    LogrichTextBox.AppendText(" service named '" + name + "' is installed. Looking for its state. \n");
                    serviceToStart = s;
                    goto uninstallService;

                }
            }

            LogrichTextBox.AppendText(" service named '" + name + "' is not installed. Aborting UnInstall process. \n");
            return false;

        uninstallService:
            try
            {
                ServiceInstaller ServiceInstallerObj = new ServiceInstaller();
                InstallContext Context = new InstallContext(path, null);
                ServiceInstallerObj.Context = Context;
                ServiceInstallerObj.ServiceName = name;
                ServiceInstallerObj.Uninstall(null);
                LogrichTextBox.AppendText(" service named '" + name + "' is now UNinstalled. \n");
            }
            catch (InvalidOperationException exception)
            {
                LogrichTextBox.AppendText("could not uninstall the service. Exception Occured : " + exception.InnerException.Message + "\n");
                return false;
            }
            return true;

        }

        public bool InstallAServiceFromPathAndName(string path, string servicename, string ip, string port, ref ServiceController newService)
        {

            // check if service is already installed

            ServiceController[] services = ServiceController.GetServices();

            ServiceController serviceToStart;

            foreach (ServiceController s in services)
            {
                if (s.ServiceName.Equals(servicename))
                {
                    LogrichTextBox.AppendText(" service named '"+servicename+"' is already installed. Aborting installation. \n");
                    serviceToStart = s;
                    goto launchService;
  
                }
            }

            ServiceProcessInstaller ProcesServiceInstaller = new ServiceProcessInstaller();
            ProcesServiceInstaller.Account = ServiceAccount.LocalSystem;


            /*
            GetUserAndPasswordForm g = new GetUserAndPasswordForm();
            if (g.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProcesServiceInstaller.Username =   g.username;
                ProcesServiceInstaller.Password =  g.password; 
            }
            else
            {
                LogrichTextBox.AppendText("please provide a username and password to install mARC Server as Service. \n");
                return false;
            }
            */

            ProcesServiceInstaller.Username = null;
            ProcesServiceInstaller.Password = null; 


            ServiceInstaller ServiceInstallerObj = new ServiceInstaller();
            InstallContext Context = new InstallContext();
            String paths = String.Format("/assemblypath={0}", @path);
            String[] cmdline = { paths };

            Context = new System.Configuration.Install.InstallContext("", cmdline);
            ServiceInstallerObj.Context = Context;
            ServiceInstallerObj.DisplayName = servicename;
            ServiceInstallerObj.Description = "mARC server installation as Windows Service";
            ServiceInstallerObj.ServiceName = servicename;
            ServiceInstallerObj.StartType = ServiceStartMode.Manual;
            ServiceInstallerObj.Parent = ProcesServiceInstaller;

            System.Collections.Specialized.ListDictionary state = new System.Collections.Specialized.ListDictionary();
            ServiceInstallerObj.Install(state);
            LogrichTextBox.AppendText("mARC server is now installed as a service \n");
            serviceToStart = new ServiceController();
            serviceToStart.ServiceName = servicename;



        launchService:

            LogrichTextBox.AppendText("Starting mARC server as a Service...\n");

        try
        {
            serviceToStart.Start();
            serviceToStart.WaitForStatus(ServiceControllerStatus.Running);
            LogrichTextBox.AppendText("mARC server service status is now " + serviceToStart.Status.ToString()+". \n");

            connector.Lock();

            if (connector.isConnected)
                connector.disConnect();

            connector.IP = ip;
            connector.Port = int.Parse(port);

            connector.Connect();

            if (connector.isConnected)
            {
                LogrichTextBox.AppendText("mARC server responded '" + connector.challengeMessage + "' \n");
                connector.disConnect();
                connector.UnLock();
                return true;
            }

            connector.UnLock();


            return false;
        }
        catch (InvalidOperationException exception)
        {
            LogrichTextBox.AppendText("could not start the service. Exception Occured : " + exception.InnerException.Message + "\n");
            LogrichTextBox.AppendText("Have a look inside the server log. \n");
            return false;
        }

        }

        public bool ShutDownAServiceFromName(string name)
        {

            ServiceController[] services = ServiceController.GetServices();

            ServiceController serviceToShutDown = null;

            foreach (ServiceController s in services)
            {
                if (s.ServiceName.Equals(name))
                {
                    LogrichTextBox.AppendText(" service named '" + name + "' is already shut down. Aborting. \n");
                    serviceToShutDown = s;
                    break;
                }
            }

            if (serviceToShutDown == null)
            {
                LogrichTextBox.AppendText("service not installed. Aborting Shut Down. \n");
                return false;
            }

            try
            {
                serviceToShutDown.Stop();
                serviceToShutDown.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            catch (InvalidOperationException exception)
            {
                LogrichTextBox.AppendText("could not stop the service. Exception Occured : " + exception.InnerException.Message + "\n");
                return false;
            }

            return true;
        }

        private void DisplayDetailsbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string metrics = "", tasks ="";
            connector._DirectExecute = true;

            connector.Server().GetProperties("marc_particles;marc_shapes;marc_relations;marc_references;indexation_cache_used;marc_quality");

            string[] values = connector.GetDataByName("prop_value", -1);
            float ratioCache = 0, cacheUsed;
            if ( values != null && values.Length > 0 )
            {
                metrics += "knowledge marc :";
                    // le reste


                        metrics += "     shapes :" + values[1];

                        metrics += "     cels :" + values[0];

                        metrics += "     relations :" + values[2];

                        metrics += "     references :" + values[3];
                        metrics += "     marc quality  :" + values[5];
                cacheUsed = (float) int.Parse(connector._propertieValue[9]);
                ratioCache = (float)(int.Parse(values[4]))/cacheUsed * 100;
                        metrics += "     indexation cache used "+(int) ratioCache+"% \n";
                 }

                // les tasks
                connector.Server().GetTasks();
                string[] task = connector.GetDataByName("task", -1);
                if (task != null && task.Length > 0)
                {
                   
                    string[] completion = connector.GetDataByName("completion", -1);
                    string[] Current = connector.GetDataByName("current", -1);
                    string[] From = connector.GetDataByName("from", -1);
                    string[] To = connector.GetDataByName("to", -1);
                    for (int ii = 0; ii < task.Length; ii++)
                    {
                        tasks += "Task :" + task[ii] + "   completion: " + completion[ii] + "%";
                        tasks += "   current: " + Current[ii];
                        tasks += "   from: " + From[ii];
                        tasks += "   to: " + To[ii]+"\n";
                    }
                }


            connector.Table().GetInstances("1","-1");
            string[] r = connector.GetDataByName("tables", -1);
            if (r != null && r.Length > 0)
            {
                metrics += "\n";
                for (int i = 0; i < r.Length; i++)
                {
                    metrics += "Table#"+i+" :" + r[i]+" ";
                    connector.Table().GetLines(r[i]);
                    string[] lines = connector.GetDataByName("lines", -1);
                    if (lines != null && lines.Length > 0)
                        metrics += "     lines :" + lines[0] + "\n";
                    else
                        metrics += "     lines :0 \n";
                }
            }


            string[] toPrint = new string[2] { metrics, tasks };

            e.Result = toPrint;
        }

        private void DisplayDeatilsbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            metricsrichTextBox.Text = "";
            tasksrichTextBox.Text = "";

            string metrics =  connector._propertieValue[3]+ " "+connector._propertieValue[1]+ " ";
            metrics += connector._propertieValue[4];
			metrics  += " ";
			metrics  += connector._propertieValue[6];
			metrics  += " ";
			metrics  += connector._propertieValue[5];
			metrics  += " ";
			metrics  += connector._propertieValue[0];
			metrics  += " port : ";
			metrics  += connector._propertieValue[0];
            metrics += " Build: " + connector._propertieValue[2]+"\n";

            metricsrichTextBox.AppendText(metrics);


            if ( e.Result == null )
                return;

            string[] toPrint = (string[]) e.Result;

            if ( !string.IsNullOrEmpty( toPrint[0] ) )
            metricsrichTextBox.AppendText(toPrint[0]);
            if ( !string.IsNullOrEmpty(toPrint[1]))
                tasksrichTextBox.AppendText(toPrint[1]);

            connector.UnLock();

            if (displaycheckBox.Checked)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (DisplayDetailsbackgroundWorker.IsBusy)
            {
                LogrichTextBox.AppendText("the background thread is busy. not available.\n");
                return;
            }

            connector.Lock();

            if (!connector.isConnected)
            {
                connector.UnLock();
                return;
            }

            timer1.Enabled = false;
            DisplayDetailsbackgroundWorker.RunWorkerAsync();
        }

        private void displaycheckBox_MouseUp(object sender, MouseEventArgs e)
        {

            if (displaycheckBox.Checked)
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void displayRefreshRatetrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;

            timer1.Interval = displayRefreshRatetrackBar.Value;

            MStextBox.Text = displayRefreshRatetrackBar.Value.ToString();

            if (displaycheckBox.Checked)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;
        }

        private void displayRefreshRatetrackBar_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void displayRefreshRatetrackBar_ValueChanged(object sender, EventArgs e)
        {
            MStextBox.Text = displayRefreshRatetrackBar.Value.ToString();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ConfigurationToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void ConfigurationToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;

            if (item == null )
            {
                LogrichTextBox.AppendText("No server selected. Aborting...");
                return;
            }
            if (item == addAserverToolStripMenuItem)
            {
                if (AddServerbackgroundWorker.IsBusy)
                {
                    return;
                }
                //AddServerbackgroundWorker.RunWorkerAsync();
                AddServer();
                return;
            }

            if ( ServerslistView.SelectedItems == null )
            {
                LogrichTextBox.AppendText("No server selected. Aborting...");
                return;
            }
            ListViewItem ite = ServerslistView.SelectedItems[0];
            if (ite == null)
            {
                LogrichTextBox.AppendText("No server selected. Aborting...");
                return;
            }
                PlatformID p =Environment.OSVersion.Platform;

                if (item == DuplicateServerToolStripMenuItem)
                {
                    DuplicateServer(ite);
                    return;
                }
                else
                    if (item == RemoveServerToolStripMenuItem)
                    {
                        RemoveServer(ite);
                    }
                    else
                    if (item == ModifyServerToolStripMenuItem1)
                        {
                            ModifyServer(ite);
                        }
                        else
                            if (item == StartServerToolStripMenuItem)
                            {
                                StartServer(ite);

                            }
                        else
                            if (item == ShutDownServerToolStripMenuItem)
                            {
                                if (ShutDownAServiceFromName(ite.SubItems[1].Text, ite.SubItems[2].Text))
                                {
                                    ite.ImageIndex = 1;
                                }
                            }
                        else
                        if (item == UnInstallServerToolStripMenuItem)
                        {
                            UnInstallAService(ite.SubItems[0].Text, ite.SubItems[1].Text);
                        }
                        else
                            if (item == ShowServerDetailsToolStripMenuItem1)
                            {
                                    connector.Lock();
                                    if (connector.isConnected)
                                    {
                                        connector.disConnect();
                                    }
                                    connector.Port = int.Parse(ite.SubItems[3].Text);
                                    connector.IP = ite.SubItems[4].Text;
                                    connector.Connect();
                                    if (!connector.isConnected)
                                    {
                                        LogrichTextBox.AppendText("show Details : could not connect to the server. not available.\n");
                                        connector.UnLock();
                                        return;
                                    }
                                    connector.UnLock();
                                
                            }
            }

        private void duplicatebackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryInfo[] p = (DirectoryInfo[])e.Argument;
            DirectoryInfo diSource = p[0];
            DirectoryInfo diTarget = p[1];
            CopyAll(diSource, diTarget);
            e.Result = (object) diTarget.FullName;
        }

        private void duplicatebackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string msg = (string)e.UserState;
            LogrichTextBox.AppendText(msg);
        }

        private void duplicatebackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string targetDir = (string) e.Result;
            MessageBox.Show("server is now duplicated to '" + targetDir + "'. open this directory, edit the .cfg file and modify server port to avoid TCP problems. Add this server to the manager and start it.");

        }

        private void AddServerbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            AddServer();
        }

        private void AddServerbackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string msg = (string)e.UserState;
            LogrichTextBox.AppendText(msg);
        }

        private void AddServerbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AppendColoredText(LogrichTextBox,Color.GreenYellow,"Adding server operation completed. \n");
        }

    }
}
