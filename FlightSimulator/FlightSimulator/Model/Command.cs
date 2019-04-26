using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FlightSimulator.Model
{ 

public class Command
{
        private TcpClient server;
        private static string flightServerIP;
        private static int flightCommandPort;
        public Boolean connected = false;

        private static Command c_Instance = null;
        public static Command Instance
        {
            get
            {
                if (c_Instance == null)
                {
                    c_Instance = new Command();
                    flightServerIP = ApplicationSettingsModel.Instance.FlightServerIP;
                    flightCommandPort = ApplicationSettingsModel.Instance.FlightCommandPort;
                  
                }
                return c_Instance;
            }
        }

        public void Connect( )

        {
            while (server == null || !server.Connected)
            {
                try
                {
                    //if(flightServerIP!=null)
                        server = new TcpClient(flightServerIP, flightCommandPort);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Unable to connect to server");
                }
            }
            connected = true;
        }


    public void start(string input)
    {
        byte[] data = new byte[1024];
        NetworkStream ns;
            if (server != null && server.Connected)
            {
                ns = server.GetStream();

                /* int recv = ns.Read(data, 0, data.Length);
                 stringData = Encoding.ASCII.GetString(data, 0, recv);
                 Console.WriteLine(stringData);*/

                // while (true)
                //{
                //if (input == "exit")
                //  break;
                string[] cmds = input.Split('\n');
                foreach (string cmd in cmds)
                {
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
                //server.Close();
            }
    }
}
}