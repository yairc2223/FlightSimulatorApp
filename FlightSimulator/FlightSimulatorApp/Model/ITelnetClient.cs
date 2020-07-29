using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    /// <summary>
    /// This is an Interface that represents generic telnet clients.
    /// </summary>
    interface ITelnetClient
    {

        /// <summary>
        /// this is an acces method with ag  get and aset for port number.
        /// </summary>
        int Port { get; set; }
        /// <summary>
        /// this is an acces method with a  get and a set for IP.
        /// </summary>
        string IP { get; set; }
        /// <summary>
        ///  a method that creates a socket and connect it.
        /// </summary>
        /// <param name="ip"></param> the ip of the connection.
        /// <param name="port"></param> thee port number of the socket.
        /// <returns></returns> whether the proccess had suceeded.
        int connect(string ip, int port);
        /// <summary>
        /// a method that sends commands and read the feeddback.
        /// </summary>
        /// <param name="command"></param> the string command.
        /// <returns></returns> the feedback.
        string writeandread(string command);
        /// <summary>
        /// a method that sends acommand to the socket.
        /// </summary>
        /// <param name="command"></param> the string command.
        void write(string command);
        /// <summary>
        /// reads from the dreated socket, a blocking call.
        /// </summary>
        /// <returns></returns> the output from the socket.
        string read();
        /// <summary>
        /// closes the connection between the client and the server.
        /// </summary>
        void disconnect();
       
    }
}
