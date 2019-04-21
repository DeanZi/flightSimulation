using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
