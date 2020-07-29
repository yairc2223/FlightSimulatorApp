using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ViewModel.MapVM Map { get; internal set; }
        public ViewModel.DashBoardVM Dash { get; internal set; }
        public ViewModel.ControlsVM Controls { get; internal set; }
        public MainWindow window;
        void App_Startup(object sender, StartupEventArgs e)
        {

            Model.FSClient client = new Model.FSClient();
            Model.MFSModel model = new Model.MFSModel(client);
            Controls = new ViewModel.ControlsVM(model);
            Dash = new ViewModel.DashBoardVM(model);
            Map = new ViewModel.MapVM(model);
            window = new MainWindow();
            window.Show();
        }
    }
}
