using FlightSimulator.Model;
using FlightSimulator.ViewModels.Windows;
using FlightSimulator.Views;
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
        public Settings settingsWindow = new Settings();

        private CommandHandler _settingsCommand;
        public CommandHandler SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => Settings()));
            }
        }

        private void Settings()
        {
            if (settingsWindow.IsLoaded)
                return;
            settingsWindow.Show();
                
        }

        private CommandHandler _connectCommand;
        public CommandHandler ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => Connect()));
            }
        }
       // Task task2 = Task.Factory.StartNew(() => FlightBoardVModel.Connect());


        public void Connect()
        {
            flightInfoModel = new Info(false);
            if (flightInfoModel.flag)
            {
                flightInfoModel.MainServer("stop", "0", "0");
                Task task2 = Task.Factory.StartNew(() => flightInfoModel.MainServer("start", ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort));
                Task task3 = Task.Factory.StartNew(() => Update());
            }

            
        }

        public void Update()
        {
            while (true)
            {
                if (flightInfoModel != null)
                {

                    if (flightInfoModel.flag)
                    {
                       
                        
                            try
                            {
                                Lon = flightInfoModel.lanLon[0];
                                Lat = flightInfoModel.lanLon[1];
                            }
                            catch (Exception e) { }
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
