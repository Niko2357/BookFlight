using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookFlight
{
    public class Flight
    {
        public int id { get; set; }
        public string flightNumber { get; set; }
        public int planeId { get; set; }
        public Plane plane { get; set; }
        public DateTime departure { get; set; }
        public DateTime arrival { get; set; }
        public string departurePlace { get; set; }
        public string arrivalPlace { get; set; }

        public Flight()
        {
            this.id = id;
            this.flightNumber = flightNumber;
            this.planeId = planeId;
            this.plane = plane;
            this.departure = departure;
            this.arrival = arrival;
            this.departurePlace = departurePlace;
            this.arrivalPlace = arrivalPlace;
        }
    }
}
