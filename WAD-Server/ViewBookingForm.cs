// Lee Wei Xiong, Seanmarcus, S10168234B
// Features: populateDataGrid() and btnSearch_Click function
using System;
using System.Windows.Forms;

namespace WAD_Server
{
    public partial class ViewBookingForm : Form
    {
        // Populates data grid on initalization
        public ViewBookingForm()
        {
            InitializeComponent();

            // Populates the data grid view with all booking initally
            populateDataGrid();
            cbFilter.SelectedIndex = 0;
        }

        // Populates data grid with all booking details
        public void populateDataGrid()
        {
            // Adds columns to data grid view
            dgvBooking.Columns.Add("ID", "ID");
            dgvBooking.Columns.Add("Movie", "Movie");
            dgvBooking.Columns.Add("User", "User");
            dgvBooking.Columns.Add("Price", "Price");
            dgvBooking.Columns.Add("Date", "Date");
            dgvBooking.Columns.Add("Timeslot", "Timeslot");
            dgvBooking.Columns.Add("Seat(s)", "Seat(s)");

            foreach (Booking details in variables.bookingList)
            {
                string seats = string.Join(",", details.Seats);
                dgvBooking.Rows.Add(new object[] { details.TransactionId, details.Movie, details.User,
                    details.Price, details.Date, details.Timeslot, seats });
            }

            // set autosize mode
            dgvBooking.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBooking.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBooking.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

           // datagrid has calculated it's widths so we can store them
            for (int i = 0; i <= dgvBooking.Columns.Count - 1; i++)
            {
                // store autosized widths
                int colw = dgvBooking.Columns[i].Width;
                // remove autosizing
                dgvBooking.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                // set width to calculated by autosize
                dgvBooking.Columns[i].Width = colw;
            }
        }

        // Searches the hash set on click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvBooking.Rows.Clear();

            string input = txtSearch.Text.Trim();
            string filter = cbFilter.Text;

            // Search based on filter, default is Name
            if (filter == "Name")
            {
                foreach (Booking details in variables.bookingList)
                {
                    if (details.User.ToLower() == input.ToLower() || details.User.StartsWith(input))
                    {
                        string seats = string.Join(",", details.Seats);
                        dgvBooking.Rows.Add(new object[] { details.TransactionId, details.Movie, details.User,
                    details.Price, details.Date, details.Timeslot, seats });
                    }
                }
            }
            else if (filter == "Movie")
            {
                foreach (Booking details in variables.bookingList)
                {
                    if (details.Movie.ToLower() == input.ToLower() || details.Movie.StartsWith(input))
                    {
                        string seats = string.Join(",", details.Seats);
                        dgvBooking.Rows.Add(new object[] { details.TransactionId, details.Movie, details.User,
                    details.Price, details.Date, details.Timeslot, seats });
                    }
                }
            }
            else
            {
                foreach (Booking details in variables.bookingList)
                {
                    if (details.TransactionId.ToLower() == input.ToLower())
                    {
                        string seats = string.Join(",", details.Seats);
                        dgvBooking.Rows.Add(new object[] { details.TransactionId, details.Movie, details.User,
                    details.Price, details.Date, details.Timeslot, seats });
                    }
                }
            }
            
        }
    }
}
