using FlightSimulator.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Pilot.xaml
    /// </summary>
    public partial class Pilot : UserControl
    {
        public Pilot()
        {
            PilotVM pilotVM;
            InitializeComponent();
            DataContext = pilotVM = new PilotVM();
        }
    }
}
