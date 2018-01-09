using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD_Server
{
    public class Booking
    {
        public String TransactionId { get; set; }
        public String Seat { get; set; }
        public double Price { get; set; }
        public String DateTime { get; set; }
        //public List<Booking> bookingList = new List<Booking>();

        public void initBooking(String transactionId, string seat, double price, String dateTime)
        {
            this.TransactionId = transactionId;
            this.Seat = seat;
            this.Price = price;
            this.DateTime = dateTime;
        }

        //public void addBooking(Booking book)
        //{
        //    bookingList.Add(book);
        //}

        //public List<Booking> GetList() { return bookingList; }
    }
}
