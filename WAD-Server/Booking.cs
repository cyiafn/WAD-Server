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
        public string[] Seats { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateTime { get; set; }

        public Booking(String transactionId, string[] seats, double totalPrice, DateTime dateTime)
        {
            this.TransactionId = transactionId;
            this.Seats = seats;
            this.TotalPrice = totalPrice;
            this.DateTime = dateTime;
        }
    }
}
