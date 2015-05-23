using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace MainForm
{
    public partial class ConsoleOutput : DockContent
    {
        public ConsoleOutput()
        {
            InitializeComponent();
        }



        #region Toolbar menu

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        #endregion



        #region Write Commands

        public void Write(String str)
        {
            richTextBox1.Text += str;
        }

        public void WriteLine(String str)
        {
            richTextBox1.Text += str + "\r\n";
        }

        #endregion


    }
}
