using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Maps.MapControl.WPF;
using static System.Net.Mime.MediaTypeNames;

namespace FlightSimulator.Model
{
    class MFSModel : IFSModel
    {
        FSClient client;
        volatile Boolean stop =false;
        bool changeInRudder = false;
        bool changeInElevator = false;
        bool changeInAileron = false;
        bool changeInThrottle = false;
       
        public MFSModel(FSClient flightSimulatorClient)
        {
            this.client = flightSimulatorClient;
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #region Prperties
        private string status;
        public string Status
        {
            get { return this.status; }
            set
            {
                this.status = value;
                NotifyPropertyChanged("Status");
            }
        }
        volatile private bool connected;
        public bool isConnected
        {
            get { return this.connected; }
            set
            {
                this.connected = value;
            }
        }

        private double altimeter;
        public double Altimeter {
            get { return altimeter; }
            set
            {
                altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        
        }
        private double headingDeg;
        public double HeadingDeg
        {
            get { return headingDeg; }
            set
            {
                headingDeg = value;
                NotifyPropertyChanged("HeadingDeg");
            }
        }
        private double verticalSpeed;
        public double VerticalSpeed
        {
            get { return verticalSpeed; }
            set
            {
                verticalSpeed = value;
                NotifyPropertyChanged("VerticalSpeed");
            }
        }
        private double groundSpeed;
        public double GroungSpeed
        {
            get { return groundSpeed; }
            set
            {
                groundSpeed = value;
                NotifyPropertyChanged("GroungSpeed");
            }
        }
        private double airSpeed;
        public double AirSpeed
        {
            get { return airSpeed; }
            set
            {
                airSpeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }
        private double altitude;
        public double Altitude
        {
            get => altitude;
            set
            {
                altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }
        private double roolDeg;
        public double RollDeg
        {
            get { return roolDeg; }
            set
            {
                roolDeg = value;
                NotifyPropertyChanged("RollDeg");
            }
        }
        private double pitchDeg;
        public double PitchDeg
        {
            get { return pitchDeg; }
            set
            {
                pitchDeg = value;
                NotifyPropertyChanged("PitchDeg");
            }
        }
        private double throttle;
        public double Throttle {
            get { return throttle; } 
            set 
            { 
                throttle = value;
                changeInThrottle = true;
                //NotifyPropertyChanged("Throttle");
            } 
        }
        private double aileron;
        public double Aileron
        {
            get 
            { 
                return aileron;
            }
            set
            {
                aileron = value;
                changeInAileron = true;
                //NotifyPropertyChanged("Aileron"); 
            }
        }
        private double elevator;
        public double Elevator 
        { 
            get { return elevator; } 
            set
            {
                elevator = value;
                changeInElevator = true;
                //NotifyPropertyChanged("Elevator"); 
            }
        }
        private double rudder;
        public double Rudder { 
            get { return rudder; } 
            set 
            { 
                rudder = value;
                changeInRudder = true;
                NotifyPropertyChanged("Rudder"); 
            }
        }
        private double longitude;
        public double Longitude
        {
            get { return this.longitude; }
            set
            {
                if ((value > -180) && (value < 180))
                {
                    this.longitude = value;
                    this.Center = new Location(this.latitude, value);
                    NotifyPropertyChanged("Center");
                }
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return this.latitude; }
            set
            {
                if ((value > -90) && value < 90)
                {
                    this.latitude = value;
                    this.Center = new Location(value, this.longitude);
                    NotifyPropertyChanged("Center");
                }
            }
        }

        private Location center;
        public Location Center
        {
            get { return this.center; }
            set
            {
                this.center = value;
                NotifyPropertyChanged("Center");
            }
        }
        #endregion 
        public void NotifyPropertyChanged(string p)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        /// <summary>
        /// connects to the simulator.
        /// </summary>
        /// <param name="ip"></param> th ip of the simulator.
        /// <param name="port"></param> thr port number of the socket.
        public void connect(string ip, int port)
        {
            int check;
            check = client.connect(ip,port);
            if(check == 1)
            {
                this.stop = false;
                this.Status = "Connected To The Simulator";
                isConnected = true;
                NotifyPropertyChanged("isConnected");
                this.start();
            }
            if (check == 2)
            {
                this.Status = "Couldn't connect to the simulator,please check the server and try again.\n Check The IP and the Port";
                this.stop = true;
                isConnected = false;
                NotifyPropertyChanged("isConnected");

            }
            if( check == 3)
            {
                this.Status = "Couldn't connect, please try again check the server.";
                this.stop = true;
                isConnected = false;
                NotifyPropertyChanged("isConnected");

            }
        }
        /// <summary>
        /// disconnect method for the simulator.
        /// </summary>
        public void disconnect()
        {
            this.stop = true;
            client.disconnect();
            isConnected = false;
            NotifyPropertyChanged("isConnected");

        }
        /// <summary>
        ///  a method that runs a loop and communicates with the simulator server.
        /// </summary>
        public void start()
        {
            double tempaltitude, tempairspeed, temprolldeg, temppitchdeg, tempaltimeter, temphedin, tempground, tempvertical;
            int countIOERR=0;
            new Thread(delegate ()
            {
                while ((isConnected) && (stop == false))
            {
                // getting the info from the simulator.
                tempaltitude = Double.Parse(client.writeandread("get /instrumentation/gps/indicated-altitude-ft\n"));
                if ((Convert.ToInt32(tempaltitude) == -1111)|| (Convert.ToInt32(tempaltitude) == -1112))
                {
                    this.Status = "Couldn't Get Altitude. ";
                    if (Convert.ToInt32(tempaltitude) == -1112)
                    {
                        this.Status = "I/O Error, Coouldn't Communicate with server ";
                        countIOERR++;
                    }

                }
                else
                {
                    Altitude = tempaltitude;
                }
                tempairspeed = Double.Parse(client.writeandread("get /instrumentation/airspeed-indicator/indicated-speed-kt\n"));
                if (Convert.ToInt32(tempairspeed) == -1111)
                {
                    this.Status = "Couldn't Get AirSpeed.";
                }
                else
                {
                    AirSpeed = tempairspeed;
                }
                temprolldeg = Double.Parse(client.writeandread("get /instrumentation/attitude-indicator/internal-roll-deg\n"));
                if (Convert.ToInt32(temprolldeg) == -1111)
                {
                    this.Status = "Couldn't Get RollDeg.";
                }
                else
                {
                    RollDeg = temprolldeg;
                }
                temppitchdeg = Double.Parse(client.writeandread("get /instrumentation/attitude-indicator/internal-pitch-deg\n"));
                if (Convert.ToInt32(temppitchdeg) == -1111)
                {
                    this.Status = "Couldn't Get PitchDeg.";
                }
                else
                { PitchDeg = temppitchdeg;

                }
                tempaltimeter = Double.Parse(client.writeandread("get /instrumentation/altimeter/indicated-altitude-ft\n"));
                if (Convert.ToInt32(tempaltimeter) == -1111)
                {
                    this.Status = "Couldn't Get PitchDeg.";
                }
                else
                { Altimeter = tempaltimeter;

                }
                temphedin = Double.Parse(client.writeandread("get /instrumentation/heading-indicator/indicated-heading-deg\n"));
                if (Convert.ToInt32(temphedin) == -1111)
                {
                    this.Status = "Couldn't Get Heading Deg.";
                }
                else
                { 
                    HeadingDeg = temphedin;

                }
                tempground = Double.Parse(client.writeandread("get /instrumentation/gps/indicated-ground-speed-kt\n"));
                if (Convert.ToInt32(tempground) == -1111)
                {
                    this.Status = "Couldn't Get Ground Speed.";
                }
                else
                {
                    GroungSpeed = tempground;

                }
                tempvertical = Double.Parse(client.writeandread("get /instrumentation/gps/indicated-vertical-speed\n"));
                if (Convert.ToInt32(tempvertical) == -1111)
                {
                    this.Status = "Couldn't Get Vertical Speed.";
                }
                else
                {
                    VerticalSpeed = tempvertical;

                }
                


                //Altitude = Double.Parse(client.writeandread("get /instrumentation/gps/indicated-altitude-ft\n"));
                //AirSpeed = Double.Parse(client.writeandread("get /instrumentation/airspeed-indicator/indicated-speed-kt\n"));
                //RollDeg = Double.Parse(client.writeandread("get /instrumentation/attitude-indicator/internal-roll-deg\n"));
                //PitchDeg = Double.Parse(client.writeandread("get /instrumentation/attitude-indicator/internal-pitch-deg\n"));
                //Altimeter = Double.Parse(client.writeandread("get /instrumentation/altimeter/indicated-altitude-ft\n"));
                //HeadingDeg = Double.Parse(client.writeandread("get /instrumentation/heading-indicator/indicated-heading-deg\n"));
                //GroungSpeed = Double.Parse(client.writeandread("get /instrumentation/gps/indicated-ground-speed-kt\n"));
                //VerticalSpeed = Double.Parse(client.writeandread("get /instrumentation/gps/indicated-vertical-speed\n"));
                Longitude= Double.Parse(client.writeandread("get /position/longitude-deg\n"));
                Latitude = Double.Parse(client.writeandread("get /position/latitude-deg\n"));

                if (changeInAileron == true)
                {
                    client.write("set /controls/flight/aileron " + aileron.ToString() + "\n");
                    changeInAileron = false;
                }
                if (changeInRudder == true)
                {
                    client.write("set /controls/flight/rudder " + rudder.ToString() + "\n");
                    changeInRudder = false;
                }
                if (changeInThrottle == true)
                {
                    client.write("set /controls/engines/current-engine/throttle " + throttle.ToString() + "\n");
                    changeInThrottle = false;
                }
                if (changeInElevator == true)
                {
                    client.write("set /controls/flight/elevator " + elevator.ToString() + "\n");
                    changeInElevator = false;
                }
                Thread.Sleep(250);
                if (countIOERR > 5)
                {
                    this.disconnect();
                    
                }
            }
        }

            ).Start();
    }


}
}
