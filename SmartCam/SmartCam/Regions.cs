using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCam
{
    [Serializable]
    public class Region
    {
        private Guid _guid;
        private Size _size;
        private Point _center;
        private Rectangle _rect;


        public Guid Guid { get { return _guid; } }

        public String Name { get; set; }

        public Point Center
        {
            get { return _center; }
            set { _center = value; UpdateRect(); }
        }

        public Size Size
        {
            get { return _size; }
            set { _size = value; UpdateRect(); }
        }

        public Rectangle Rect { get { return _rect; } }
        

        private void UpdateRect()
        {
            _rect = new Rectangle((int)(Center.X - Size.Width / 2),
                                         (int)(Center.Y - Size.Height / 2),
                                         Size.Width, Size.Height);
        }


        public Region()
        {
            _guid = Guid.NewGuid();
            _size = new Size();
            _center = new Point();
            UpdateRect();
        }

        public override string ToString()
        {

            return Name;
        }
    }
}
