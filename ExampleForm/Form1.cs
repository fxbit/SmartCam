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
            SmartCam.OnUpdatePersonsList += SmartCam_OnUpdatePersonsList;
            SmartCam.OnUpdateCameras += SmartCam_OnUpdateCameras;
        }

        //------------------------------------------------------------------------------------------------------------------------

        void SmartCam_OnUpdateCameras(Shop Shop, List<CameraPeoples> listCameras)
        {
            listBox1.Invoke(() =>
            {
                listBox1.Items.Add(String.Format("Update camera persons {0} for shop {1}", listCameras.Count, Shop.Name));
            });
        }

        //------------------------------------------------------------------------------------------------------------------------

        void SmartCam_OnUpdatePersonsList(Shop Shop)
        {
            listBox1.Invoke(() =>
            {
                listBox1.Items.Add(String.Format("Update list persons {0} for shop {1}", Shop.Persons.Count, Shop.Name));
            });
        }

        //------------------------------------------------------------------------------------------------------------------------

        void SmartCam_OnNewMsg(SmartCam.MsgType msgType, Shop Shop)
        {
            switch (msgType)
            {
                case MsgType.ShopConnected:
                    listBox1.Invoke(() =>
                    {
                        listBox1.Items.Add(String.Format("New Shop ({0}) Connected with {1} cameras, Regions: {2}", Shop.Name, Shop.Cameras.Count, Shop.Regions.Count));
                        pictureBox1.BackgroundImage = Shop.Plan;
                    });
                    break;
                case MsgType.ShopUpdate:
                    listBox1.Invoke(() =>
                    {
                        listBox1.Items.Add(String.Format("Updated Shop ({0}) Num of Cameras: {1}, Regions: {2}", Shop.Name, Shop.Cameras.Count, Shop.Regions.Count));
                        pictureBox1.BackgroundImage = Shop.Plan;
                    });
                    break;
                case MsgType.HeatMapUpdate:
                    listBox1.Invoke(() =>
                    {
                        listBox1.Items.Add(String.Format("Updated Heat from shop {0}", Shop.Name));
                        pictureBox_heatmap.BackgroundImage = Shop.Heat;
                    });
                    break;
                default:
                    break;
            }


            // Limit the messages to 100 to avoid any memory issues
            listBox1.Invoke(() =>
            {
                while (listBox1.Items.Count > 100)
                {
                    listBox1.Items.RemoveAt(0);
                }
            });
        }

        //------------------------------------------------------------------------------------------------------------------------
    }
}
