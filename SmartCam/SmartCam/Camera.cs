using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCam
{
    [Serializable]
    public class Camera
    {
        /// <summary>
        /// A friently name of the cameras
        /// </summary>
        public String Name;


        /// <summary>
        /// Position of the cameras
        /// </summary>
        public PointF Center;

        /// <summary>
        /// Size of the cameras
        /// </summary>
        public SizeF Size;

    }

    [Serializable]
    public class CameraPeoples
    {
        public Camera Camera;
        public List<PersonSimple> Persons;
    }
}
