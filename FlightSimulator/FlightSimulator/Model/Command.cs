using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FlightSimulator.Model
{ 

public class Command
{
        public TcpClient server;


        public Command( string flightServerIP, int flightCommandPort)

        {
            if (server == null || !server.Connected)
            {
                try
                {
                    server = new TcpClient(flightServerIP, flightCommandPort);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Unable to connect to server");
                    return;
                }
            }
           // start(input, flightServerIP, flightCommandPort);
        }


    public void start(string input)
    {
        byte[] data = new byte[1024];
        NetworkStream ns;
            if (server != null)
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