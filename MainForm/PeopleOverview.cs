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
        GeometryPlotElement gpe;
        ImageElement heat;
        ImageElement ie;

        public PeopleOverview()
        {
            InitializeComponent();

            gpe = new GeometryPlotElement();
            gpe.Name = "People";
            gpe.lockMoving = true;
            canvas1.AddElement(gpe);

        }

        public PeopleOverview(FxMatrixF im)
        {
            InitializeComponent();

            // add building 
            ie = new ImageElement(im, ColorMap.GetColorMap(ColorMapDefaults.Bones));
            ie.Name = "Katopsi";
            ie.lockMoving = true;
            canvas1.AddElement(ie);

            // add geometry plot
            gpe = new GeometryPlotElement();
            gpe.Name = "People";
            gpe.lockMoving = true;
            canvas1.AddElement(gpe);


            // Add heat map
            heat = new ImageElement(im, ColorMap.GetColorMap(ColorMapDefaults.Jet));
            heat.Name = "Heat";
            heat.lockMoving = true;
            heat.Position = new FxMaths.Vector.FxVector2f(0, ie.Size.y);
            canvas1.AddElement(heat);


        }

        private void PeopleOverview_Load(object sender, EventArgs e)
        {

        }
        
        internal void UpdateKatopsi(FxMatrixF newKatopsi)
        {
            ie.UpdateInternalImage(newKatopsi, ColorMap.GetColorMap(ColorMapDefaults.Bones));
        }

        public void PeopleUpdate(List<Person> personsList)
        {
            // remove all the old geometry
            gpe.ClearGeometry(false);

            // add all persons
            foreach (Person p in personsList)
            {
                // simulation path
                Path pa = new Path(p.Path);
                gpe.AddGeometry(pa, false);

                // with kalman
                pa = new Path(p.PathKalman);
                pa.LineColor = SharpDX.Color.Red;
                pa.UseDefaultColor = false;
                gpe.AddGeometry(pa, false);

                // the circle
                Circle c = new Circle(p.Position, 10);
                gpe.AddGeometry(c, false);
            }

            gpe.ReDraw();
            
        }


        public ColorMap heatMap = ColorMap.GetColorMap(ColorMapDefaults.Jet);
        public void HeatMapUpdate(FxMatrixF h)
        {
            heat.UpdateInternalImage(h, heatMap); ;
        }
    }
}
