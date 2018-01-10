using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD_Server
{
    public class Movie : IEquatable<Movie>
    {
        public String Title { get; set; }
        public String MovieType { get; set; }
        public double Price { get; set; }
        public String ImageFileName { get; set; }
        public bool Status { get; set; }

        public void initMovie(String title, String movieType, double price, String imageFileName)
        {
            this.Title = title;
            this.MovieType = movieType;
            this.Price = price;
            this.ImageFileName = imageFileName;
            this.Status = true;
        }

        public bool Equals(Movie other)
        {
            return Title.Equals(other.Title);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }
}
