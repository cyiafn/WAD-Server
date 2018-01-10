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
    public partial class ViewBookingForm : Form
    {
        public ViewBookingForm()
        {
            InitializeComponent();

            populateDataGrid();
        }

        public void populateDataGrid()
        {
            dgvBooking.Columns.Add("ID", "ID");
            dgvBooking.Columns.Add("Movie", "Movie");
            dgvBooking.Columns.Add("User", "User");
            dgvBooking.Columns.Add("Price", "Price");
            dgvBooking.Columns.Add("Date", "Date");
            dgvBooking.Columns.Add("Timeslot", "Timeslot");
            dgvBooking.Columns.Add("Seat(s)", "Seat(s)");
            //dgvBooking.DataSource = variables.bookingList.ToList();
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
    }
}
