using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;

namespace FlightSimulator.Model
{
    class FSClient : ITelnetClient
    {
		public TcpClient tcpclnt;
		public NetworkStream networkStream;
		/// <summary>
		/// constructor for FSclient
		/// </summary>
		public FSClient()
		{
			//tcpclnt = new TcpClient();
		}

		/// <summary>
		/// this is an acces method with ag  get and aset for port number.
		/// </summary>
		public int Port { get { return this.Port; } set { this.Port = value; } }
		/// <summary>
		/// this is an acces method with a  get and a set for IP.
		/// </summary>
		public string IP { get { return this.IP; } set { this.IP = value; } }
		/// <summary>
		///  a method that creates a socket and connect it.
		/// </summary>
		/// <param name="ip"></param> the ip of the connection.
		/// <param name="port"></param> thee port number of the socket.
		/// <returns></returns> whether the proccess had suceeded.
		public int connect(string ip, int port)
		{

			try
			{
				tcpclnt = new TcpClient();
				Console.WriteLine("Connecting.....");
				tcpclnt.Connect(ip,port);
				this.networkStream = tcpclnt.GetStream();
				this.networkStream.ReadTimeout = 10000;
				return 1;
			}
			catch (SocketException)
			{
				return 2;
			}
			catch (Exception)
			{
				return 3;
			}
	

		}
		/// <summary>
		/// closes the connection between the client and the server.
		/// </summary>
		public void disconnect()
        {
			tcpclnt.Close();

		}
		/// <summary>
		/// reads from the dreated socket, a blocking call.
		/// </summary>
		/// <returns></returns> the output from the socket.
		public string read()
        {
			byte[] bb = new byte[100];
			this.networkStream = tcpclnt.GetStream();
			int k = this.networkStream.Read(bb, 0, 100);
			string j = "";
			for (int i = 0; i < k; i++)
			{
				j += (Convert.ToChar(bb[i]));

			}
			if (j.CompareTo("ERR") == 0) { return "0.0"; }
			return j;
		}
		/// <summary>
		/// a method that sends commands and read the feeddback.
		/// </summary>
		/// <param name="command"></param> the string command.
		/// <returns></returns> the feedback.
		public string writeandread(string command)
		{

			try
			{
				String str = command;
				ASCIIEncoding asen = new ASCIIEncoding();
				byte[] ba = asen.GetBytes(str);
				//Console.WriteLine("Transmitting.....");
				this.networkStream.Write(ba, 0, ba.Length);

				byte[] bb = new byte[256];
				int k = this.networkStream.Read(bb, 0, 256);
				string j = "";
				for (int i = 0; i < k; i++)
				{
					j += (Convert.ToChar(bb[i]));

				}
				if (j.Contains("ERR")) { return "-1111.0"; }
				return j;
			}
			catch (ArgumentNullException)
			{
				return "-1111.0";
			}
			catch (Exception)
			{
				return "-1112.0";
			}
			
		}
		/// <summary>
		/// a method that sends acommand to the socket.
		/// </summary>
		/// <param name="command"></param> the string command.
		public void write(string command)
		{
			try
			{
				String str = command;
				ASCIIEncoding asen = new ASCIIEncoding();
				byte[] ba = asen.GetBytes(str);
				byte[] rcvbytes = new byte[256];
				//Console.WriteLine("Transmitting.....");
				this.networkStream.Write(ba, 0, ba.Length);
				 this.networkStream.Read(rcvbytes, 0, 256);
				byte[] bb = new byte[100];
			}
			catch(Exception)
			{
				this.disconnect();
			}
			
		}
	}
}
