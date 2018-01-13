using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAD_Server
{
    public partial class AddMovieForm : Form
    {
        private Form1 f;

        public AddMovieForm(Form1 f)
        {
            InitializeComponent();
            this.f = f;
        }

        // To add movie with textbox and image from picturebox
        #region addMovie() function
        public void addMovie()
        {
            string title = txtTitle.Text.Trim();
            string movieType = cbType.Text;
            double price = 0;
            double.TryParse(txtPrice.Text.Trim(), out price);
            string imageFileName = txtImage.Text.Trim();
            string videoUrl = VideoId(txtUrl.Text.Trim());
            byte[] fileNameByte;
            byte[] fileData;

            // Check if picture box has image, else check other fields
            if (pbPreview.Image == null)
            {
                MessageBox.Show("Please upload image!");
                return;
            }
            else if (title == "" || movieType == "" || price <= 0 || imageFileName == "" || videoUrl == string.Empty)
            {
                MessageBox.Show("Please fill in all the fields!");
                return;
            }
            try
            {
                // Code to save image as JPG
                int width = Convert.ToInt32(pbPreview.Width);
                int height = Convert.ToInt32(pbPreview.Height);
                Bitmap bmp = new Bitmap(width, height);
                pbPreview.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                bmp.Save(imageFileName, ImageFormat.Jpeg);
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                MessageBox.Show("Please ensure that image uploaded is not in current folder.\nAlternatively, rename the image file.");
                return;
            }

            fileNameByte = Encoding.ASCII.GetBytes(imageFileName);
            fileData = File.ReadAllBytes(imageFileName);

            // Add new movie to MovieList list
            Movie newMovie = new Movie();
            newMovie.initMovie(title, movieType, price, imageFileName, fileNameByte, fileData, videoUrl);
            bool exist = false;
            lock (variables.movieList)
            {
                exist = variables.movieList.Contains(newMovie);
            }
            //bool exist = variables.movieList.Contains(newMovie);
            if (exist)
            {
                MessageBox.Show("Movie is already added");
                return;
            }

            lock (variables.movieList) variables.movieList.Add(newMovie);
            f.SetText(title + " has been added to Movie List.");
            MessageBox.Show(title + " has been added.");
            this.Close();
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
                    pbPreview.Image = new Bitmap(ofd.FileName);
                    // Image file path
                    txtImage.Text = ofd.SafeFileName;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: could not read image from disk.");
                }
            }
        }
        #endregion

        // Gets video ID from URL link, check if youtube link is valid with regex
        #region VideoId() function
        public string VideoId(string _ytUrl)
        {
            // Checks if Youtube URL given is valid using Regex
            // Allows clients to open web browser control with youtube trailers
            var ytMatch = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)").Match(_ytUrl);
            return ytMatch.Success ? ytMatch.Groups[1].Value : string.Empty;
        }
        #endregion

        private void btnUpload_Click(object sender, EventArgs e)
        {
            uploadImage();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            addMovie();
        }
    }
}
