using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SmartCam
{
    public class SmartCamClient
    {
        #region Variables
        //------------------------------------------------------------------------------------------------------------------------
        protected string RemoteHost;
        protected int RemotePort = 3333;
        //------------------------------------------------------------------------------------------------------------------------
        private Socket sock;
        private NetworkStream stream;
        private IFormatter formatter;
        //------------------------------------------------------------------------------------------------------------------------
        #endregion



        public SmartCamClient(string RemoteHost)
        {
            this.RemoteHost = RemoteHost;
            this.formatter = new BinaryFormatter();
        }

        //------------------------------------------------------------------------------------------------------------------------

        public Boolean Connect()
        {
            lock (this)
            {
                //Try to connect
                try
                {
                    //create socket
                    sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //attemp connection
                    sock.Connect(RemoteHost, RemotePort);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }

                //create stream
                stream = new NetworkStream(sock, true);

                //success
                return true;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------

        public Boolean SendShop(Shop shop)
        {
            lock(this)
            {
                if (sock.Connected)
                {
                    // Send the first message
                    FirstMsg fm = new FirstMsg();
                    fm.Type = MsgType.ShopConnected;
                    fm.MsgSize = 0;
                    formatter.Serialize(stream, fm);

                    // Send the Shop
                    formatter.Serialize(stream, shop);


                    return true;
                }
                else
                    return false;

            }
        }

        //------------------------------------------------------------------------------------------------------------------------

    }
}
