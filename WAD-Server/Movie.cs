using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD_Server
{
    class Movie
    {
        public String Title { get; set; }
        public String MovieType { get; set; }
        public double Price { get; set; }
        public String ImageFileName { get; set; }
        public List<Movie> MovieList { get; set; }

        public void initMovie(String title, String movieType, double price, String imageFileName)
        {
            this.Title = title;
            this.MovieType = movieType;
            this.Price = price;
            this.ImageFileName = imageFileName;
        }

        public List<Movie> GetList() { return MovieList; }
    }
}
