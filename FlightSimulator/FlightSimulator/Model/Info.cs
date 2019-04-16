using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;



namespace FlightSimulator.Model
{
    public class Info
    {
        public bool flag = false;
        public double[] lanLon = new double[2];

        public Info(bool started)
        {
            if (started == false)
            {
                started = true;
                MainServer();
            }

        }
        private void MainServer()
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5400);
            // set our IP address as server's address, and we also set the port: 9999

            server.Start();  // start the server

            while (true)   //wait for a connection
            {
                TcpClient client = server.AcceptTcpClient();  //if a connection exists, the server will accept it
                Console.WriteLine("1 connected"); //now , we write the message as string

                NetworkStream ns = client.GetStream(); //networkstream is used to send/receive messages

                byte[] hello = new byte[100];   //any message must be serialized (converted to byte array)
                hello = Encoding.Default.GetBytes("hello world");  //conversion string => byte array

                ns.Write(hello, 0, hello.Length);     //sending the message

                connect(ns, client);

            }
        }


        private void connect(NetworkStream ns, TcpClient client)
        {
            while (client.Connected)  //while the client is connected, we look for incoming messages
            {
                byte[] msg = new byte[1024];     //the messages arrive as byte array
                ns.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                char[] charsToTrim = { ' ', '?' };
                string phrase = Encoding.Default.GetString(msg).Trim(charsToTrim);
                Console.WriteLine(Encoding.Default.GetString(msg).Trim(charsToTrim)); //now , we write the message as string
                string[] details = phrase.Split(',');
                Update(Convert.ToDouble(details[0]), Convert.ToDouble(details[1]));
            }
        }

        private void Update(double x, double y)
        {
            this.lanLon[0] = x;
            this.lanLon[1] = y;
            flag = true;
        }
    }
}

