using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFlight
{
    public class Plane
    {
        public int id { get; set; }
        public string name { get; set; }
        public int capacity { get; set; }
        public string producer { get; set; }

        public Plane(int id, string name, int capacity, string producer)
        {
            this.id = id;
            this.name = name;
            this.capacity = capacity;
            this.producer = producer;
        }
    }
}
