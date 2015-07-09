using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FxMaths.GUI;
using FxMaths.Images;
using FxMaths.Geometry;   
using FxMaths.Matrix;
using WeifenLuo.WinFormsUI.Docking;
using SmartCam;

namespace MainForm
{
    public partial class PeopleOverview : DockContent
    {
        // -------------------------------------------------------------------------------------------------------------------------------

        GeometryPlotElement peoples;
        GeometryPlotElement cameras;

        ImageElement heat;
        public ColorMap heatMap = ColorMap.GetColorMap(ColorMapDefaults.Jet);

        ImageElement plan;
        public ColorMap planMap = ColorMap.GetColorMap(ColorMapDefaults.Bones);

        // -------------------------------------------------------------------------------------------------------------------------------

        public PeopleOverview()
        {
            InitializeComponent();

            peoples = new GeometryPlotElement();
            peoples.Name = "People";
            peoples.lockMoving = true;
            canvas1.AddElement(peoples, false);


            cameras = new GeometryPlotElement();
            cameras.Name = "Cameras";
            cameras.lockMoving = true;
            canvas1.AddElement(cameras, false);
        }

        // -------------------------------------------------------------------------------------------------------------------------------

        public PeopleOverview(FxMatrixF im)
        {
            InitializeComponent();

            // add building 
            plan = new ImageElement(im, planMap);
            plan.Name = "Katopsi";
            plan.lockMoving = true;
            canvas1.AddElement(plan);

            // add geometry plot
            peoples = new GeometryPlotElement();
            peoples.Name = "People";
            peoples.lockMoving = true;
            canvas1.AddElement(peoples);


            cameras = new GeometryPlotElement();
            cameras.Name = "Cameras";
            cameras.lockMoving = true;
            canvas1.AddElement(cameras, false);

            // Add heat map
            heat = new ImageElement(im, heatMap);
            heat.Name = "Heat";
            heat.lockMoving = true;
            heat.Position = new FxMaths.Vector.FxVector2f(0, plan.Size.y);
            canvas1.AddElement(heat);
        }
        
        internal void UpdateKatopsi(FxMatrixF newKatopsi)
        {
            plan.UpdateInternalImage(newKatopsi, planMap);
        }

        public void PeopleUpdate(List<Person> personsList)
        {
#if true
            // remove all the old geometry
            peoples.ClearGeometry(false);

            // add all persons
            foreach (Person p in personsList)
            {
                // simulation path
                //Path pa = new Path(p.Path);
                //peoples.AddGeometry(pa, false);

                // with kalman
                Path pa = new Path(p.PathKalman);
                pa.LineColor = SharpDX.Color.Red;
                pa.UseDefaultColor = false;
                peoples.AddGeometry(pa, false);

                // the circle
                Circle c = new Circle(p.Position, 10);
                peoples.AddGeometry(c, false);
            }

            peoples.ReDraw();
#endif
        }

        // -------------------------------------------------------------------------------------------------------------------------------

        public void CamerasUpdate(List<Camera> cameraList)
        {
#if true
            // remove all the old geometry
            cameras.ClearGeometry(false);

            // add all cameras
            foreach(Camera c in cameraList)
            {
                // calc start/end
                var start = new FxMaths.Vector.FxVector2f(c.Center.X - c.Size.Width / 2, c.Center.Y - c.Size.Height / 2);
                var end = new FxMaths.Vector.FxVector2f(c.Center.X + c.Size.Width / 2, c.Center.Y + c.Size.Height / 2);

                // Add  to the geometry but not redraw
                var rect = new FxMaths.Geometry.Rectangle(start, end);
                cameras.AddGeometry(rect, false);
            }

            cameras.ReDraw();
#endif
        }

        // -------------------------------------------------------------------------------------------------------------------------------

        public void HeatMapUpdate(FxMatrixF h)
        {
            heat.UpdateInternalImage(h, heatMap); ;
        }

        // -------------------------------------------------------------------------------------------------------------------------------

        public void PlanUpdate(FxMatrixF p)
        {
            plan.UpdateInternalImage(p, planMap);
        }

        // -------------------------------------------------------------------------------------------------------------------------------
    }
}
