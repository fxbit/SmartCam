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
            ImageElement ie = new ImageElement(im, new ColorMap(ColorMapDefaults.Bones));
            ie.Name = "Katopsi";
            ie.lockMoving = true;
            canvas1.AddElement(ie);

            // add geometry plot
            gpe = new GeometryPlotElement();
            gpe.Name = "People";
            gpe.lockMoving = true;
            canvas1.AddElement(gpe);

        }

        private void PeopleOverview_Load(object sender, EventArgs e)
        {

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
    }
}
