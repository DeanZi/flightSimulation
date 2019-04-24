using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModels
{
    class ManualVM : INotifyPropertyChanged
    {
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string throttle;
        public double Throttle
        {
            set
            {
                throttle = "set /controls/engines/current-engine/throttle " + Convert.ToString(value);
                NotifyPropertyChanged("Throttle");
                ManualAction(throttle);
            }
        }

        private string aileron;
        public double Aileron
        {
            set
            {
                aileron = "set /controls/flight/aileron " + Convert.ToString(value);
                NotifyPropertyChanged("Aileron");
                ManualAction(aileron);
            }
        }

        private string rudder;
        public double Rudder
        {
            set
            {
                rudder = "set /controls/flight/rudder " + Convert.ToString(value);
                NotifyPropertyChanged("Rudder");
                ManualAction(rudder);
            }
        }

        private string elevator;
        public double Elevator
        {
            set
            {
                elevator = "set /controls/flight/elevator " + Convert.ToString(value);
                NotifyPropertyChanged("Elevator");
                ManualAction(elevator);
            }
        }

        private Command cmd;

        public event PropertyChangedEventHandler PropertyChanged;

        public void ManualAction(string setting)
        {
            string currentCommand = setting;
            NotifyPropertyChanged(currentCommand);
            cmd = new Command(currentCommand, ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);
        }
    }
}
