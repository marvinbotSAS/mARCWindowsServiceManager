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

namespace mARCUILauncher
{
    public partial class newServerForm : Form
    {

        public string name;

        public string path;

        public string port;

        public string Ip;


        public newServerForm()
        {
            InitializeComponent();

            Ip = textBoxIp.Text;
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.Enabled = false;

            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Multiselect = false;
            if (openFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFD.FileNames == null )
                {
                    return;
                }

            textBox1.Enabled = true;
            path = openFD.FileName;
            textBox1.Text = path;
            }

            textBox1.Enabled = true;

        }

        private void buttonCancel_MouseUp(object sender, MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            port = textBoxPort.Text;
        }

        private void textBoxIp_TextChanged(object sender, EventArgs e)
        {
            Ip = textBoxIp.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            name = textBox3.Text;
        }

        private void newServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel || DialogResult == DialogResult.Abort)
            {
                return;
            }
            if ( string.IsNullOrEmpty(path) ||  string.IsNullOrEmpty(name))
            {
                e.Cancel = true;
            }

        }

        private void buttonOK_MouseUp(object sender, MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }



    }
}
