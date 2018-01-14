// Lee Wei Xiong, Seanmarcus, S10168234B
// Features: updateSeats() function to enable/disable buttons to emulate booked seats
using System;
using System.Linq;
using System.Windows.Forms;

namespace WAD_Server
{
    public partial class ViewSeatsForm : Form
    {
        public delegate void SetButtonCallback(Button b);
        private string timeslotGiven = null;
        private string dateGiven = null;
        private string movieGiven = null;

        // Requires movie, time and date to initalize form
        public ViewSeatsForm(string movie, string time, string date)
        {
            InitializeComponent();

            movieGiven = movie;
            timeslotGiven = time;
            dateGiven = date;

            // Disable/Enable seats
            updateSeats();
        }

        // To set the button to enabled and prevent cross-thread issues
        #region SetButton() function
        public void SetButton(Button b)
        {
            if (this.InvokeRequired)
            {
                SetButtonCallback d = new SetButtonCallback(SetButton);
                this.Invoke(d, b);
                return;
            }
            b.Enabled = true;
        }
        #endregion

        // Updates the seating chart to enable buttons if not reserved
        #region updateSeats() function
        public void updateSeats()
        {
            try
            {
                Button b;

                foreach (Movie m in variables.movieList)
                {
                    if (m.Title == movieGiven)
                    {
                        string[] seats = m.ShowTime[dateGiven + ";" +timeslotGiven];

                        foreach(string seat in seats)
                        {
                            // Find control of the button by given string in string array
                            b = this.Controls.Find(seat, true).FirstOrDefault() as Button;
                            SetButton(b);
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured, please try again.");
            }
        }
        #endregion
    }
}
