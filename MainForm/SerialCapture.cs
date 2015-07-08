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

        private FxMatrixMask imageMask;
        public ColorMap imageMaskColorMap;

        private Dictionary<Cameras, ImageElement> imageMaskViews = new Dictionary<Cameras, ImageElement>();
        private Dictionary<Cameras, ImageElement> imageViews = new Dictionary<Cameras, ImageElement>();
        float NewImagePosition = 0;

        public event EventHandler onUpdatedCameras = null;

        // -------------------------------------------------------------------------------------------------------------------------------

        public SerialCapture()
        {
            InitializeComponent();

            // Init the mask and colormap
            imageMask = new FxMatrixMask(64, 64);
            imageMaskColorMap = new ColorMap(ColorMapDefaults.Jet);

            // Load default configurations
            LoadConfigurations();
        }
        
        // -------------------------------------------------------------------------------------------------------------------------------

        #region Add/Remove
        private void toolStripButton_plus_Click(object sender, EventArgs e)
        {
            OpenSerialForm osf = new OpenSerialForm();
            osf.SerialSelected += osf_SerialSelected;
            osf.ShowDialog();
            osf.Dispose();
        }

        // -------------------------------------------------------------------------------------------------------------------------------
        
        void osf_SerialSelected(OpenSerialForm.SerialSelectedEventArgs e)
        {
            var cam = new Cameras(e.Port, e.Rate)
            {
                CameraName = "Camera",
                Position = new Point(),
            };

            AddCamera(cam);
        }

        // -------------------------------------------------------------------------------------------------------------------------------

        private void AddCamera(Cameras cam)
        {
            listBox1.Items.Add(cam);

            // Start the streaming
            cam.Start();


            if (onUpdatedCameras != null)
                onUpdatedCameras(this, new EventArgs());

            // Create a new image viewer for this camera
            // and link it with dictionary 
            {
                var imageMaskView = new ImageElement(imageMask.ToFxMatrixF(), imageMaskColorMap);
                imageMaskView.Position = new FxMaths.Vector.FxVector2f(0, NewImagePosition);
                imageMaskView.lockMoving = true;
                NewImagePosition += imageMaskView.Size.Y + 10f /* Offset */;
                canvas1.AddElement(imageMaskView, false);
                imageMaskViews.Add(cam, imageMaskView);


                var imageView = new ImageElement(imageMask.ToFxMatrixF(), imageMaskColorMap);
                imageView._Position.x = imageMaskView.Position.X + imageMaskView.Size.X + 10f  /* Offset */;
                imageView._Position.Y = imageMaskView.Position.Y;
                imageView.lockMoving = true;
                canvas1.AddElement(imageView, false);
                imageViews.Add(cam, imageView);

                canvas1.FitView();
                canvas1.ReDraw();
            }

            // Camera 
            cam.onImageRecv += cam_onImageRecv;
        }

        // -------------------------------------------------------------------------------------------------------------------------------

        private void toolStripButton_minus_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem!=null)
            {
                Cameras c = (Cameras)listBox1.SelectedItem;
                c.Stop();


                listBox1.Items.Remove(listBox1.SelectedItem);
                
                if (onUpdatedCameras != null)
                    onUpdatedCameras(this, new EventArgs());
                
                // Remove the old image viewers
                try
                {
                    var maskView = imageMaskViews[c];
                    imageMaskViews.Remove(c);
                    canvas1.RemoveElement(maskView, false);
                    
                    var view = imageViews[c];
                    imageViews.Remove(c);
                    canvas1.RemoveElement(view, false);
                    
                    canvas1.FitView();
                    canvas1.ReDraw();
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }
        }
        #endregion

        // -------------------------------------------------------------------------------------------------------------------------------

        #region Properties

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            listBox1.Refresh();
            typeof(ListBox).InvokeMember("RefreshItems",
                                          BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
                                          null, listBox1, new object[] { });

            // Send event for update camera
            if (onUpdatedCameras != null)
                onUpdatedCameras(this, new EventArgs());
        }

        #endregion

        // -------------------------------------------------------------------------------------------------------------------------------

        #region Save/Load


        private void toolStripButton_save_Click(object sender, EventArgs e)
        {
            var list = listBox1.Items;
            var str = JsonConvert.SerializeObject(list);
            File.WriteAllText(FileName, str);
        }

        // -------------------------------------------------------------------------------------------------------------------------------

        private void LoadConfigurations()
        {
            if(File.Exists(FileName))
            {
                string str = File.ReadAllText(FileName);
                var list = JsonConvert.DeserializeObject<List<Cameras>>(str);
                listBox1.Items.Clear();
                
                foreach (var c in list)
                    AddCamera(c);
            }
        }

        #endregion

        // -------------------------------------------------------------------------------------------------------------------------------

        void cam_onImageRecv(object sender, EventArgs e)
        {
            Cameras cam = sender as Cameras;

            // Update the recv images
            var imageMaskView = imageMaskViews[cam];
            var imageView = imageViews[cam];

            // Update the image viewer
            imageMaskView.UpdateInternalImage(cam.image, imageMaskColorMap);
            imageView.UpdateInternalImage(cam.result, imageMaskColorMap);

            // Update fps
            toolStripLabel_fps.Text = cam.FPS.ToString();


            // TODO: Add here the detection algorithm



            // Refresh the view
            canvas1.ReDraw();
        }

        // -------------------------------------------------------------------------------------------------------------------------------
        
        public void Stop()
        {
            foreach(Cameras c in listBox1.Items)
            {
                c.Stop();
            }
        }
        
        // -------------------------------------------------------------------------------------------------------------------------------

        public List<SmartCam.Camera> GetCameras()
        {
            var cameras = new List<SmartCam.Camera>();

            foreach (Cameras cam in listBox1.Items)
            {
                cameras.Add(new SmartCam.Camera()
                {
                    Center = cam.Position,
                    Name = cam.CameraName,
                    Size = cam.Size,
                    Guid = cam.Guid,
                    IsRunning = cam.IsRunning,
                    Rect = new Rectangle((int)(cam.Position.X - cam.Size.Width / 2),
                                         (int)(cam.Position.Y - cam.Size.Height / 2),
                                         cam.Size.Width, cam.Size.Height)

                });
            }

            return cameras;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
                propertyGrid1.SelectedObject = listBox1.SelectedItem;
        }

        // -------------------------------------------------------------------------------------------------------------------------------

    }
}
