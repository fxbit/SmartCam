using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCam
{
    class Server
    {
        Socket sock;
        Thread PortListener;
        int Port;

        public delegate void OnNewClientHandler(Server Server, Socket ClientSock);
        public event OnNewClientHandler OnNewClient = null;

        public Server()
        {
        }

        public void Start(int Port = 6517)
        {
            //keep
            this.Port = Port;

            //start port listener
            PortListener = new Thread(PortListenerEntryPoint);
            PortListener.IsBackground = true;
            PortListener.Start();
        }


        void PortListenerEntryPoint()
        {
            //create socket and bind
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Bind(new IPEndPoint(IPAddress.Any, Port));


            Console.WriteLine("Server Started");

            //heartbeat
            while (true)
            {
                //start listening
                sock.Listen(10);

                //accept and get new socket
                var newsocket = sock.Accept();

                // Start the client connection
                if (OnNewClient != null)
                    OnNewClient(this, newsocket);
            }
        }

    }
}
