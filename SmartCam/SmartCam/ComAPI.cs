using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCam
{
    [Serializable]
    public enum MsgType
    {
        ShopConnected,
        ShopDisconnected,
        PersonsUpdate,
        CamerasUpdate,
        ShopUpdate
    }

    [Serializable]
    class FirstMsg
    {
        public MsgType Type;
        public int MsgSize;
    }
}
