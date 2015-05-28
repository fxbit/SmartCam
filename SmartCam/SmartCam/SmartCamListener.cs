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

        public delegate void OnUpdatePersonsListHandler(Shop Shop);
        public event OnUpdatePersonsListHandler OnUpdatePersonsList = null;

        public delegate void OnUpdateCamerasHandler(Shop Shop, List<CameraPeoples> listCameras);
        public event OnUpdateCamerasHandler OnUpdateCameras = null;

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
                        case MsgType.ShopUpdate:
                        case MsgType.ShopConnected:
                            listener.shop = (Shop)listener.formatter.Deserialize(listener.stream);
                            break;
                        // --------------------------------------------------------------------------------
                        case MsgType.PersonsUpdate:
                            List<Person> list;
                            if (msg.MsgSize > 0)
                            {
                                list = (List<Person>)listener.formatter.Deserialize(listener.stream);
                            }
                            else
                            {
                                list = new List<Person>();
                            }

                            // Merge the persons and remove the ones that have be removed
                            List<Person> newList = new List<Person>();
                            foreach (var p in list)
                            {
                                var per = listener.shop
                                    .Persons
                                    .FirstOrDefault(x => x.Guid == p.Guid);

                                if (per != null)
                                {
                                    per.UpdatePerson(p);
                                    newList.Add(per);
                                }
                                else
                                {
                                    newList.Add(p);
                                }
                            }
                            listener.shop.Persons = newList;


                            // Send the signal to the user
                            if (OnUpdatePersonsList != null)
                                OnUpdatePersonsList(listener.shop);
                            break;
                        // --------------------------------------------------------------------------------
                        case MsgType.CamerasUpdate:
                            List<CameraPeoples> c;
                            if (msg.MsgSize > 0)
                            {
                                c = (List<CameraPeoples>)listener.formatter.Deserialize(listener.stream);
                            }
                            else
                            {
                                c = new List<CameraPeoples>();
                            }

                            if (OnUpdateCameras != null)
                                OnUpdateCameras(listener.shop, c);

                            break;
                        // --------------------------------------------------------------------------------
                        default:
                            break;
                    }


                    // Send signal to higher layer that we have recv data from shop
                    if (OnNewMsg != null)
                        OnNewMsg(msg.Type, listener.shop);
                }
            }
            catch (Exception ex)
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
