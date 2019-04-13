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
        Info flightInfoModel = new Info();
        
        public void Connect()
        {
            flightInfoModel.MainServer();
           // this.NotifyPropertyChanged("Lat");
            
        }

        public double Lon
        {
            get
            {
                return flightInfoModel.lanLon[0];
            }
        }

        public double Lat
        {
            get
            {
                return flightInfoModel.lanLon[1];
            }
        }
    }
}
