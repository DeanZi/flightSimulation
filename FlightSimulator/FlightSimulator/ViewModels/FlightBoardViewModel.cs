using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        public Info flightInfoModel;
        
        public void Connect()
        {
            Task task2 = Task.Factory.StartNew(()=> flightInfoModel = new Info(false));
            while (true)
            {
                if (flightInfoModel != null)
                {
                    if (flightInfoModel.flag)
                    {
                        Task task3 = Task.Factory.StartNew(() =>
                        {
                            Lon = flightInfoModel.lanLon[0];
                            Lat = flightInfoModel.lanLon[1];
                        });
                    }
                }
            }
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
