using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SmartCam;

namespace ExampleForm
{
    public partial class Form1 : Form
    {
        public SmartCamListener SmartCam;

        //------------------------------------------------------------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------------------------------------------------------------------

        private void Form1_Load(object sender, EventArgs e)
        {
            SmartCam = new SmartCamListener();
            SmartCam.OnNewMsg += SmartCam_OnNewMsg;
        }
        
        //------------------------------------------------------------------------------------------------------------------------

        void SmartCam_OnNewMsg(SmartCam.MsgType msgType, Shop Shop)
        {
            switch (msgType)
            {
                case MsgType.ShopConnected:
                    listBox1.Invoke(() =>
                    {
                        listBox1.Items.Add("New Shop Connected");
                        pictureBox1.BackgroundImage = Shop.Plan;
                    });
                    break;
                case MsgType.PeopleList:
                    break;
                case MsgType.CameraUpdate:
                    break;
                default:
                    break;
            }



        }

        //------------------------------------------------------------------------------------------------------------------------
    }
}
