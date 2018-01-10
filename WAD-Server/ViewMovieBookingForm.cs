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

        public ViewMovieBookingForm()
        {
            InitializeComponent();

            // Obtain selected movie from other form
            MovieSelected = variables.selectedMovie;
        }
    }
}
