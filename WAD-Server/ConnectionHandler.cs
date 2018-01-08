using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WAD_Server
{
    class ConnectionHandler
    {
        private Socket client;
        private NetworkStream ns;
        private StreamReader reader;
        private StreamWriter writer;
        private static int connections = 0;
        private Form1 f;
        
        // Current way to keep track of multi socket (can be changed to dict<int, socket>)
        public static ArrayList arrSocket = new ArrayList();

        public ConnectionHandler(Socket client, Form1 f)
        {
            this.client = client;
            this.f = f;
        }

        public void HandleConnection(Object state)
        {
            try
            {
                ns = new NetworkStream(client);
                reader = new StreamReader(ns);
                writer = new StreamWriter(ns);
                connections++;
                arrSocket.Add(client);

                f.SetText("New client accepted : " + connections + " active connections");

                string input;
                while (true)
                {
                    input = reader.ReadLine();

                    if (input.ToLower() == "terminate")
                        break;
                    f.SetText("Client >> " + input);
                }
                ns.Close();
                client.Close();
                connections--;
                f.SetText("Client disconnected : " + connections + " active connections");
            }
            catch (Exception)
            {
                connections--;
                f.SetText("Client disconnected : " + connections + " active connections");
            }
        }
    }
}
