using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace SmartCam
{

    public class SmartCamListener
    {
        //------------------------------------------------------------------------------------------------------------------------

        public List<Shop> Shops;
        private Server serverSock;
        private List<Listerners> clients;


        public delegate void OnNewMsgHandler(MsgType Type, Shop Shop);
        public event OnNewMsgHandler OnNewMsg = null;

        //------------------------------------------------------------------------------------------------------------------------

        public SmartCamListener()
        {
            // Init variables
            Shops = new List<Shop>();
            serverSock = new Server();
            clients = new List<Listerners>();

            serverSock.Start(3333);
            serverSock.OnNewClient += serverSock_OnNewClient;
        }

        //------------------------------------------------------------------------------------------------------------------------

        void serverSock_OnNewClient(Server Server, Socket ClientSock)
        {
            Console.WriteLine("New Client connected");


            // Create the listener 
            var listener = new Listerners();
            listener.sock = ClientSock;
            listener.stream = new NetworkStream(ClientSock, true);
            listener.formatter = new BinaryFormatter();
            listener.shop = new Shop();


            // Add to the list
            clients.Add(listener);

            // Start wait thread
            listener.readThread = new Thread(() => readData(listener));
            listener.readThread.IsBackground = true;
            listener.readThread.Start();


        }

        //------------------------------------------------------------------------------------------------------------------------

        void readData(Listerners listener)
        {
            try
            {
                while (true)
                {
                    // First we wait the type of the message
                    FirstMsg msg = (FirstMsg)listener.formatter.Deserialize(listener.stream);

                    Console.WriteLine("We have recv msg type: {0} Len: {1}", msg.Type, msg.MsgSize);

                    switch (msg.Type)
                    {
                        case MsgType.ShopConnected:
                            listener.shop = (Shop)listener.formatter.Deserialize(listener.stream);
                            break;
                        case MsgType.PeopleList:
                            break;
                        case MsgType.CameraUpdate:
                            break;
                        default:
                            break;
                    }


                    // Send signal to higher layer that we have recv data from shop
                    if (OnNewMsg != null)
                        OnNewMsg(msg.Type, listener.shop);
                }
            }
            catch(Exception ex)
            {
                // Send signal of disconnection
                if (OnNewMsg != null)
                    OnNewMsg(MsgType.ShopDisconnected, listener.shop);

                Console.WriteLine(ex.Message);
                clients.Remove(listener);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
    }

    class Listerners
    {
        public Socket sock;
        public NetworkStream stream;
        public Thread readThread;
        public IFormatter formatter;
        public Shop shop;
    }
}
