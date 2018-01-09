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
                        Booking newBooking = new Booking(temp[0], temp[1], Convert.ToDouble(temp[2]), Convert.ToDateTime(temp[3]));
                        bookingList.Add(newBooking);
                        count++;
                    }
                    //txtDisplay.Text = "Number of records loaded: " + count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: could not read file from disk.");
                }
            }
        }
        #endregion

        // To list booking of specifc movie
        #region listBooking() function
        public void listBooking()
        {
            foreach (Booking booked in bookingList)
            {

            }
        }
        #endregion

        // To add movie with textbox and image from picturebox
        #region addMovie() function
        public void addMovie()
        {
            //string title = txtTitle.Text.Trim();
            //string movieType = txtType.Text.Trim();
            //double price = Convert.ToDouble(txtPrice.Text.Trim());
            // Open file dialog to upload to picture box
            // means uploadImage is used
            //string imageFileName = txtImage.Text.Trim();

            // Save/Write image to server based on the picture box
            //pictureBox.Image.Save(@"Path", System.Drawing.Imaging.ImageFormat.Jpeg);

            //MemoryStream ms = new MemoryStream();
            //pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //byte[] ar = new byte[ms.Length];
            //ms.Write(ar, 0, ar.Length);

            //Movie newMovie = new Movie(title, movieType, price, imageFileName);
            //movieList.Add(newMovie);

        }
        #endregion

        // To open file dialog to upload image to picturebox
        #region uploadImage() function
        public void uploadImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // Image filters
            ofd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Display image in picture box
                    //PictureBox1.Image = new Bitmap(ofd.FileName);
                    // Image file path
                    //textBox1.Text = ofd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: could not read image from disk.");
                }
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
