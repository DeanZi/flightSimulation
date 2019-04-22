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
    class AutoPilotVM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;


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
            cmd = new Command(currentCommand, ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);

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
