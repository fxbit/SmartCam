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
        /// The UID of camera
        /// </summary>
        public Guid Guid;

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
        
        /// <summary>
        /// The status of the camera
        /// </summary>
        public Boolean IsRunning;

        /// <summary>
        /// The area that the camera cover
        /// </summary>
        public Rectangle Rect;
    }

    [Serializable]
    public class CameraPeoples
    {
        public Camera Camera;
        public List<Person> Persons;
    }
}
