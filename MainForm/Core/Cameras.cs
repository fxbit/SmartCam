using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

using FxMaths.GUI;
using FxMaths.Images;
using FxMaths.Matrix;

namespace MainForm.Core
{
    public class Cameras
    {

        #region Public configurations
        public String CameraName { get; set; }
        private String _port;
        public String Port { get { return _port; } }
        private int _rate;
        public int Rate { get { return _rate; } }
        public Point Position { get; set; }
        public Size Size { get; set; }
        private float _fps;
        public float FPS { get { return _fps; } }
        #endregion

        public event EventHandler onImageRecv;

        public Cameras(String Port, int Rate)
        {
            _port = Port;
            _rate = Rate;
            _fps = 0.0f;
        }


        #region Utils
        public override string ToString()
        {
            return CameraName + " (" + Port + ")";
        }
        #endregion

        // ---------------------------------------------------------------------------


        #region Capture parameters

        // Fps Measure
        private int fpsCount = 0;
        private System.Windows.Forms.Timer fpsTimer = new System.Windows.Forms.Timer();
        private Stopwatch watch = new Stopwatch();

        private Thread readThread;
        private SerialPort serialPort;

        private Boolean _continue = false;
        private ColorMap imageMaskColorMap;

        [JsonIgnore]
        public FxMatrixF image;
        [JsonIgnore]
        public FxMatrixF result;
        [JsonIgnore]
        public FxMatrixMask imageMask;
        #endregion


        #region Capture


        #region Start/Stop
        public void Start()
        {
            // init the serial port
            serialPort = new SerialPort();

            // Set the read/write timeouts
            serialPort.ReadTimeout = -1;
            serialPort.WriteTimeout = -1;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.Two;
            serialPort.Handshake = Handshake.None;
            serialPort.NewLine = "\r\n";
            serialPort.PortName = _port;
            serialPort.BaudRate = _rate;


            imageMask = new FxMatrixMask(64, 64);
            imageMaskColorMap = new ColorMap(ColorMapDefaults.Jet);



            // Start the connection
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _continue = false;
                return;
            }

            try
            {
                _continue = true;

                if (readThread == null)
                    readThread = new Thread(Read);

                // check if the thread have be stoped
                if (readThread.ThreadState == System.Threading.ThreadState.Aborted || readThread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    readThread = new Thread(Read);
                    readThread.Priority = ThreadPriority.Highest;
                    readThread.Start();
                }
                else
                    readThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace + ": " + ex.Message);
                MessageBox.Show(ex.StackTrace + ": " + ex.Message);
                serialPort.Close();
                _continue = false;
                return;
            }

            // flag the connection
            Console.WriteLine("Serial Connected");


            // add the timer for the fps measure
            fpsTimer.Interval = 1000;
            watch.Start();

            fpsTimer.Tick += (s, te) =>
            {
                watch.Stop();
                _fps = fpsCount * 1000.0f / watch.ElapsedMilliseconds;
                fpsCount = 0;
                watch.Reset();
                watch.Start();
            };
            fpsTimer.Start();

        }


        public void Stop()
        {
            _continue = false;
            serialPort.Close();
            if (readThread!=null)
                readThread.Abort();
        }
        
        #endregion



        #region Thread
        
        private void Read(object obj)
        {
            int count = 0;
            Byte[] buffer = new Byte[130];

            int numBytes = 10;

            Byte[,] imageBytes = new Byte[130, numBytes];

            int row_id = 0;

            FxBlobTracker fxtracker = new FxBlobTracker(imageMask.ToFxMatrixF());

            while (_continue)
            {
                try
                {
                    // Read one row
                    row_id = readRow(buffer, numBytes) - 32;

                    // save the row
                    if (row_id >= 0 &&
                        row_id < 256)
                    {
                        for (int i = 0; i < numBytes; i++)
                            imageBytes[row_id, i] = buffer[i];
                    }

                    // Show results 
                    if (row_id == 63)
                    {
                        //Console.WriteLine("Read Image");

                        for (int i = 0; i < 64; i++)
                        {
                            int bindex = 0;
                            for (int j = 0; j < 64; j++)
                            {
                                byte b = imageBytes[i, bindex];

                                // Select the bit 
                                imageMask[j, i] = ((b & (1 << 7 - j % 8)) > 0);

                                // Move to the next byte
                                if (j % 8 == 7)
                                    bindex++;
                            }
                        }
                        /* process the new matrix */
                        //fxtracker.Process(imageMask.ToFxMatrixF());
                        fxtracker.Process(imageMask);

                        image = imageMask.ToFxMatrixF();

                        var blobs = new FxContour(fxtracker.G_small);
                        result = blobs.ToFxMatrixF(64, 64);

                        foreach (FxBlob b in fxtracker.ListBlobs)
                        {
                            result.DrawCircle(b.Center, b.Radius, 0.5f);
                            image.DrawCircle(b.Center, b.Radius, 0.5f);
                        }


                        // Update the show image
                        if (onImageRecv != null)
                            onImageRecv(this, new EventArgs());

                        fpsCount++;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

            Console.WriteLine(count);
        }

        private int readRow(Byte[] buffer, int numBytes)
        {
            // Wait start chararcter
            int sc = 0x00;
            while (sc != 0xAA)
                sc = serialPort.ReadByte();

            // Wait the line id
            int ind = serialPort.ReadByte();
            serialPort.BaseStream.Read(buffer, 0, numBytes);

            return ind;
        }

        #endregion


        #endregion
    }
}
