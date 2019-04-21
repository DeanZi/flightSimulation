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
    class AutoPilotVM
    {


        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private SolidColorBrush _bgcolor;
        public SolidColorBrush BGColor
        {
            get
            {
                return _bgcolor;
            }
            set
            {
                _bgcolor = value;
                OnPropertyChanged("BGColor");
            }
        }

        private string _input;

        public string Input
        {
            get { return _input; }
            set
            {
                if (value != _input)
                {
                    _input = value;
                    OnPropertyChanged("Input");
                }
            }
        }
        private Command cmd;
       
        
        public void TakeCommand()
        {
            cmd = new Command(_input);
        }

    }
}
