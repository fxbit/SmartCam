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

using FxMaths.Images;
using FxMaths.GUI;
using FxMaths.Vector;
using FxMaths.GMaps;
using FxMaths;
using FxMaths.Matrix;
using SmartCam;

namespace MainForm
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Form for debuging.
        /// You can print that console by call UIConsole.Write
        /// </summary>
        public static ConsoleOutput UIConsole = null;

        /// <summary>
        /// Form for drawing people position.
        /// </summary>
        public static PeopleOverview UIPeopleOverview = null;

        public static SerialInput UISerialInput = null;

        public static SerialCapture UISerialCapture = null;

        /// <summary>
        /// The class that simulate the people movments.
        /// </summary>
        public static PeopleSimulation peopleSimulation = null;


        public static FxMatrixF katopsi = null;
        public static FxMatrixF ikatopsi = null;
        public static FxMatrixMask katopsiMask = null;
        public static FxMatrixF heatMap = null;
        public static FxMatrixF katopsiHeatMap = null;
        public static FxImages heatImage;
        public static Bitmap heatBitmap;


        public static SmartCam.SmartCamClient smartCamClient;
        public static Bitmap PlanImage;
        public static FxImages planFxImage;
        public static String ShopName;

        #region Form
        public MainForm()
        {
            InitializeComponent();

            katopsi = FxMatrixF.Load("Katopsi.jpg", FxMaths.Matrix.ColorSpace.Grayscale);
            katopsiMask = katopsi > 0.8f;
            ikatopsi = -1 * (katopsi - 1);

            heatBitmap = new System.Drawing.Bitmap(katopsi.Width, katopsi.Height);
            heatImage = FxMaths.Images.FxTools.FxImages_safe_constructors(heatBitmap);

            // init the console
            UIConsole = new ConsoleOutput();
            UIConsole.Show(dockPanel1, DockState.DockBottomAutoHide);
            consoleOutputToolStripMenuItem.Checked = true;

            // init the people over view
            UIPeopleOverview = new PeopleOverview(katopsi);
            UIPeopleOverview.Show(dockPanel1, DockState.Document);
            peopleOverviewToolStripMenuItem.Checked = true;

            // Init Serial debugiing
            UISerialInput = new SerialInput();
            UISerialInput.Show(dockPanel1, DockState.Document);


            // Init serial Capture menu
            UISerialCapture = new SerialCapture();
            UISerialCapture.Show(dockPanel1, DockState.Document);
            UISerialCapture.onUpdatedCameras += UISerialCapture_UpdatedCameras;


            // Init client connection
            smartCamClient = new SmartCamClient("localhost");
            smartCamClient.Connect();

            
            // Send the event to server
            ShopName = "Unisol 1";
            PlanImage = new System.Drawing.Bitmap(katopsi.Width, katopsi.Height);
            planFxImage = FxMaths.Images.FxTools.FxImages_safe_constructors(PlanImage);
            FxMatrixF newKatopsi = MainForm.katopsi.Copy();
            var cams = MainForm.UISerialCapture.GetCameras();
            foreach (var cam in cams)
            {
                newKatopsi.DrawRect(new FxMaths.Vector.FxVector2f(cam.Center.X - cam.Size.Width / 2, cam.Center.Y - cam.Size.Height / 2),
                    new FxVector2f(cam.Size.Width, cam.Size.Height), 0.3f);
            }
            UIPeopleOverview.UpdateKatopsi(newKatopsi);
            planFxImage.Load(newKatopsi, ColorMap.GetColorMap(ColorMapDefaults.Bones));

            smartCamClient.SendShop(new Shop()
            {
                Name = ShopName,
                Plan = PlanImage,
                Cameras = UISerialCapture.GetCameras().ToDictionary<Camera, Guid>(c => c.Guid)
            }, false);
        }

        void UISerialCapture_UpdatedCameras(object sender, EventArgs e)
        {

            FxMatrixF newKatopsi = MainForm.katopsi.Copy();
            var cams = MainForm.UISerialCapture.GetCameras();
            foreach (var cam in cams)
            {
                newKatopsi.DrawRect(new FxMaths.Vector.FxVector2f(cam.Center.X - cam.Size.Width / 2, cam.Center.Y - cam.Size.Height / 2),
                    new FxVector2f(cam.Size.Width, cam.Size.Height), 0.3f);
            }
            UIPeopleOverview.UpdateKatopsi(newKatopsi);
            planFxImage.Load(newKatopsi, ColorMap.GetColorMap(ColorMapDefaults.Bones));


            smartCamClient.SendShop(new Shop()
            {
                Name = ShopName,
                Plan = PlanImage,
                Cameras = cams.ToDictionary<Camera, Guid>(c => c.Guid)
            }, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } 
        #endregion




        #region Console Output window

        private void consoleOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (consoleOutputToolStripMenuItem.Checked)
            {
                UIConsole.Hide();
            }
            else
            {
                if (UIConsole == null)
                    UIConsole = new ConsoleOutput();

                // add the viewport to the dock
                UIConsole.Show(dockPanel1, DockState.DockBottom);
            }

            consoleOutputToolStripMenuItem.Checked = !consoleOutputToolStripMenuItem.Checked;
        }

        #endregion




        #region People Overview


        private void peopleOverviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (peopleOverviewToolStripMenuItem.Checked)
            {
                UIPeopleOverview.Hide();
            }
            else
            {
                if (UIPeopleOverview == null)
                    UIPeopleOverview = new PeopleOverview();

                // add the viewport to the dock
                UIPeopleOverview.Show(dockPanel1, DockState.Document);
            }

            peopleOverviewToolStripMenuItem.Checked = !peopleOverviewToolStripMenuItem.Checked;
        }


        #endregion



        #region Simulator

        int refreshCount = 0;
        ColorMap heatColorMap = new ColorMap(ColorMapDefaults.Jet);
        private void peopleRefreshCB(PeopleSimulation ps)
        {
            var cameras = UISerialCapture.GetCameras();

            /* now we must update People Overview */
            if (UIPeopleOverview != null)
                UIPeopleOverview.PeopleUpdate(ps.PersonList);

            /* create a heet map */
            {
                FxMatrixF mask = new FxMatrixF(katopsi.Width, katopsi.Height);
                float a = 0.98f;


                // Create a map with persons
                foreach (Person p in ps.PersonList)
                {
                    mask.FillCircle(p.Position, 15.0f, 1 - a);
                }

                if (null == (heatMap as object))
                    heatMap = mask;
                else
                    heatMap = heatMap * a + mask;

                katopsiHeatMap = ikatopsi.Copy();
                katopsiHeatMap[katopsiMask] = heatMap / heatMap.Max();

                UIPeopleOverview.HeatMapUpdate(katopsiHeatMap);

                // Send it every 10 frames ~= 1Sec
                if (refreshCount > 10)
                {
                    heatImage.Load(katopsiHeatMap, heatColorMap);
                    smartCamClient.SendHeatMap(heatBitmap);
                    refreshCount = 0;
                }
                refreshCount++;
            }

            /* Send event to server */
            var listPersons = new List<SmartCam.Person>();
            foreach (var p in ps.PersonList)
            {
                listPersons.Add(new SmartCam.Person()
                {
                    Guid = p.Guid,
                    Direction = new PointF(p.Direction.x, p.Direction.y),
                    Position = new PointF(p.Position.x, p.Position.y),
                    CameraGuid = cameras
                                    .Where(x => x.Rect.Contains(new Point((int)p.Position.x, (int)p.Position.y)))
                                    .Select(y => y.Guid)
                                    .FirstOrDefault()
                });
            }
            smartCamClient.SendListPersons(listPersons);

        }

        #endregion

        // -------------------------------------------------------------------------------------------------------------------------------
        // clean resource when we close form 
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (peopleSimulation != null)
                peopleSimulation.Stop();

            if (UISerialCapture != null)
                UISerialCapture.Stop();

            if (UISerialInput != null)
                UISerialInput.Stop();
        }

        // -------------------------------------------------------------------------------------------------------------------------------
        // Simulation start 
        private void simulationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (peopleSimulation != null)
                peopleSimulation.Stop();

            peopleSimulation = new PeopleSimulation(10, new FxVector2f(560, 145), new FxVector2f(-1, 0), katopsi);
            peopleSimulation.Start(peopleRefreshCB);
        }

        // -------------------------------------------------------------------------------------------------------------------------------
        // Refresh the people overview -> camera informations
        private void refreshPeopleOverviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // The first things that we need to update is the camera overview
            UIPeopleOverview.CamerasUpdate(UISerialCapture.GetCameras());
        }
        // -------------------------------------------------------------------------------------------------------------------------------
    }
}
