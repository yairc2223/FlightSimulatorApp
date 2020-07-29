using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Text.RegularExpressions;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Regex regValidIp;
        bool isConnected;
        bool ipToConn;
        bool portToConn;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new
            {
                map = (Application.Current as App).Map,
                dash = (Application.Current as App).Dash,
                controls = (Application.Current as App).Controls,
            };
            this.regValidIp = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            isConnected = false;
            ipToConn = true;
            portToConn = true;


        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            //string ip1 = "127.0.0.1";
            //int port1 = 5402;
            // Check if the application is connected to the simulator
            isConnected = (Application.Current as App).Dash.VM_isConnected;
            if (!isConnected)
            {
                //If given connection details are valid, attempt to connect
                if (isValidIp(IPTextbox.Text) && isValidPort(PortTextbox.Text))
                {
                    (Application.Current as App).Dash.VM_Status = "Valid IP and Port";
                    string ip = IPTextbox.Text;
                    int port = Int32.Parse(PortTextbox.Text);
                    (Application.Current as App).Dash.model.connect(ip, port);
                    //(Application.Current as App).Dash.model.start();
                    //(Application.Current as App).window.ControlPanel.restartControls();
                }
                else
                {
                    (Application.Current as App).Dash.VM_Status = "Invalid connection info";
                    if (!ipToConn)
                    {
                        IPTextbox.Text = "127.0.0.1";
                    }
                    if (!portToConn)
                    {
                        PortTextbox.Text = "5402";
                    }
                }
            }
            else
            {
                (Application.Current as App).Dash.VM_Status = "Already Connected!";
            }

        }

        //Event handler for clicking the 'Connect' button
       
        //Event handler for clicking the 'Disconnect' button
        // Method to check given IP validity
        private bool isValidIp(string ip)
        {
            if (!string.IsNullOrEmpty(ip))
            {
                try
                {
                    Match match = this.regValidIp.Match(ip);
                    if (match.Success)
                    {
                        (Application.Current as App).Dash.VM_Status = "Valid IP";
                        ipToConn = true;
                        return true;
                    }
                }
                catch (ArgumentException)
                {
                    ipToConn = false;
                    return false;
                }
            }
            ipToConn = false;
            return false;
        }
        // Method to check given port validity
        private bool isValidPort(string port)
        {
            if (!string.IsNullOrEmpty(port))
            {
                int validPort;
                bool isValid = int.TryParse(port, out validPort);
                if (isValid)
                {
                    if (validPort < 0 || validPort > 65535)
                    {
                        portToConn = false;
                        (Application.Current as App).Dash.VM_Status = "Port invalid: Negative or above 65535";
                        return false;
                    }
                    portToConn = true;
                    (Application.Current as App).Dash.VM_Status = "Port valid!";
                    return true;
                }
                portToConn = false;
                (Application.Current as App).Dash.VM_Status = "Port invalid.\nTry again in a few moments.";
                return false;
            }
            portToConn = false;
            (Application.Current as App).Dash.VM_Status = "Port invalid: Null or Empty";
            return false;
        }

        private void DisconnectButton_Click_1(object sender, RoutedEventArgs e)
        {
            // Check if the application is connected to the simulator
            isConnected = (Application.Current as App).Dash.VM_isConnected;
            if (isConnected)
            {
                (Application.Current as App).Dash.model.disconnect();
                (Application.Current as App).Dash.VM_Status = "Disconnected!";
                //(Application.Current as App).window.ControlPanel.restartControls();
            }
            else
            {
                (Application.Current as App).Dash.VM_Status = "Already Disconnected!";
            }
        }
    }
}
