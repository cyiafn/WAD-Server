using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAD_Server
{
    public partial class ViewMovieBookingForm : Form
    {
        private string MovieSelected = null;
        private string TimeSelected = null;
        private string DateSelected = null;

        public delegate void SetTextCallback(string msg);

        public ViewMovieBookingForm()
        {
            InitializeComponent();

            // Obtain selected movie from other form
            MovieSelected = variables.selectedMovie;

            // Update combobox
            UpdateCBDate();
        }

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

        public void addDate(string date)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(addDate);
                this.Invoke(d, date);
                return;
            }
            cbDate.Items.Add(date);
        }

        public void UpdateCBDate()
        {
            foreach (Booking details in variables.bookingList)
            {
                if (details.Movie == MovieSelected)
                {
                    if (!cbDate.Items.Contains(details.Date))
                    {
                        // Prevent cross thread from happening here
                        addDate(details.Date);
                    }
                }
            }
        }

        public void UpdateGVBookingList()
        {
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
                    if ((details.Timeslot == TimeSelected) && (details.Date == date))
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

        private void btnViewBookingList_Click(object sender, EventArgs e)
        {
            UpdateGVBookingList();
        }
    }
}
