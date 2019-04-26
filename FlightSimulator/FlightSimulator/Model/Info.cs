using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;



namespace FlightSimulator.Model
{/// <summary>
/// Info Channel of connection
/// </summary>
    public class Info
    {
        /// <summary>
        /// singleton design pattern - we want only once to have a server in which we recieve data at
        /// </summary>
        /// a flag to let us know if there's an instance of INFO already
        public bool flag = false;
        // will save the first two given pieces of data from the input stream (that comes from the client - simulator)
        //they are the Lat & Lon to be used down at the flightBoarViewModel and then down to the view.
        public double[] latLon = new double[2];

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="started"></param>
        public Info(bool started)
        {
            if (started == false)
            {
                flag = true;
            }

        }
        /// <summary>
        /// where our server connects to (client - simulator) and recieves data from it.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="flightServerIP"></param>
        /// <param name="FlightInfoPort"></param>
        public void MainServer(string input, string flightServerIP, int FlightInfoPort)
        {
            TcpListener server = new TcpListener(IPAddress.Parse(flightServerIP), FlightInfoPort);
            if(input=="start")
                server.Start();  // start or stop the server
            if (input == "stop")
                server.Stop();
            while (true && input== "start")   //wait for the connection
            {
                TcpClient client = server.AcceptTcpClient();  //when a connection is available, our server will accept it
                Console.WriteLine("connection established");  

                NetworkStream ns = client.GetStream(); //an object that can contain the clients messages

                byte[] hello = new byte[100];  // byte array
                hello = Encoding.Default.GetBytes("hello flightgear");  //string into the byte array

                ns.Write(hello, 0, hello.Length);     //sending our hello message
                // call the below function to figure out the clients message - (input) Networkstream
                connect(ns, client);

            }
        }


        private void connect(NetworkStream ns, TcpClient client)
        {
            while (client.Connected)  //while the client is connected, we look for incoming messages
            {
                byte[] msg = new byte[1024];     // byte array
                ns.Read(msg, 0, msg.Length);   //the networkstream now reads what is being sent from the client
                char[] charsToTrim = {' ', '?' };
                string phrase = Encoding.Default.GetString(msg).Trim(charsToTrim);
                Console.WriteLine(phrase); //we print the filtered input from the client
                string[] details = phrase.Split(',', '\n');
                if (details[0] != "" && details[1] != "") 
                    Update(Convert.ToDouble(details[0]), Convert.ToDouble(details[1]));//we take the interesting first two
                //pieces of information which stands for Lat and Lon and update our fields accordinglly.
            }
        }

        private void Update(double x, double y)
        {
            this.latLon[0] = x;
            this.latLon[1] = y;
            flag = true;
        }
    }
}

