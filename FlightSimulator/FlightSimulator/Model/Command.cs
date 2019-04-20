using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FlightSimulator.Model
{ 

public class TcpClientSample
{
    public void start()
    {
        byte[] data = new byte[1024];
        string input, stringData;
        TcpClient server;

        try
        {
            server = new TcpClient("127.0.0.1", 5402);
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

        while (true)
        {
            input = "set controls/flight/aileron -1";
                if (input == "exit")
                break;
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
        }
        Console.WriteLine("Disconnecting from server...");
        ns.Close();
        server.Close();
    }
}
}