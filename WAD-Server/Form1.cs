using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace WAD_Server
{
    public partial class Form1 : Form
    {
        // Booking, Movie list
        List<Booking> bookingList = new List<Booking>();
        List<Movie> movieList = new List<Movie>();
        //List<user> userList = new List<user>();

        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // Declare delegate
        public delegate void SetTextCallback(string msg);

        public Form1()
        {
            InitializeComponent();

            // Starts the server in the background
            runServer();
        }

        // Code to start the server
        #region runServer()
        async void runServer()
        {
            int port = 9000;
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port);
            server.Bind(endpoint);
            server.Listen(10);
            SetText("Waiting for clients on port " + port);
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        Socket client = server.Accept();
                        ConnectionHandler handler = new ConnectionHandler(client, this);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(handler.HandleConnection));
                    }
                    catch (Exception)
                    {
                        SetText("Connection falied on port " + port);
                    }
                }
            });
        }
        #endregion

        // To prevent cross-threading and set text
        #region setText() function
        public void SetText(string msg)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, msg);
                return;
            }
            //txtResult.Text = msg;
        }
        #endregion

        // To load booking from text file
        #region loadBooking() function
        public void loadBooking()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Text File";
            dlg.FileName = "BookingList.txt";
            dlg.Filter = "TXT files|*.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int count = 0;
                    string[] data = File.ReadAllLines(dlg.FileName);
                    foreach (var line in data)
                    {
                        string[] temp = Convert.ToString(line).Split(';');
                        Booking booking = new Booking();
                        booking.initBooking(temp[0], temp[1], Convert.ToDouble(temp[2]), Convert.ToDateTime(temp[3]));
                        booking.bookingList.Add(booking);
                        count++;
                    }
                    MessageBox.Show(count + " booking details has been loaded.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: could not read file from disk.");
                }
            }
        }
        #endregion

        // Use of SaveFileDialog to be user friendly
        #region saveBooking() function
        public void saveBooking()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            // default file name, file extension, filter file extension
            dlg.Title = "Open Text File";
            dlg.FileName = "BookingList.txt";
            dlg.Filter = "TXT files|*.txt";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Save document
                //File.WriteAllText(dlg.FileName, info);
                StreamWriter writer = new StreamWriter(dlg.OpenFile());
                Booking details = new Booking();
                List<Booking> calledList = details.GetList();

                foreach (Booking book in calledList)
                {
                    writer.WriteLine("{0};{1};{2};{3};{4}", book.TransactionId, book.Seat, book.Price, book.DateTime);
                }
                    //foreach (var kvp in dict)
                    //{
                    //    writer.WriteLine(kvp.Key + ";" + kvp.Value);
                    //}
                writer.Dispose();
                writer.Close();
                MessageBox.Show("Booking list saved!");
            }
        }
        #endregion

        // To list booking of specifc movie
        #region listBooking() function
        public void listBooking()
        {
            Booking details = new Booking();
            List<Booking> calledList = details.GetList();

            foreach (Booking book in calledList)
            {
                // do logic
            }
        }
        #endregion

        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            AddMovieForm form2 = new AddMovieForm();
            form2.Show();
        }
    }
}
