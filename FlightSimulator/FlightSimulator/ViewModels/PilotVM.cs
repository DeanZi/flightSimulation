using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using FlightSimulator.Model;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.ViewModels
{
    //View models for both Auto Pilot and Manual views
    class PilotVM : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        //Sends all comands created in manual view to the server
        public void ManualAction(string setting)
        {
            NotifyPropertyChanged(setting);
            new Thread(delegate ()
            {
                Command.Instance.Start(setting, ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);
            }).Start();    
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Brush _bgcolor= Brushes.White;
        public Brush BGColor
        {
            get
            {
                return _bgcolor;
            }
            set
            {
                _bgcolor = value;
                NotifyPropertyChanged("BGColor");
            }
        }

        private string _input;

        public string Input
        {
            get { return _input; }
            set
            {
                {
                    _input = value;
                    if (string.IsNullOrEmpty(Input))
                        BGColor = Brushes.White;
                    else BGColor = Brushes.LightPink;

                }
            }
        }
        private Command cmd;

        private CommandHandler _okCommand;
        public CommandHandler OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => OkAction()));
            }
        }
        public void OkAction()
        {
            string currentCommand = Input;
            Input = "";
            BGColor = Brushes.White;
            NotifyPropertyChanged(Input);
            //Take current commands and send it to server
            new Thread(delegate ()
            {
                Command.Instance.Start(currentCommand, ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);
            }).Start();
        }

        private CommandHandler _clearCommand;
        public CommandHandler ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => ClearAction()));
            }
        }

        private void ClearAction()
        {
            Input = "";
            BGColor = Brushes.White;
            NotifyPropertyChanged(Input);
        }
    }
}
