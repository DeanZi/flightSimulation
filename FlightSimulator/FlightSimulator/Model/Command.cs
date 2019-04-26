using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;

namespace FlightSimulator.Model.Interface
{ 
    /// <summary>
    /// 
    /// Command Channel
    /// </summary>
public class Command: ICommandModel
    {
        /// <summary>
        /// singleton design pattern - we want multiple users of the same instance
        /// </summary>
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

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="input">the command to be sent</param>
        /// <param name="flightServerIP">the ip address of the server - flightgear simulator </param>
        /// <param name="flightCommandPort">the port in which we will open a socket connection </param>

        public Command(string input, string flightServerIP, int flightCommandPort)
        {
            Start(input, flightServerIP, flightCommandPort);
        }


        /// <summary>
        /// the actual connection function
        /// when invoked - it will send to the server the actual command
        /// </summary>
        /// <param name="input"></param>
        /// <param name="flightServerIP"></param>
        /// <param name="flightCommandPort"></param>

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
            string[] cmds = input.Split('\n');
            foreach (string cmd in cmds)
                {
                string tmpCmd = cmd + "\r\n";
                ns.Write(Encoding.ASCII.GetBytes(tmpCmd), 0, tmpCmd.Length);
                ns.Flush();
                }
            ns.Close();
            server.Close();
        }
    }
}