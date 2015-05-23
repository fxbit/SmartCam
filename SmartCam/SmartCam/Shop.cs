using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCam
{
    [Serializable]
    public class Shop
    {
        /// <summary>
        /// The plan image of the shop.
        /// This is going to be updated when we get the first connection.
        /// </summary>
        public Image Plan;

        /// <summary>
        /// The name of the shop
        /// </summary>
        public String Name;


    }
}
