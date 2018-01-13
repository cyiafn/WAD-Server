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
        // Server's socket
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // Declare delegate
        public delegate void SetTextCallback(string msg);

        public Form1()
        {
            InitializeComponent();

            // Starts the server in the background
            runServer();

            // Populates combobox with movies
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
            SetText("Waiting for clients on port " + port + ".");
            // Runs the task in the background
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
            txtDisplay.AppendText(DateTime.Now.ToString("h: mm:ss tt") + ":" + msg + "\n");
            //txtDisplay.Text = msg;
        }
        #endregion

        // To prevent cross-threading and add title to combo box
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
            cbMovies.Sorted = true;
            cbMovies.Refresh();
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

                        int match = 0;
                        int count = 0;

                        foreach (Movie m in variables.movieList)
                        {
                            // If there is a matching movie
                            if (m.Title == temp[1])
                            {
                                match++;
                                try
                                {
                                    string[] bookedSeats = m.ShowTime[temp[4] + ";" + temp[5]];

                                    if (bookedSeats == null || bookedSeats.Length == 0)
                                    {
                                        SetText("All seats are being reserved!");
                                    }
                                    else if (bookedSeats.Length > 0)
                                    {
                                        List<string> list = new List<string>(bookedSeats);
                                        foreach (string s in seats)
                                        {
                                            if (list.Contains(s))
                                            {
                                                count++;
                                            }
                                        }
                                        // Check to see if no. of reserve seats matches no. avail seats
                                        if (count == seats.Length)
                                        {
                                            foreach (string s in seats)
                                            {
                                                if (list.Contains(s))
                                                {
                                                    list.Remove(s);
                                                }
                                            }
                                            // converts back to string[] and update ShowTime
                                            m.ShowTime[temp[4] + ";" + temp[5]] = list.ToArray();
                                            newbook.initBooking(temp[0], temp[1], temp[2], Convert.ToDouble(temp[3]), temp[4], temp[5], seats);
                                            lock (variables.bookingList) variables.bookingList.Add(newbook);
                                        }
                                        else
                                        {
                                            SetText("An booking from txt file seats are already reserved!");
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // Movie matches txt movie, but date/time given is outdated because ShowTime[key] does not exist anymore
                                    newbook.initBooking(temp[0], temp[1], temp[2], Convert.ToDouble(temp[3]), temp[4], temp[5], seats);
                                    lock (variables.bookingList) variables.bookingList.Add(newbook);
                                }
                            }
                        }
                        if (match == 0)
                        {
                            newbook.initBooking(temp[0], temp[1], temp[2], Convert.ToDouble(temp[3]), temp[4], temp[5], seats);
                            lock (variables.bookingList) variables.bookingList.Add(newbook);
                        }
                    }
                    SetText("Booking details has been loaded.");
                    MessageBox.Show("Booking details has been loaded.");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: could not read file from disk.");
                }
            }
        }
        #endregion

        // To use SaveFileDialog to save booking
        #region saveBooking() function
        public void saveBooking()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            // default file name, file extension, filter file extension
            dlg.Title = "Save Text File";
            dlg.FileName = "newBookingList.txt";
            dlg.Filter = "TXT files|*.txt";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Save document
                StreamWriter writer = new StreamWriter(dlg.OpenFile());

                // Saves booking list in "1;Spiderman;Seanmarcus;2.00;16/01/2018;2PM;A1" format
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

        // To change movie status from cbox selected item
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
                    lock (variables.movieList) details.Status = !details.Status;
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

        // To list all booking of specific movie from cbox selected item
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
        }
        #endregion

        // To save txtdisplay.text to file
        #region saveServerLog() function
        public void saveServerLog()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            // default file name, file extension, filter file extension
            dlg.Title = "Save Server Log";
            dlg.FileName = "ServerLog.txt";
            dlg.Filter = "TXT files|*.txt";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Save document
                File.WriteAllLines(dlg.FileName, txtDisplay.Lines);

                SetText("Server log has been saved.");
                MessageBox.Show("Server log has been saved!");
            }
        }
        #endregion

        // Button click functions
        #region button_click functions
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
            viewMovieBooking();
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            saveServerLog();
        }
        #endregion
    }

    public class variables
    {
        public static HashSet<Booking> bookingList = new HashSet<Booking>();
        public static HashSet<Movie> movieList = new HashSet<Movie>();
        public static HashSet<user> userList = new HashSet<user>();
        public static string selectedMovie = null;
    }
}
