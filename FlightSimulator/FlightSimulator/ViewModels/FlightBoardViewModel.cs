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
        public Info flightInfoModel=new Info(false);
        
        public void Connect()
        {
            Task task2 = Task.Factory.StartNew(()=> flightInfoModel.MainServer());
            while (true)
            {
                if (flightInfoModel != null)
                {
                    
                    if (flightInfoModel.flag)
                    {
                        Task task3 = Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                Lon = flightInfoModel.lanLon[0];
                                Lat = flightInfoModel.lanLon[1];
                            }
                            catch(Exception e) { }
                        });
                    }
                }
            }
        }
        private double lon;
        public double Lon
        {
            get
            {
                return lon;
            }

            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");

            }
        }
        private double lat;
        public double Lat
        {
            get
            {

                return lat;
            }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lon");

            }
        }
    }
}
