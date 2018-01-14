// Lee Wei Xiong, Seanmarcus, S10168234B
// Features: UpdateGVBookingList() and UpdateCBDate() function
using System;
using System.Windows.Forms;

namespace WAD_Server
{
    public partial class ViewMovieBookingForm : Form
    {
        private string MovieSelected = null;
        private string TimeSelected = null;

        public delegate void SetTextCallback(string msg);

        // On initialization, populates the combo box with dates of selected movie
        public ViewMovieBookingForm()
        {
            InitializeComponent();

            // Obtain selected movie from other form
            MovieSelected = variables.selectedMovie;

            // Update combobox
            UpdateCBDate();
        }

        // Sets the text for label
        #region SetText() function
        public void SetText(string msg)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, msg);
                return;
            }
            lblTime.Text = "Time selected: " + msg;
        }
        #endregion

        // Adds dates to combo box
        #region addDate() function
        public void addDate(string date)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(addDate);
                this.Invoke(d, date);
                return;
            }
            cbDate.Items.Add(date);
            cbDate.Sorted = true;
        }
        #endregion

        // Populates combo box with dates based on movie selected
        #region UpdateCBDate() function
        public void UpdateCBDate()
        {
            string d = null;
            foreach (Movie m in variables.movieList)
            {
                foreach (var date in m.ShowTime)
                {
                    // Gets the key which is date;time format
                    string[] temp = date.Key.Split(';');
                    d = temp[0];
                    if (!cbDate.Items.Contains(d))
                    {
                        // Prevent cross thread from happening here
                        addDate(d);
                    }
                }
            }
        }
        #endregion

        // Populates data grid view with booking list on specific movie, time and date
        #region UpdateGVBookingList() function
        public void UpdateGVBookingList()
        {
            // Clear previous data and add rows
            dgvBookingList.Rows.Clear();
            dgvBookingList.Columns.Clear();
            dgvBookingList.Columns.Add("ID", "ID");
            dgvBookingList.Columns.Add("Movie", "Movie");
            dgvBookingList.Columns.Add("User", "User");
            dgvBookingList.Columns.Add("Price", "Price");
            dgvBookingList.Columns.Add("Seat(s)", "Seat(s)");

            string date = cbDate.Text;

            if (TimeSelected != null && date != "")
            {
                foreach (Booking details in variables.bookingList)
                {
                    if ((details.Timeslot == TimeSelected) && (details.Date == date) && (details.Movie == MovieSelected))
                    {
                        string seats = string.Join(",", details.Seats);
                        dgvBookingList.Rows.Add(new object[] { details.TransactionId, details.Movie, details.User, details.Price, seats });
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select date and time to view booking list.");
            }
        }
        #endregion

        // Button click events for all buttons in the form
        // Opens a new form on btnViewSeats_Click
        #region Button_Click events
        private void btnViewSeats_Click(object sender, EventArgs e)
        {
            string date = cbDate.Text;

            if (TimeSelected != null && date != "")
            {
                ViewSeatsForm form2 = new ViewSeatsForm(MovieSelected, TimeSelected, date);
                form2.Show();
            }
            else
            {
                MessageBox.Show("Please select date and time to view seats");
            }
        }

        private void btn12PM_Click(object sender, EventArgs e)
        {
            TimeSelected = "12PM";
            SetText(TimeSelected);
        }

        private void btn2PM_Click(object sender, EventArgs e)
        {
            TimeSelected = "2PM";
            SetText(TimeSelected);
        }

        private void btn4PM_Click(object sender, EventArgs e)
        {
            TimeSelected = "4PM";
            SetText(TimeSelected);
        }

        private void btn6PM_Click(object sender, EventArgs e)
        {
            TimeSelected = "6PM";
            SetText(TimeSelected);
        }

        private void btn8PM_Click(object sender, EventArgs e)
        {
            TimeSelected = "8PM";
            SetText(TimeSelected);
        }

        private void btn10PM_Click(object sender, EventArgs e)
        {
            TimeSelected = "10PM";
            SetText(TimeSelected);
        }

        private void btn12AM_Click(object sender, EventArgs e)
        {
            TimeSelected = "12AM";
            SetText(TimeSelected);
        }

        // Calls UpdateGVBookingList() on button click
        private void btnViewBookingList_Click(object sender, EventArgs e)
        {
            UpdateGVBookingList();
        }
        #endregion
    }
}
