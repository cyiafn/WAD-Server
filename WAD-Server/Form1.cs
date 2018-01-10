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
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // Declare delegate
        public delegate void SetTextCallback(string msg);

        public Form1()
        {
            InitializeComponent();

            // Starts the server in the background
            runServer();
            populateCBox();

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
                        //MessageBox.Show("Connection failed on port " + port);
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
            txtDisplay.AppendText(msg + "\n");
            //txtDisplay.Text = msg;
        }
        #endregion

        #region addTitle() function
        public void addTitle(string title)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(addTitle);
                this.Invoke(d, title);
                return;
            }
            cbMovies.Items.Add(title);
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
                    string[] data = File.ReadAllLines(dlg.FileName);
                    foreach (var line in data)
                    {
                        Booking newbook = new Booking();
                        string[] temp = Convert.ToString(line).Split(';');
                        string[] seats = temp[6].Split('|');
                        newbook.initBooking(temp[0], temp[1], temp[2], Convert.ToDouble(temp[3]), temp[4], temp[5], seats);
                        variables.bookingList.Add(newbook);
                    }
                    SetText("Booking details has been loaded.");
                    MessageBox.Show("Booking details has been loaded.");
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
                    writer.WriteLine("{0};{1};{2};{3};{4};{5}", 
                        book.TransactionId, book.Movie, book.Price, book.Date, book.Timeslot, string.Join("|", book.Seats));
                }
                writer.Dispose();
                writer.Close();
                SetText("Booking details has been saved.");
                MessageBox.Show("Booking list saved!");
            }
        }
        #endregion

        // To list all booking of movie (not needed anymore)
        #region listBooking() function
        public void listBooking()
        {
            string s = null;

            //foreach (Booking book in variables.bookingList)
            //{
            //    // do logic
            //    s += "ID\tSeat\tPrice\tDateTime" + Environment.NewLine;
            //    s += string.Format("{0}\t{1}\t{2}\t{3}", book.TransactionId, book.Seat, book.Price, book.DateTime) + Environment.NewLine;
            //}
            // to temporary display
            SetText(s);
            //MessageBox.Show(s);
        }
        #endregion

        // To populate combobox (updates every 30 seconds)
        #region populateCBox() function
        async void populateCBox()
        {
            await Task.Run(async () =>
            {
                int count = 0;
                while (true)
                {
                    try
                    {
                        if (count == 0)
                        {
                            foreach (Movie details in variables.movieList)
                            {
                                // Prevent cross thread from happening here
                                addTitle(details.Title);
                                count++;
                            }
                        }
                        else if (count != variables.movieList.Count)
                        {
                            foreach (Movie details in variables.movieList)
                            {
                                // check status of movie and remove accordingly
                                if (!cbMovies.Items.Contains(details.Title))
                                {
                                    // Prevent cross thread from happening here
                                    addTitle(details.Title);
                                    count++;
                                }
                            }
                            cbMovies.Refresh();
                        }
                        await PutTaskDelay();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            });
        }
        #endregion

        // Code to delay tasks without affecting UI
        #region PutTaskDelay()
        async Task PutTaskDelay()
        {
            await Task.Delay(30000);
        }
        #endregion

        // To remove movie based on cbox (or change the status to not showing)
        #region changeStatus() function
        public void changeStatus()
        {
            string movie = cbMovies.Text;

            if (movie == "")
            {
                MessageBox.Show("Please select movie from drop down list to change status.");
                return;
            }
            // to be modified to not remove but update status
            foreach (Movie details in variables.movieList)
            {
                if (details.Title == movie)
                {
                    details.Status = !details.Status;
                    if (details.Status)
                        MessageBox.Show(movie + " status has been changed to now showing.");
                    else
                       MessageBox.Show(movie + " status has been changed to not showing.");
                    SetText(movie + " status has been updated.");
                    return;
                }
            }
        }
        #endregion

        // To list all booking of specific movie
        #region viewMovieBooking() function
        public void viewMovieBooking()
        {
            string movie = cbMovies.Text;

            if (movie == "")
            {
                MessageBox.Show("Please select view movie from drop down list Movie List.");
                return;
            }

            variables.selectedMovie = movie;
            ViewMovieBookingForm form2 = new ViewMovieBookingForm();
            form2.Show();
            // open new form or smth
            // set a global variable static string for movie
            // form will show movie timeslots combobox or something
            // show date or smth
            // display on gridview
        }
        #endregion

        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            AddMovieForm form2 = new AddMovieForm(this);
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
            ViewBookingForm form2 = new ViewBookingForm();
            form2.Show();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            changeStatus();
        }

        private void btnViewSpecificMovie_Click(object sender, EventArgs e)
        {
            // do logic
            viewMovieBooking();
        }
    }

    public class variables
    {
        //public static List<Booking> bookingList = new List<Booking>();
        //public static List<Movie> movieList = new List<Movie>();
        //public static List<user> userList = new List<user>();

        public static HashSet<Booking> bookingList = new HashSet<Booking>();
        public static HashSet<Movie> movieList = new HashSet<Movie>();
        public static HashSet<user> userList = new HashSet<user>();
        public static string selectedMovie = null;
    }
}
