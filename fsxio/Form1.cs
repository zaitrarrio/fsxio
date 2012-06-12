using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

// Add these two statements to all SimConnect clients
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace Managed_Data_Request
{
    public partial class Form1 : Form
    {


        // User-defined win32 event
        const int WM_USER_SIMCONNECT = 0x0402;

        // SimConnect object
        SimConnect simconnect = null;

        enum DEFINITIONS
        {
            Struct1,
            StructAil,
            StructEle,
            StructThrottle,
            StructRudder,
        }

        enum DATA_REQUESTS
        {
            REQUEST_1,
        };


        // this is how you declare a data structure so that
        // simconnect knows how to fill it/read it.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct Struct1
        {
            // this is how you declare a fixed size string
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String title;
            // variables for data to be extracted from FSX go here
            public float latitude;
            public float longitude;
            public float trueAltitude;
            public float pitch;
            public float roll;
            public float headingMag;
            public float headingTrue;
            public float groundspeed;
            public float indicatedAltitude;
            public float gpsBearing;
            public float ias;
            public float vx;
            public float vy;
            public float vz;
            public float omegaX;
            public float omegaY;
            public float omegaZ;
            public double engineOnHours;
        };

        // Structs for control data to be sent to FSX
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct StructAil
        {
            public float ail;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct StructEle
        {
            public float ele;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct StructThrottle
        {
            public float throttle1;
            public float throttle2;
            public float throttle3;
            public float throttle4;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct StructRudder
        {
            public float rudder;
        }

        StructAil sa;
        StructEle se;
        StructThrottle st;
        StructRudder sr;

        bool TxRxUDP = false;
        UdpClient udpClient;
        String remoteHost;
        int remotePort;

        Thread inThread;
        int deadman = 0;
        double timeOld;
        double vxeOld;
        double vyeOld;
        double vzeOld;
        Stopwatch sw;
        int lastTicks;

        float radToDeg = 180 / (float)Math.PI;
        float degToRad = (float)Math.PI / 180;

        // variables for modeling gyro noise and drift
        double gyroBiasDrift;
        double gyroNoise;
        double gyroBiasLimit;
        double calculatedGyroBiasX;
        double calculatedGyroBiasY;
        double calculatedGyroBiasZ;
        Random rand = new Random();


        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();


        public Form1()
        {
            InitializeComponent();

            setButtons(true, false, false);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // initialize default values stored in form object properties
            timer1.Interval = (int)numericUpDownInterval.Value;
            gyroNoise = (double)numericUpDownNoise.Value;
            gyroBiasDrift = (double)numericUpDownBiasDrift.Value;
            gyroBiasLimit = (double)numericUpDownBiasLimit.Value;

        }


        // Simconnect client will send a win32 message when there is 
        // a packet to process. ReceiveMessage must be called to
        // trigger the events. This model keeps simconnect processing on the main thread.


        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_USER_SIMCONNECT)
            {
                if (simconnect != null)
                {
                    try
                    {
                        simconnect.ReceiveMessage();
                    }
                    catch (COMException ce) 
                    {
                        displayText("COM EXCEPTION");
                        // if this happens, try to re-connect
                        buttonDisconnect_Click(new object(), new EventArgs());
                        Thread.Sleep(10);
                        buttonConnect_Click(new object(), new EventArgs());
                    }
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        private void setButtons(bool bConnect, bool bGet, bool bDisconnect)
        {
            buttonConnect.Enabled = bConnect;

            buttonDisconnect.Enabled = bDisconnect;
        }

        private void closeConnection()
        {
            if (simconnect != null)
            {
                // Dispose serves the same purpose as SimConnect_Close()
                simconnect.Dispose();
                simconnect = null;
                displayText("Connection closed");
            }
        }

        // Set up all the SimConnect related data definitions and event handlers
        private void initDataRequest()
        {
            try
            {
                // listen to connect and quit msgs
                simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(simconnect_OnRecvOpen);
                simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(simconnect_OnRecvQuit);

                // listen to exceptions
                simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(simconnect_OnRecvException);

                // define a data structure
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Title", null, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Altitude", "meters", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Pitch Degrees", "degrees", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Bank Degrees", "degrees", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Heading Degrees Magnetic", "degrees", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Heading Degrees True", "degrees", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Ground Velocity", "meters per second", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Indicated Altitude", "meters", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "GPS Ground True Heading", "degrees", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Airspeed Indicated", "meters per second", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "VELOCITY WORLD X", "meters per second", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "VELOCITY WORLD Y", "meters per second", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "VELOCITY WORLD Z", "meters per second", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "ROTATION VELOCITY BODY Z", "radians per second", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "ROTATION VELOCITY BODY X", "radians per second", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "ROTATION VELOCITY BODY Y", "radians per second", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "GENERAL ENG ELAPSED TIME:1", "hours", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.StructAil, "Aileron Position", "Position", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.StructEle, "Elevator Position", "Position", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.StructThrottle, "GENERAL ENG THROTTLE LEVER POSITION:1", "percent", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.StructThrottle, "GENERAL ENG THROTTLE LEVER POSITION:2", "percent", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.StructThrottle, "GENERAL ENG THROTTLE LEVER POSITION:3", "percent", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.StructThrottle, "GENERAL ENG THROTTLE LEVER POSITION:4", "percent", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.StructRudder, "Rudder Position", "Position", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED);


                // IMPORTANT: register it with the simconnect managed wrapper marshaller
                // if you skip this step, you will only receive a uint in the .dwData field.
                simconnect.RegisterDataDefineStruct<Struct1>(DEFINITIONS.Struct1);
                simconnect.RegisterDataDefineStruct<Struct1>(DEFINITIONS.StructAil);
                simconnect.RegisterDataDefineStruct<Struct1>(DEFINITIONS.StructEle);
                simconnect.RegisterDataDefineStruct<Struct1>(DEFINITIONS.StructThrottle);
                simconnect.RegisterDataDefineStruct<Struct1>(DEFINITIONS.StructRudder);


                // catch a simobject data request
                simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(simconnect_OnRecvSimobjectDataBytype);
                simconnect.OnRecvSimobjectData += new SimConnect.RecvSimobjectDataEventHandler(simconnect_OnRecvSimobjectData);
            }
            catch (COMException ex)
            {
                displayText(ex.Message);
            }
        }

        double rxIntervalSmoothed = 0.016;
        void simconnect_OnRecvSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data)
        {            
            Struct1 s1 = (Struct1)data.dwData[0];
            // calculate timestep, there is no good way to get a timestamp from the sim
            // jitter will add noise to the calculated accelerations, but it should average out
            int ticks = Environment.TickCount;
            rxIntervalSmoothed = (0.001 * (ticks - lastTicks) + 9 * rxIntervalSmoothed) / 10; // convert to seconds and apply some smoothing
            lastTicks = ticks;
            // display the frequency of data reception
            int rxFrequency = (int)(1 / rxIntervalSmoothed);
            labelRxFreq.Text = rxFrequency.ToString();

            //build a rotation matrix with the given roll, pitch, heading, using aircraft axis convention (x = roll axis, y = pitch axis, z = yaw axis)
            double[] dcm = new double[9];
            double Sp = Math.Sin(s1.pitch * degToRad);
            double Cp = Math.Cos(s1.pitch * degToRad);
            double Sr = Math.Sin(s1.roll * degToRad);
            double Cr = Math.Cos(s1.roll * degToRad);
            double Sy = Math.Sin(s1.headingTrue * degToRad);
            double Cy = Math.Cos(s1.headingTrue * degToRad);

            dcm[0] = Cp * Cy;
            dcm[1] = Sr * Sp * Cy - Cr * Sy;
            dcm[2] = Cr * Sp * Cy + Sr * Sy;
            dcm[3] = Cp * Sy;
            dcm[4] = Sr * Sp * Sy + Cr * Cy;
            dcm[5] = Cr * Sp * Sy - Sr * Cy;
            dcm[6] = -Sp;
            dcm[7] = Sr * Cp;
            dcm[8] = Cr * Cp;

            // differentiate world speeds to get accels, then add gravity and rotate to body frame
            // fsx uses X as left/right, Y as up/down, Z as fore/aft; here we convert to aircraft convention
            // accelerations are positive in AFT LEFT DOWN direction
            double vxe = s1.vz;
            double vye = s1.vx;
            double vze = s1.vy;
            // if deltaT is 0, just use gravity
            double axe = rxIntervalSmoothed > 0 ? (vxe - vxeOld) / rxIntervalSmoothed : 0;
            double aye = rxIntervalSmoothed > 0 ? (vye - vyeOld) / rxIntervalSmoothed : 0;
            double aze = rxIntervalSmoothed > 0 ? (vze - vzeOld) / rxIntervalSmoothed + 9.81 : 9.81;
            vxeOld = vxe;
            vyeOld = vye;
            vzeOld = vze;

            // multiply earth frame accelerations by rotation matrix to get body frame accelerations
            double axb = axe * dcm[0] + aye * dcm[3] + aze * dcm[6];
            double ayb = axe * dcm[1] + aye * dcm[4] + aze * dcm[7];
            double azb = axe * dcm[2] + aye * dcm[5] + aze * dcm[8];

            // ***** Code for modelling gyro errors *********
            // calculate StdDev required to give the user-specified bias instability (rads/min) at the current sample rate
            // the bias instability is the 1sd error in angle after one minute of integration
            // biasinstability = sigma*sqrt(n)
            int samplesPerMinute = 60 * rxFrequency;
            double sigma = gyroBiasDrift / Math.Sqrt(samplesPerMinute);
            sigma /= 60; // convert to rads/sec
            // integrate noise to make the bias follow a random walk
            calculatedGyroBiasX += gaussianRandom(sigma);
            calculatedGyroBiasY += gaussianRandom(sigma);
            calculatedGyroBiasZ += gaussianRandom(sigma);
            // constrain them within limits
            constrainDouble(ref calculatedGyroBiasX, -gyroBiasLimit, gyroBiasLimit);
            constrainDouble(ref calculatedGyroBiasY, -gyroBiasLimit, gyroBiasLimit);
            constrainDouble(ref calculatedGyroBiasZ, -gyroBiasLimit, gyroBiasLimit);
            // add bias drift plus white noise
            s1.omegaX += (float)(gaussianRandom(gyroNoise) + calculatedGyroBiasX);
            s1.omegaY += (float)(gaussianRandom(gyroNoise) + calculatedGyroBiasY);
            s1.omegaZ += (float)(gaussianRandom(gyroNoise) + calculatedGyroBiasZ);

            // display the "gyro" readings and calculated accelerations
            labelOmegaX.Text = s1.omegaX.ToString();
            labelOmegaY.Text = s1.omegaY.ToString();
            labelOmegaZ.Text = s1.omegaZ.ToString();
            labelAX.Text = axb.ToString();
            labelAY.Text = ayb.ToString();
            labelAZ.Text = azb.ToString();


            // Convert to binary format, using x-plane UDP protocol
            // x-plane data is in 36 byte blocks with an index and 32 bytes of data (usually as floats)
            // gyro rates and accelerations are sent with index 255
            // BitConverter class is used to get a byte[4] array for each float. This is then copied to the main byte[]
            // some things are reversed to meet android conventions

            byte[] simData = new byte[201];
            simData[0] = 70; simData[1] = 83; simData[2] = 88; simData[3] = 33; // "FSX!" header
            simData[4] = 0; //not used

            simData[5] = 20; simData[6] = 0; simData[7] = 0; simData[8] = 0; // index for lat/lon/alt as int32
            BitConverter.GetBytes(s1.latitude).CopyTo(simData, 9);
            BitConverter.GetBytes(s1.longitude).CopyTo(simData, 13);
            BitConverter.GetBytes(s1.trueAltitude / 0.3048f).CopyTo(simData, 17); //convert to feet for x-plane compatability

            simData[41] = 3; simData[42] = 0; simData[43] = 0; simData[44] = 0; //index for speeds
            BitConverter.GetBytes(s1.ias / 0.5144f).CopyTo(simData, 45); //convert to knots for x-plane compatability
            BitConverter.GetBytes(s1.groundspeed / 0.5144f).CopyTo(simData, 57);

            simData[77] = 17; simData[78] = 0; simData[79] = 0; simData[80] = 0; //index for pitch roll heading
            BitConverter.GetBytes(s1.pitch).CopyTo(simData, 81);
            BitConverter.GetBytes(-s1.roll).CopyTo(simData, 85);
            BitConverter.GetBytes(s1.headingTrue).CopyTo(simData, 89);
            BitConverter.GetBytes(s1.headingMag).CopyTo(simData, 93);

            simData[113] = 4; simData[114] = 0; simData[115] = 0; simData[116] = 0; //index for processed accels, convert to Gs 
            BitConverter.GetBytes((float)azb / 9.8f).CopyTo(simData, 133); // down
            BitConverter.GetBytes((float)axb / 9.8f).CopyTo(simData, 137); // aft
            BitConverter.GetBytes((float)ayb / 9.8f).CopyTo(simData, 141); // left

            simData[149] = 16; simData[150] = 0; simData[151] = 0; simData[152] = 0; // index for gyro rates
            BitConverter.GetBytes(-s1.omegaY).CopyTo(simData, 153); // pitch rate
            BitConverter.GetBytes(-s1.omegaX).CopyTo(simData, 157); // roll rate
            BitConverter.GetBytes(s1.omegaZ).CopyTo(simData, 161); // yaw rate

            simData[185] = 18; simData[186] = 0; simData[187] = 0; simData[188] = 0;
            BitConverter.GetBytes(s1.gpsBearing).CopyTo(simData, 197);

            // send over the network to the remote host and port specified by user
            if (TxRxUDP)
            {
                byte[] packet = simData;
                try
                {
                    udpClient.Send(packet, packet.Length, remoteHost, remotePort);
                }
                catch (Exception ex) { displayText(ex.Message); }
            }


        }

        void simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            displayText("Connected to FSX");
        }

        // The case where the user closes FSX
        void simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            displayText("FSX has exited");
            closeConnection();
            timer1.Stop();
        }

        void simconnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            displayText("Exception received: " + data.dwException);
        }

        // The case where the user closes the client
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeConnection();
        }



        void simconnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            // not used at the moment
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (simconnect == null)
            {
                try
                {
                    // the constructor is similar to SimConnect_Open in the native API
                    simconnect = new SimConnect("Managed Data Request", this.Handle, WM_USER_SIMCONNECT, null, 0);

                    setButtons(false, true, true);

                    initDataRequest();
                    timer1.Start();
                    // timer for measuring elapsed time between updates
                    lastTicks = Environment.TickCount;
                    simconnect.RequestDataOnSimObject(DATA_REQUESTS.REQUEST_1, DEFINITIONS.Struct1, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_PERIOD.SIM_FRAME, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0U, 0U, 0U);

                    
                }
                catch (COMException ex)
                {
                    displayText("Unable to connect to FSX");
                }
            }
            else
            {
                displayText("Error - try again");
                closeConnection();

                setButtons(true, false, false);
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            TxRxUDP = false;
            closeConnection();
            setButtons(true, false, false);
        }


        // Response number
        int response = 1;

        // Output text - display a maximum of 10 lines
        string output = "\n\n\n\n\n\n\n\n\n\n";

        void displayText(string s)
        {
            // remove first string from output
            output = output.Substring(output.IndexOf("\n") + 1);

            // add the new string
            output += "\n" + response++ + ": " + s;

            // display it
            richResponse.Text = output;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            deadman++;
            // Deadman timer is reset when when new input is received; if no input is received then stop sending old data and pass control back to joystock
            if (deadman > 15) return;  
                               
            // Set the aileron, elevator, throttle, and rudder positions
            simconnect.SetDataOnSimObject(DEFINITIONS.StructAil, SimConnect.SIMCONNECT_OBJECT_ID_USER, 0, sa);
            simconnect.SetDataOnSimObject(DEFINITIONS.StructEle, SimConnect.SIMCONNECT_OBJECT_ID_USER, 0, se);
            simconnect.SetDataOnSimObject(DEFINITIONS.StructThrottle, SimConnect.SIMCONNECT_OBJECT_ID_USER, 0, st);
            simconnect.SetDataOnSimObject(DEFINITIONS.StructRudder, SimConnect.SIMCONNECT_OBJECT_ID_USER, 0, sr);
        }

        private void buttonUDP_Click(object sender, EventArgs e)
        {
            // listen on port 49005
            if (udpClient == null) udpClient = new UdpClient(49005);
            // get the user-specified IP and port to send to
            String[] remote = textBox1.Text.Split(':');
            if (remote.Length == 2 && remote[0] != null && Int32.TryParse(remote[1], out remotePort))
            {
                remoteHost = remote[0];
                TxRxUDP = true;
                inThread = new Thread(new ThreadStart(udpListener));
                inThread.Start();
                //this ensures listener is nuked when exiting
                inThread.IsBackground = true;

            }
            else MessageBox.Show("Enter remoteIP:remotePort");
        }
        
        
        //Delegate method for listener thread
        void udpListener()
        {           
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            while (TxRxUDP)
            {
                // listen for incoming data from any IP (this will block the thread untill data is received)
                byte[] data = udpClient.Receive(ref remoteEP);

                // FSX requires floats from -1 to 1 for ail/ele/rudder, and floats from 0 to 100 for throttle
                // parse the mini-SSC protocol (0 to 254) into floats, and place the values in the appropriate structs
                // they will be sent to FSX in the timer_tick method (lazy but it works)
                
                sa.ail = (checkBoxAil.Checked? -1:1) * (data[2] - 127) / 127f;
                se.ele = (checkBoxEle.Checked? -1:1) * (data[5] - 127) / 127f;
                float throttle = data[8] / 2.54f;
                if (checkBoxThrottle.Checked) throttle = 100 - throttle;
                st.throttle1 = throttle;
                st.throttle2 = throttle;
                st.throttle3 = throttle;
                st.throttle4 = throttle;
                sr.rudder = (checkBoxRudder.Checked? -1:1) * (data[11] - 127) / 127f;
                // reset the deadman timer
                deadman = 0;
            }
        }

        private void numericUpDownHandler(object sender, EventArgs e)
        {
            if (sender.Equals(numericUpDownInterval)) timer1.Interval = (int)numericUpDownInterval.Value;
            else if (sender.Equals(numericUpDownNoise)) gyroNoise = (double)numericUpDownNoise.Value;
            else if (sender.Equals(numericUpDownBiasDrift)) gyroBiasDrift = (double)numericUpDownBiasDrift.Value;
            else if (sender.Equals(numericUpDownBiasLimit)) gyroBiasLimit = (double)numericUpDownBiasLimit.Value;
        }

        private double gaussianRandom(double stdDev)
        {
            double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }
        private void constrainDouble(ref double value, double min, double max)
        {
            if (value > max) value = max;
            else if (value < min) value = min;           
        }
       


    }
}
