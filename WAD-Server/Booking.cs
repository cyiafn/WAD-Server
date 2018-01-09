using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD_Server
{
    class Booking
    {
        public String TransactionId { get; set; }
        public String Seat { get; set; }
        public double Price { get; set; }
        public DateTime DateTime { get; set; }
        public List<Booking> bookingList { get; set; }

        public void initBooking(String transactionId, string seat, double price, DateTime dateTime)
        {
            this.TransactionId = transactionId;
            this.Seat = seat;
            this.Price = price;
            this.DateTime = dateTime;
        }

        public List<Booking> GetList() { return bookingList; }
    }
}
