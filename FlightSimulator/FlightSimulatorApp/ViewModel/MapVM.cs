using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModel
{
    public class MapVM : IVM
    {
        public event PropertyChangedEventHandler PropertyChanged;
        FlightSimulator.Model.IFSModel model;
        public MapVM(Model.IFSModel flightSimulatorModel)
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
        /// prpert for the vmlocation of the bing map center.
        /// </summary>
        public Location VM_Center
        {
            get
            {
                Location locationairp = model.Center;
                return locationairp != null ? locationairp : new Location(model.Latitude, model.Longitude);
            }
        }
    }
}
