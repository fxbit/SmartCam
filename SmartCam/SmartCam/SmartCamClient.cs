﻿using System;
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

        public Boolean SendShop(Shop shop, Boolean Update)
        {
            lock (this)
            {
                if (sock.Connected)
                {
                    try
                    {
                        // Send the first message
                        FirstMsg fm = new FirstMsg();
                        fm.Type = (Update) ? MsgType.ShopUpdate : MsgType.ShopConnected;
                        fm.MsgSize = 0;
                        formatter.Serialize(stream, fm);

                        // Send the Shop
                        formatter.Serialize(stream, shop);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error:{0}",ex.Message);
                        return false;
                    }
                    return true;
                }
                else
                    return false;

            }
        }

        //------------------------------------------------------------------------------------------------------------------------

        public Boolean SendListPersons(List<Person> listPersons)
        {
            lock (this)
            {
                if (sock.Connected)
                {
                    try
                    {
                        // Send the first message
                        FirstMsg fm = new FirstMsg();
                        fm.Type = MsgType.PersonsUpdate;
                        fm.MsgSize = listPersons.Count;
                        formatter.Serialize(stream, fm);

                        // Send the Shop
                        if (listPersons.Count > 0)
                            formatter.Serialize(stream, listPersons);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error:{0}", ex.Message);
                        return false;
                    }
                    return true;
                }
                else
                    return false;
            }
        }


        //------------------------------------------------------------------------------------------------------------------------

        public Boolean SendCamerasUpdate(List<CameraPeoples> peoples)
        {
            lock (this)
            {
                if (sock.Connected)
                {
                    try
                    {
                        // Send the first message
                        FirstMsg fm = new FirstMsg();
                        fm.Type = MsgType.CamerasUpdate;
                        fm.MsgSize = peoples.Count;
                        formatter.Serialize(stream, fm);

                        // Send the Shop
                        if (peoples.Count > 0)
                            formatter.Serialize(stream, peoples);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error:{0}", ex.Message);
                        return false;
                    }
                    return true;
                }
                else
                    return false;
            }
        }


        //------------------------------------------------------------------------------------------------------------------------


        public bool SendHeatMap(Image HeatMap)
        {
            lock (this)
            {
                if (sock.Connected)
                {
                    try
                    {
                        // Send the first message
                        FirstMsg fm = new FirstMsg();
                        fm.Type = MsgType.HeatMapUpdate;
                        fm.MsgSize = 0;
                        formatter.Serialize(stream, fm);

                        // Send the heatMap
                        formatter.Serialize(stream, HeatMap);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error:{0}", ex.Message);
                        return false;
                    }
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
