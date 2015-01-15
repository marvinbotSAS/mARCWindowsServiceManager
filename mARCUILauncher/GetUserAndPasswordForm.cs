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
    public partial class GetUserAndPasswordForm : Form
    {
        public string username;
        public string password;

        public GetUserAndPasswordForm()
        {
            InitializeComponent();
        }

        private void GetUserAndPasswordForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK ;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            username = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            password = textBox2.Text;
        }
    }
}
