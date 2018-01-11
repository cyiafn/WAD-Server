using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        //public static ArrayList arrSocket = new ArrayList();

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
                //arrSocket.Add(client);

                f.SetText("New client accepted : " + connections + " active connections");

                string input;
                while (true)
                {
                    input = reader.ReadLine();

                    if (input.ToLower() == "login")
                    {
                        Authorize();
                    }
                    else if (input.ToLower() == "request_movie")
                    {
                        SendMovieList();
                    }
                    else if (input.ToLower() == "book_movie")
                    {
                        AddClientBooking();
                    }
                    else if (input.ToLower() == "view_booking")
                    {
                        ViewClientBooking();
                    }
                    else if (input.ToLower() == "search_movie")
                    {
                        SearchMovie();
                    }
                    else if (input.ToLower() == "register")
                    {
                        Register();
                    }
                    else if (input.ToLower() == "terminate")
                        break;
                    //f.SetText("Client >> " + input);
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

        //basic authorize without checks
        public void Authorize()
        {
            ns = new NetworkStream(client);
            writer = new StreamWriter(ns);
            writer.AutoFlush = true;

            try
            {
                string email = reader.ReadLine();
                string password = reader.ReadLine();
                bool authorized = false;
                
                foreach (user details in variables.userList)
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
                        break;
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
                f.SetText("Exception occured on login");
                // some error when reading line (client disconnected etc)
            }
        }

        //basic register
        public void Register()
        {
            ns = new NetworkStream(client);
            writer = new StreamWriter(ns);
            writer.AutoFlush = true;

            try
            {
                string email = reader.ReadLine();
                string password = reader.ReadLine();
                string firstName = reader.ReadLine();
                string middleName = reader.ReadLine();
                string lastName = reader.ReadLine();
                string dob = reader.ReadLine();

                user newUser = new user();
                newUser.intializeUser(firstName, middleName, lastName, email, password, dob);

                bool exist = variables.userList.Contains(newUser);
                if (exist)
                {
                    writer.WriteLine("fail");
                    return;
                }
                variables.userList.Add(newUser);
                writer.WriteLine("success");
            }
            catch (Exception)
            {
                f.SetText("Exception occured on register.");
            }
        }

        // Send list of movies to client
        public void SendMovieList()
        {
            ns = new NetworkStream(client);
            writer = new StreamWriter(ns);
            writer.AutoFlush = true;

            //byte[] fileNameByte;
            //byte[] fileData;
            try
            {
                var xs = new XmlSerializer(typeof(HashSet<Movie>));
                string xml;
                using (var writer = new StringWriter())
                {
                    xs.Serialize(writer, variables.movieList);
                    xml = writer.ToString();
                    writer.WriteLine(xml);
                }

                //using (var reader = new StringReader(xml))
                //{
                //    var set2 = (HashSet<Movie>)xs.Deserialize(reader);
                //    foreach (Movie s in set2)
                //    {
                //        f.SetText("SendMovieList()");
                //    }
                //}

                //foreach (Movie item in variables.movieList)
                //{
                //    // If Movie is showing, send info. (?)
                //    if (item.Status == true)
                //    {
                //        fileNameByte = Encoding.ASCII.GetBytes(item.ImageFileName);
                //        fileData = File.ReadAllBytes(item.ImageFileName);

                //        // sends the title, filename, file data bytes and file data
                //        writer.WriteLine(item.Title);
                //        writer.WriteLine(item.ImageFileName);
                //        writer.WriteLine(fileData.Length);
                //        client.Send(fileData);
                //    }
                //}
                writer.WriteLine("sent");
            }
            catch (Exception)
            {
                f.SetText("Exception occured when sending movie list.");
            }
        }

        // Add booking to booking list
        public void AddClientBooking()
        {
            ns = new NetworkStream(client);
            reader = new StreamReader(ns);
            writer = new StreamWriter(ns);
            writer.AutoFlush = true;
            try
            {
                // id need to be unique? how? ***
                string id = reader.ReadLine();
                string movie = reader.ReadLine();
                string user = reader.ReadLine();
                double price = Convert.ToDouble(reader.ReadLine());
                string date = reader.ReadLine();
                string time = reader.ReadLine();
                string[] seats = (reader.ReadLine()).Split('|');

                Booking newBook = new Booking();
                newBook.initBooking(id, movie, user, price, date, time, seats);

                // Hashset collection will prevent duplicates
                variables.bookingList.Add(newBook);

                f.SetText("New booking added to Booking List.");
            }
            catch (Exception)
            {
                f.SetText("Exception occured when adding client booking.");
            }

            //bool exist = variables.bookingList.Contains(newBook);
            //if (exist)
            //{
            //    writer.WriteLine("exist");
            //    return;
            //}
        }

        // To return list of booking that client has booked
        public void ViewClientBooking()
        {
            ns = new NetworkStream(client);
            reader = new StreamReader(ns);
            writer = new StreamWriter(ns);
            writer.AutoFlush = true;
            try
            {
                string user = reader.ReadLine();
                bool found = false;
                HashSet<Booking> newSet = new HashSet<Booking>();

                foreach (Booking details in variables.bookingList)
                {
                    if (details.User == user)
                    {
                        found = true;
                        newSet.Add(details);
                        //writer.WriteLine(details.Movie);
                        //writer.WriteLine(details.Price);
                        //writer.WriteLine(details.Date);
                        //writer.WriteLine(details.Timeslot);
                        //// send string[] as a single string joined by |
                        //writer.WriteLine(string.Join("|", details.Seats));
                    }
                }
                if (found)
                {
                    var xs = new XmlSerializer(typeof(HashSet<Booking>));
                    string xml;
                    using (var writer = new StringWriter())
                    {
                        xs.Serialize(writer, newSet);
                        xml = writer.ToString();
                        writer.WriteLine(xml);
                    }
                }
                writer.WriteLine("sent");
            }
            catch (Exception)
            {
                f.SetText("Exception occured when viewing client booking.");
            }
        }

        // To search movie list based on client input
        public void SearchMovie()
        {
            ns = new NetworkStream(client);
            reader = new StreamReader(ns);
            writer = new StreamWriter(ns);
            writer.AutoFlush = true;

            //byte[] fileNameByte;
            //byte[] fileData;
            try
            {
                string input = reader.ReadLine();
                bool found = false;
                HashSet<Movie> newSet = new HashSet<Movie>();

                foreach (Movie details in variables.movieList)
                {
                    // If the title matches user input or input matches title first few characters
                    if (details.Title.ToLower() == input.ToLower() || details.Title.StartsWith(input.ToLower()))
                    {
                        // If Movie is showing, send info. (?)
                        if (details.Status == true)
                        {
                            found = true;
                            newSet.Add(details);
                            //fileNameByte = Encoding.ASCII.GetBytes(details.ImageFileName);
                            //fileData = File.ReadAllBytes(details.ImageFileName);

                            //// sends the title, filename, file data bytes and file data
                            //writer.WriteLine(details.Title);
                            //writer.WriteLine(details.ImageFileName);
                            //writer.WriteLine(fileData.Length);
                            //client.Send(fileData);
                        }
                    }
                }
                if (found)
                {
                    var xs = new XmlSerializer(typeof(HashSet<Movie>));
                    string xml;
                    using (var writer = new StringWriter())
                    {
                        xs.Serialize(writer, newSet);
                        xml = writer.ToString();
                        writer.WriteLine(xml);
                    }
                }
                writer.WriteLine("sent");
            }
            catch (Exception)
            {
                f.SetText("Exception occured when searching movie.");
            }
        }
    }
}
