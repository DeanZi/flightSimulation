using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        Info flightInfoModel;
        


        public void Connect()
        {
            flightInfoModel = new Info();


        }

        public double Lon
        {
            get
            {
                return flightInfoModel.lanLon[0];
            }

            set
            {
                NotifyPropertyChanged("Lon");

            }
        }

        public double Lat
        {
            get
            {
                return flightInfoModel.lanLon[1];
            }
            set
            {
                NotifyPropertyChanged("Lon");

            }
        }
    }
}
