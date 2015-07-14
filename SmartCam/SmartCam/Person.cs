using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCam
{
    [Serializable]
    public class Person
    {
        public Guid Guid;
        public PointF Position;
        public PointF Direction;
        public List<PointF> Path = new List<PointF>();
        public Guid CameraGuid; // the camera that detected this person last time
        public Guid RegionGuid; // 

        public void UpdatePerson(Person p)
        {
            // Check that we are updating the correct person
            if (p.Guid == Guid)
            {
                // Update the path by accumulate the path
                Path.Add(p.Position);

                // Update the camera Guid
                CameraGuid = p.CameraGuid;
                RegionGuid = p.RegionGuid;
            }
        }
    }
}
