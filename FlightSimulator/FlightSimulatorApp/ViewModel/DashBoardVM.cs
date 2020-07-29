using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModel
{
    public class DashBoardVM:IVM
    {
        public FlightSimulator.Model.IFSModel model;
        #region Properties
        public double VM_Altimeter { get { return model.Altimeter;} }
        public double VM_HeadingDeg { get { return model.HeadingDeg; } }
        public double VM_VerticalSpeed { get { return model.VerticalSpeed; } }

        public String VM_Status
        {
            get { return model.Status; }
            set
            {
                this.model.Status = value;
            }
        }
        public bool VM_isConnected { get { return model.isConnected; } }

        public double VM_GroungSpeed { get { return model.GroungSpeed; }  }

        public double VM_AirSpeed { get { return model.AirSpeed; } }

        public double VM_Altitude { get { return model.Altitude; } }

        public double VM_RollDeg { get { return model.RollDeg; } }

        public double VM_PitchDeg { get { return model.PitchDeg; }  }
        #endregion
        public DashBoardVM(FlightSimulator.Model.IFSModel flightSimulatorModel)
        {
            this.model = flightSimulatorModel;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            { NotifyPropertyChanged("VM_" + e.PropertyName);};
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// notifies when a propert is canges using the INotifyPropertyChanged
        /// </summary>
        /// <param name="p"></param> the name of the prperty.
        public void NotifyPropertyChanged(string p)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
       
    }
}