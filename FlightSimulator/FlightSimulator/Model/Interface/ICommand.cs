using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface ICommandModel
    {
        void Start(string input, string flightServerIP, int flightCommandPort);
    }
}
