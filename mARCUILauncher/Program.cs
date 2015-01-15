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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml.Linq;
using System.ServiceProcess;
using System.Security.Principal;
using System.Diagnostics;
using System.Reflection;

namespace mARCUILauncher
{
    static class Program
    {
        private static List<Misc.Config> _cnf;

        public static List<Misc.Config> Config { get { return _cnf; } }

        public static MainForm mainform;



        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            /*
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);

            bool runAsAdmin = wp.IsInRole(WindowsBuiltInRole.Administrator);
            ProcessStartInfo processInfo;
            if (!runAsAdmin)
            {
                // It is not possible to launch a ClickOnce app as administrator directly,
                // so instead we launch the app as administrator in a new process.
                processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

                // The following properties run the new process as administrator
                processInfo.UseShellExecute = true;
                processInfo.Verb = "runas";
                // Start the new process
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception)
                {
                    // The user did not allow the application to run as administrator
                    MessageBox.Show("Sorry, but I don't seem to be able to start " +
                       "this program with administrator rights! Aborting \n");
                    // Shut down the current process
                }
                Application.Exit();
            }
            else
            {
            */
                // We are running as administrator

                _cnf = new List<Misc.Config>();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);


                mainform = new MainForm();

                int error = Program.GetConfig();
                switch (error)
                {
                    case 0:
                        mainform.LogrichTextBox.AppendText(" configuration file loaded. \n");
                        break;
                    case -1:
                        mainform.LogrichTextBox.AppendText("config file is empty. consider adding some feeds. \n");
                        break;
                    case -2:
                        mainform.LogrichTextBox.AppendText("could not create config file. \n");
                        break;
                    case -3:
                        mainform.LogrichTextBox.AppendText("configuration file did not exist. New configuration file created. \n");
                        break;
                    case -4:
                        mainform.LogrichTextBox.AppendText("invalid configuration xml file. please delete it. \n");
                        break;
                    case -5:
                        mainform.LogrichTextBox.AppendText("could not create config directory. \n");
                        break;
                    case -6:
                        mainform.LogrichTextBox.AppendText("could not create config file. \n");
                        break;
                }

                try
                {

                Application.Run(mainform);
                }
                catch (System.Reflection.TargetInvocationException e)
                {

                   mainform.LogrichTextBox.AppendText(e.Message + "Exception Occured: '" + e.InnerException.Message + "' 2. inner exception '" + e.InnerException.InnerException.Message + "'  " + e.InnerException.InnerException.StackTrace);
                }
            //}
        }


        public static string AppDataPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar ;
            }
        }

        public static int GetConfig()
        {
            if (Directory.Exists(AppDataPath))
            {
                try
                {
                    XDocument doc = XDocument.Load(AppDataPath + "marcServersLauncher.config.xml");
                    try
                    {
                        ListViewItem item;
                        bool Save = false;
                        foreach (XElement server in doc.Root.Elements("Server"))
                        {
                            Misc.Config c = new Misc.Config(
                                    server.Attribute("Port").Value,
                                    server.Attribute("Name").Value,
                                    server.Attribute("Path").Value,
                                    server.Attribute("IP").Value
                                );

                            item = new ListViewItem();
                            item.ImageIndex = 0;

                            mainform.ServerslistView.Items.Add(item);
                            string port = "";
                            mainform.ScanServerCfgFile(c.Path, ref port);
                            if ( port != c.Port )
                            {
                                mainform.AppendColoredText(mainform.LogrichTextBox, Color.Red, "port read from cfg file of server '" + server.Attribute("Name").Value + "' is different from the one specified in config file. Updating. \n");
                                c.Port = port; 
                                Save = true;
                            }
                            ServiceControllerStatus status = ServiceControllerStatus.Stopped;
                            ServiceController service = null;
                            if (mainform.GetServiceStatus(server.Attribute("Name").Value, server.Attribute("Port").Value, server.Attribute("IP").Value, ref status, ref service))
                            {
                                if (status == ServiceControllerStatus.Stopped)
                                    item.ImageIndex = 1;
                                else
                                if (status == ServiceControllerStatus.Running)
                                    item.ImageIndex = 2;

                            }
                            item.SubItems.Add(c.Name);
                            item.SubItems.Add(c.Path);
                            item.SubItems.Add(c.Port);
                            item.SubItems.Add(c.IP);
                             Config.Add( c );
                        }

                        if ( Save )
                        {
                            mainform.ServerslistView.Refresh();
                            SaveConfig();
                        }
                    }
                    catch (NullReferenceException)
                    {
                        mainform.LogrichTextBox.AppendText("mARC Launcher Application: config file is empty: consider adding some servers. \n");
                        return -1;
                    }

                    return 0;

                }
                catch (FileNotFoundException)
                {
                    mainform.LogrichTextBox.AppendText("mARC Launcher Application: no config file found. creating...\n");
                    try
                    {
                        XDocument doc = new XDocument(new XElement("config"));
                        doc.Save(AppDataPath + "marcServersLauncher.config.xml");

                    }
                    catch (Exception ex)
                    {
                        mainform.LogrichTextBox.AppendText("mARC Launcher Application: could not create config file.\n");
                        return -2;
                        throw (new Misc.CreateConfigFileException("could not create config file", ex));
                    }
                    mainform.LogrichTextBox.AppendText("mARC Launcher Application: Config File created.\n");
                    return -3;
                }
                catch (UriFormatException)
                {
                    mainform.LogrichTextBox.AppendText("mARC Launcher Application: invalid xml file. please delete it.");
                    return -4;
                }
            }
            else
            {
                mainform.LogrichTextBox.AppendText("mARC Launcher Application: directory not found. creating...\n");
                try
                {
                    Directory.CreateDirectory(AppDataPath);
                }
                catch (Exception ex)
                {
                    mainform.LogrichTextBox.AppendText("mARC Launcher Application: could not create config directory.\n");
                    return -5;
                    throw (new Misc.CreateConfigDirException("could not create config directory", ex));
                }
                mainform.LogrichTextBox.AppendText("mARC Launcher Application: Directory '"+AppDataPath+"' created \n");

                mainform.LogrichTextBox.AppendText("mARC Launcher Application: creating config file...\n");
                try
                {
                    XDocument doc = new XDocument(new XElement("config"));
                    doc.Save(AppDataPath + "marcServersLauncher.config.xml");
                }
                catch (Exception ex)
                {
                    mainform.LogrichTextBox.AppendText("mARC Launcher Application: could not create config file.\n");
                    return -6;
                    throw (new Misc.CreateConfigFileException("could not create config file", ex));
                }
                mainform.LogrichTextBox.AppendText("mARC Launcher Application: created \n");
                return 0;
            }
        }

        public static bool SaveConfig()
        {
            string path = AppDataPath + "marcServersLauncher.config.xml";

            try
            {

            if (File.Exists(path))
            {
                File.Delete(AppDataPath + "marcServersLauncher.config.xml");
            }
            mainform.LogrichTextBox.AppendText("mARC Launcher Application: saving info to config file \n");
            XDocument doc = new XDocument(
                new XElement("config", Config.Select(
                    x => new XElement("Server",
                        new XAttribute("Port", x.Port),
                        new XAttribute("Name", x.Name),
                        new XAttribute("Path", x.Path),
                        new XAttribute("IP", x.IP)
                        )
                    ))
                );

            doc.Save(path);
            }
            catch (Exception eee)
            {
                mainform.LogrichTextBox.AppendText("Exception Occured: "+eee.StackTrace + ": " + eee.Message+"\n");
                return false;
            }

            return true;

        }

    }
}
