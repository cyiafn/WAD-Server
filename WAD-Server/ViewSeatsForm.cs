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

            // Disable/Enable seats
            updateSeats();
        }

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
                            b = this.Controls.Find(seat, true).FirstOrDefault() as Button;
                            SetButton(b);
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured, please try again.");
            }
        }
        #endregion
    }
}
