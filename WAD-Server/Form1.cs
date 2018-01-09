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
        Booking booking = new Booking();
        Movie movie = new Movie();

        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // Declare delegate
        public delegate void SetTextCallback(string msg);

        public Form1()
        {
            InitializeComponent();

            // Starts the server in the background
            runServer();

            txtDisplay.ReadOnly = true;
            txtDisplay.BackColor = System.Drawing.SystemColors.Window;
        }

        // Code to start the server
        #region runServer()
        async void runServer()
        {
            int port = 9000;
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port);
            server.Bind(endpoint);
            server.Listen(10);
            //SetText("Waiting for clients on port " + port);
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
                        MessageBox.Show("Connection failed on port " + port);
                        //SetText("Connection falied on port " + port);
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
            txtDisplay.Text = msg;
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
                        booking.initBooking(temp[0], temp[1], Convert.ToDouble(temp[2]), temp[3]);
                        variables.bookingList.Add(booking);
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
            dlg.FileName = "newBookingList.txt";
            dlg.Filter = "TXT files|*.txt";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Save document
                //File.WriteAllText(dlg.FileName, info);
                StreamWriter writer = new StreamWriter(dlg.OpenFile());

                foreach (Booking book in variables.bookingList)
                {
                    writer.WriteLine("{0};{1};{2};{3}", book.TransactionId, book.Seat, book.Price, book.DateTime);
                }
                writer.Dispose();
                writer.Close();
                MessageBox.Show("Booking list saved!");
            }
        }
        #endregion

        // To list all booking of movie
        #region listBooking() function
        public void listBooking()
        {
            string s = null;

            foreach (Booking book in variables.bookingList)
            {
                // do logic
                s += string.Format("{0}\t{1}\t{2}\t{3}", book.TransactionId, book.Seat, book.Price, book.DateTime) + Environment.NewLine;
            }
            // to temporary display
            SetText(s);
            //MessageBox.Show(s);
        }
        #endregion

        // To list all booking of specific movie
        // code here

        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            AddMovieForm form2 = new AddMovieForm();
            form2.Show();
        }

        private void btnUploadBooking_Click(object sender, EventArgs e)
        {
            loadBooking();
        }

        private void btnSaveBooking_Click(object sender, EventArgs e)
        {
            saveBooking();
        }

        private void btnListBooking_Click(object sender, EventArgs e)
        {
            listBooking();
        }
    }

    public class variables
    {
        public static List<Booking> bookingList = new List<Booking>();
        public static List<Movie> movieList = new List<Movie>();
        public static List<user> userList = new List<user>();
    }
}
