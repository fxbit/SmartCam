using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

using FxMaths.GUI;
using FxMaths.Images;
using FxMaths.Geometry;   
using FxMaths.Matrix;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace MainForm
{
    public partial class SerialInput : DockContent
    {
        ConsoleOutput uiconsole;
        Thread readThread;
        SerialPort serialPort;

        private Boolean isColor = false;

        private Boolean _continue = false;
        private FxMatrixMask imageMask;
        private ImageElement imageMaskView;
        private ImageElement imageView;
        public  ColorMap imageMaskColorMap;

        // Fps Measure
        private int fpsCount = 0;
        System.Windows.Forms.Timer fpsTimer;
        Stopwatch watch = new Stopwatch();


        public SerialInput()
        {
            InitializeComponent();

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


            // Linked to ui console
            uiconsole = MainForm.UIConsole;

            if (isColor)
            {
                imageMask = new FxMatrixMask(64, 64);
            }

            imageMask = new FxMatrixMask(64, 64);
            imageMaskColorMap = new ColorMap(ColorMapDefaults.Jet);

            // Create a visual view
            imageMaskView = new ImageElement(imageMask.ToFxMatrixF(), imageMaskColorMap);
            canvas1.AddElement(imageMaskView, false);

            imageView = new ImageElement(imageMask.ToFxMatrixF(), imageMaskColorMap);
            imageView._Position.x = imageMaskView.Size.X +  10f;
            imageView._Position.Y = 0;
            canvas1.AddElement(imageView, false);

            canvas1.FitView();



            // add the timer for the fps measure
            fpsTimer = new System.Windows.Forms.Timer();
            fpsTimer.Interval = 1000;
            watch.Start();

            fpsTimer.Tick += (s, te) =>
            {
                watch.Stop();
                float fps = fpsCount * 1000.0f / watch.ElapsedMilliseconds;
                //uiconsole.WriteLine("FPS:" + fps.ToString());
                //Console.WriteLine("FPS:" + fps.ToString());
                fpsLabel.Text = fps.ToString();
                fpsCount = 0;
                watch.Reset();
                watch.Start();
            };
            fpsTimer.Start();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenSerialForm osf = new OpenSerialForm();
            osf.SerialSelected += osf_SerialSelected;
            osf.ShowDialog();
        }

        void osf_SerialSelected(OpenSerialForm.SerialSelectedEventArgs e)
        {
            // set the conf of the serial
            serialPort.PortName = e.Port;
            serialPort.BaudRate = e.Rate;


            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace + ": " + ex.Message);
                uiconsole.WriteLine(ex.StackTrace + ": " + ex.Message);
                _continue = false;
                return;
            }

            try
            {
                _continue = true;

                if (readThread==null)
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
                uiconsole.WriteLine(ex.StackTrace + ": " + ex.Message);
                serialPort.Close();
                _continue = false;
                return;
            }

            // flag the connection
            uiconsole.WriteLine("Serial Connected");
        }




        #region Read thread

        private void Read()
        {
            int count = 0;
            Byte[] buffer = new Byte[130];

            int numBytes = (isColor) ? 130 : 10;

            Byte[,] imageBytes = new Byte[130, numBytes];
            
            int row_id = 0;

            FxBlobTracker fxtracker = new FxBlobTracker(imageMask.ToFxMatrixF());
            FxMatrixF image = new FxMatrixF(64,64);

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
                    if (isColor)
                    {
                        if (row_id == 0)
                        {
                            for (int i = 0; i < 64; i++)
                            {
                                for (int j = 0; j < 64; j++)
                                {

                                    byte c = imageBytes[i, j * 2];
                                    byte d = imageBytes[i, j * 2 + 1];

                                    byte r = (byte)(c & 0xF8);
                                    byte g = (byte)(((c & 0x07) << 5) + ((d & 0xE0) >> 3));
                                    byte b = (byte)((d & 0x1F) << 3);

                                    // Select the bit 
                                    if (false)
                                    {
                                        // RGB332
                                        image[j, i] = ((r & 0xE0) |
                                                        ((g >> 3) & 0x1C) |
                                                        ((b >> 6) & 0x03)
                                                       ) / 255.0f;
                                    }
                                    else
                                    {
                                        image[j, i] = ((b * 0.3f) + (g * 0.59f) + (r * 0.11f)) / 255.0f;
                                        if (image[j, i] > 1.0f)
                                            image[j, i] = 1.0f;
                                    }
                                }
                            }

                            // Update the show image
                            imageMaskView.UpdateInternalImage(image, imageMaskColorMap);

                            /* refresh images */
                            fpsCount++;
                            canvas1.ReDraw();
                        }
                    }
                    else
                    {
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
                            var result = blobs.ToFxMatrixF(64, 64);

                            foreach (FxBlob b in fxtracker.ListBlobs)
                            {
                                result.DrawCircle(b.Center, b.Radius, 0.5f);
                                image.DrawCircle(b.Center, b.Radius, 0.5f);
                            }


                            // Update the show image
                            imageMaskView.UpdateInternalImage(image, imageMaskColorMap);
                            imageView.UpdateInternalImage(result, imageMaskColorMap);

                            /* refresh images */
                            fpsCount++;
                            canvas1.ReDraw();
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

            Console.WriteLine(count);
        }

        private int readRow(Byte[] buffer, int  numBytes)
        {
            // Wait start chararcter
            int sc = 0x00;
            if (isColor)
            {
                while (sc != 0x55)
                    sc = serialPort.ReadByte();
            }
            else
            {
                while (sc != 0xAA)
                    sc = serialPort.ReadByte();
            }

            //Console.WriteLine("Start Frame:");

            // Wait the line id
            int ind = serialPort.ReadByte();
            //Console.WriteLine("Received Row: " + ind.ToString());



            serialPort.BaseStream.Read(buffer, 0, numBytes);

            //for (int i = 0; i < numBytes; i++)
            //    buffer[i] = (byte)serialPort.ReadByte();

            return ind;
        }

        #endregion
    }
}
