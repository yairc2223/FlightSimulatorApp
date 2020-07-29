using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    /// <summary>
    /// interface for model layer.
    /// </summary>
    public interface IFSModel : INotifyPropertyChanged
    {
        /// <summary>
        /// connects to the simulator.
        /// </summary>
        /// <param name="ip"></param> th ip of the simulator.
        /// <param name="port"></param> thr port number of the socket.
        void connect(string ip, int port);
        /// <summary>
        /// disconnect method for the simulator.
        /// </summary>
        void disconnect();
        /// <summary>
        ///  a method that runs a loop and communicates with the simulator server.
        /// </summary>
        void start();
        
//Properties

        double Altimeter { set; get; }
        double HeadingDeg { set; get; }
        double VerticalSpeed { set; get; }
        double GroungSpeed { set; get; }
        double AirSpeed { set; get; }
        double Altitude { set; get; }
        double RollDeg { set; get; }
        double PitchDeg { set; get; }
        
        double Throttle { set; get; }
        double Aileron { set; get; }
        double Elevator { set; get; }
        double Rudder { set; get; }
        double Longitude { set; get; }
        double Latitude { set; get; }
        Location Center { set; get; }
        String Status { set; get; }
        bool isConnected { set; get; }

    }
}
