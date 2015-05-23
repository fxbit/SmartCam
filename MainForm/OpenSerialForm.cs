using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class OpenSerialForm : Form
    {
        public class SerialSelectedEventArgs : EventArgs
        {
            public String Port { get; set; }
            public int Rate { get; set; }
        }

        public delegate void SerialSelectedEventHandler(SerialSelectedEventArgs e);
        public event SerialSelectedEventHandler SerialSelected;

        public OpenSerialForm()
        {
            InitializeComponent();


            // get the available serial ports
            m_comport.Items.AddRange(SerialPort.GetPortNames());
            // select the first one
            if (m_comport.Items.Count > 0)
                m_comport.SelectedIndex = 0;

            // set the baud rates
            m_baud.Items.Clear();
            m_baud.Items.Add(921600);
            m_baud.Items.Add(115200);
            m_baud.Items.Add(57600);
            m_baud.Items.Add(38400);
            m_baud.Items.Add(19200);
            m_baud.Items.Add(14400);
            m_baud.Items.Add(9600);
            m_baud.SelectedIndex = 0;
            m_baud.Refresh();
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            SerialSelectedEventArgs args = new SerialSelectedEventArgs();
            args.Port = (String)m_comport.SelectedItem;
            args.Rate = (int)m_baud.SelectedItem;
            if (SerialSelected != null)
                SerialSelected(args);
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
