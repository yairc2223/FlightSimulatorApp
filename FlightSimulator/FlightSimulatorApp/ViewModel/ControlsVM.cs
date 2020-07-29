using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModel
{
    public class ControlsVM : IVM
    {
        public event PropertyChangedEventHandler PropertyChanged;
        FlightSimulator.Model.IFSModel model;
        public ControlsVM(Model.IFSModel flightSimulatorModel)
        {
            this.model = flightSimulatorModel;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            { NotifyPropertyChanged("VM_" + e.PropertyName); };
        }
        /// <summary>
        /// notifies when a propert is canges using the INotifyPropertyChanged
        /// </summary>
        /// <param name="p"></param> the name of the prperty.
        public void NotifyPropertyChanged(string p)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
        /// <summary>
        ///  a method that sets the joystick vals.
        /// </summary>
        /// <param name="rudder"></param>
        /// <param name="elevator"></param>
        public void setJoyValues(double rudder, double elevator)
        {
            model.Rudder = rudder;
            model.Elevator = elevator;
        }
        /// <summary>
        /// a method that sets  the throttle value
        /// </summary>
        /// <param name="aileron"></param>number of change.
        public void setAileron(double aileron)
        {
            model.Aileron = aileron;
        }
        /// <summary>
        /// a method that sets  the throttle value
        /// </summary>
        /// <param name="throttle"></param> number of change.
        public void setThrottle(double throttle)
        {
            model.Throttle = throttle;
        }
    }
}
