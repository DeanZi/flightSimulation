using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;

namespace FlightSimulator.Model.Interface
{ 

public class Command: ICommandModel
    {

        private static ICommandModel c_Instance = null;
        public static ICommandModel Instance
        {
            get
            {
                if (c_Instance == null)
                {
                    c_Instance = new Command("set /controls/flight/elevator 0", ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);
                }
                return c_Instance;
            }
        }

        public Command(string input, string flightServerIP, int flightCommandPort)
        {
            Start(input, flightServerIP, flightCommandPort);
        }
    public void Start(string input, string flightServerIP, int flightCommandPort)
    {
        byte[] data = new byte[1024];
       
        TcpClient server;

        try
        {
            server = new TcpClient(flightServerIP, flightCommandPort);
        }
        catch (SocketException)
        {
            Console.WriteLine("Unable to connect to server");
            return;
        }
        NetworkStream ns = server.GetStream();

       /* int recv = ns.Read(data, 0, data.Length);
        stringData = Encoding.ASCII.GetString(data, 0, recv);
        Console.WriteLine(stringData);*/

       // while (true)
        //{
            //if (input == "exit")
              //  break;
            string[] cmds = input.Split('\n');
            foreach (string cmd in cmds) {
                    string tmpCmd = cmd + "\r\n";
                    ns.Write(Encoding.ASCII.GetBytes(tmpCmd), 0, tmpCmd.Length);
                    ns.Flush();
            }

         /*   data = new byte[1024];
            recv = ns.Read(data, 0, data.Length);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);*/
        //}
        //Console.WriteLine("Disconnecting from server...");
        ns.Close();
        server.Close();
    }
}
}