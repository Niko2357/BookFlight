using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFlight
{
    public class Reservation
    {
        public int id { get; set; }
        public int flightId { get; set; }
        public Flight flight { get; set; }
        public int passengerId { get; set; }
        public Passenger passenger { get; set; }
        public int seatId { get; set; }
        public Seat seat { get; set; }
        public int userId {  get; set; }
        public UserAccount userAccount { get; set; }
        public double price { get; set; }
        public bool isPaid { get; set; }

        public Reservation(int flightId, int passengerId, int seatId, int userId, double price, bool isPaid)
        {
            this.id = id;
            this.flightId = flightId;
            this.flight = flight;
            this.passengerId = passengerId;
            this.passenger = passenger;
            this.seatId = seatId;
            this.userId = userId;
            this.userAccount = userAccount;
            this.price = price;
            this.isPaid = isPaid;
        }
    }
}
