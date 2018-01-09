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

                //f.SetText("New client accepted : " + connections + " active connections");

                string input;
                while (true)
                {
                    input = reader.ReadLine();

                    if (input.ToLower() == "login")
                    {
                        Authorize();
                    }

                    else if (input.ToLower() == "terminate")
                        break;
                    //f.SetText("Client >> " + input);
                }
                ns.Close();
                client.Close();
                connections--;
                //f.SetText("Client disconnected : " + connections + " active connections");
            }
            catch (Exception)
            {
                connections--;
                //f.SetText("Client disconnected : " + connections + " active connections");
            }
        }

        //basic authorize without checks
        public void Authorize()
        {
            ns = new NetworkStream(client);
            reader = new StreamReader(ns);
            writer = new StreamWriter(ns);
            writer.AutoFlush = true;

            try
            {
                string email = reader.ReadLine();
                string password = reader.ReadLine();
                bool authorized = false;

                user userClass = new user();
                List<user> calledList = userClass.GetList();
                
                foreach (user details in calledList)
                {
                    if ((details.getEmail() == email) && (details.getPassword() == password))
                    {
                        authorized = true;
                        writer.WriteLine("authorized");
                        writer.WriteLine(details.getFirstName());
                        writer.WriteLine(details.getMiddleName());
                        writer.WriteLine(details.getLastName());
                        writer.WriteLine(details.getDOB());
                        writer.Flush();
                    }
                }

                if (!authorized)
                {
                    writer.WriteLine("unauthorized");
                    writer.Flush();
                }
            }
            catch (Exception)
            {
                // some error when reading line (client disconnected etc)
            }
        }
    }
}
