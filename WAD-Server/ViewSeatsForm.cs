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
    public partial class ViewSeatsForm : Form
    {
        public delegate void SetButtonCallback(Button b);
        private string timeslotGiven = null;
        private string dateGiven = null;
        private string movieGiven = null;

        public ViewSeatsForm(string movie, string time, string date)
        {
            InitializeComponent();

            movieGiven = movie;
            timeslotGiven = time;
            dateGiven = date;
            //updateSeats();
        }

        public void SetButton(Button b)
        {
            if (this.InvokeRequired)
            {
                SetButtonCallback d = new SetButtonCallback(SetButton);
                this.Invoke(d, b);
                return;
            }
            b.Enabled = false;
        }

        public void updateSeats(string ID)
        {
            try
            {
                Button b;

                foreach (Booking details in variables.bookingList)
                {
                    if ((details.Movie == movieGiven) && (details.Date == dateGiven) && (details.Timeslot == timeslotGiven))
                    {
                        string[] seats = details.Seats;
                        foreach (var seat in seats)
                        {
                            b = this.Controls.Find(seat, true).FirstOrDefault() as Button;
                            SetButton(b);
                        }
                    }
                }
            }
            catch { }
        }
    }
}
