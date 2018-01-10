using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD_Server
{
    public class Booking : IEquatable<Booking>
    {
        public String TransactionId { get; set; }
        public String Movie { get; set; }
        public String User { get; set; }
        public double Price { get; set; }
        public String Date { get; set; }
        public String Timeslot { get; set; }
        public String[] Seats { get; set; }

        public void initBooking(String transactionId, String movie, String user, double price, String date, String timeslot, String[] seats)
        {
            this.TransactionId = transactionId;
            this.Movie = movie;
            this.User = user;
            //this.Seat = seat;
            this.Price = price;
            this.Date = date;
            this.Timeslot = timeslot;
            this.Seats = seats;
        }

        public bool Equals(Booking other)
        {
            return TransactionId.Equals(other.TransactionId);
        }

        public override int GetHashCode()
        {
            return TransactionId.GetHashCode();
        }
    }
}
