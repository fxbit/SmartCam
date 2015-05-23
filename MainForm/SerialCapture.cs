using MainForm.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

using Newtonsoft.Json;
using System.IO;
using FxMaths.GUI;
using FxMaths.Matrix;
using FxMaths.Images;

namespace MainForm
{
    public partial class SerialCapture : DockContent
    {
        const string FileName = "cameras.json";

        private ImageElement imageMaskView;
        private ImageElement imageView;
        private FxMatrixMask imageMask;
        public ColorMap imageMaskColorMap;

        public SerialCapture()
        {
            InitializeComponent();

            LoadConfigurations();


            imageMask = new FxMatrixMask(64, 64);
            imageMaskColorMap = new ColorMap(ColorMapDefaults.Jet);


            // Create a visual view
            imageMaskView = new ImageElement(imageMask.ToFxMatrixF(), imageMaskColorMap);
            canvas1.AddElement(imageMaskView, false);

            imageView = new ImageElement(imageMask.ToFxMatrixF(), imageMaskColorMap);
            imageView._Position.x = imageMaskView.Size.X + 10f;
            imageView._Position.Y = 0;
            canvas1.AddElement(imageView, false);

            canvas1.FitView();
        }




        #region Add/Remove
        private void toolStripButton_plus_Click(object sender, EventArgs e)
        {
            OpenSerialForm osf = new OpenSerialForm();
            osf.SerialSelected += osf_SerialSelected;
            osf.ShowDialog();
            osf.Dispose();
        }

        void osf_SerialSelected(OpenSerialForm.SerialSelectedEventArgs e)
        {
            var cam = new Cameras(e.Port, e.Rate)
            {
                CameraName = "Camera",
                Position = new Point()
            };

            listBox1.Items.Add(cam);

            // Start the streaming
            cam.Start();
        }


        private void toolStripButton_minus_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem!=null)
            {
                Cameras c = (Cameras)listBox1.SelectedItem;
                c.Stop();


                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        } 
        #endregion



        #region Properties
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                propertyGrid1.SelectedObject = listBox1.SelectedItem;


                foreach(Cameras cam in listBox1.Items)
                {
                    cam.onImageRecv -= cam_onImageRecv;
                }


                Cameras c = (Cameras)listBox1.SelectedItem;
                c.onImageRecv += cam_onImageRecv;
            }
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            listBox1.Refresh();
            typeof(ListBox).InvokeMember("RefreshItems",
                                          BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
                                          null, listBox1, new object[] { });
        }
        #endregion



        #region Save/Load


        private void toolStripButton_save_Click(object sender, EventArgs e)
        {
            var list = listBox1.Items;
            var str = JsonConvert.SerializeObject(list);
            File.WriteAllText(FileName, str);
        }

        private void LoadConfigurations()
        {
            if(File.Exists(FileName))
            {
                string str = File.ReadAllText(FileName);
                var list = JsonConvert.DeserializeObject<List<Cameras>>(str);
                listBox1.Items.Clear();


                foreach (var c in list)
                    c.Start();

                listBox1.Items.AddRange(list.ToArray());
            }
        }

        #endregion



        void cam_onImageRecv(object sender, EventArgs e)
        {
            Cameras cam = sender as Cameras;

            // Update the image viewer
            imageMaskView.UpdateInternalImage(cam.image, imageMaskColorMap);
            imageView.UpdateInternalImage(cam.result, imageMaskColorMap);

            // Update fps
            toolStripLabel_fps.Text = cam.FPS.ToString();

            canvas1.ReDraw();
        }




        public void Stop()
        {
            foreach(Cameras c in listBox1.Items)
            {
                c.Stop();
            }
        }
    }
}
