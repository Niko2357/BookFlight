using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFlight
{
    public class Seat
    {
        public int id { get; set; }
        public int flightId { get; set; }
        public Flight flight { get; set; }
        public string seatNumber { get; set; }
        public bool isAvailable { get; set; }

        public Seat(int flightId, string seatNumber, bool isAvailable)
        {
            this.id = id;
            this.flightId = flightId;
            this.flight = flight;
            this.seatNumber = seatNumber;
            this.isAvailable = isAvailable;
        }
    }
}
