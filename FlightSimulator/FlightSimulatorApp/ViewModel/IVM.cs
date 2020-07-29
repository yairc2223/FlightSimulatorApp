using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModel
{
    public interface IVM:INotifyPropertyChanged
    {
        /// <summary>
        /// notifies when a propert is canges using the INotifyPropertyChanged
        /// </summary>
        /// <param name="p"></param> the name of the prperty.
        void NotifyPropertyChanged(string p);
    }
}
