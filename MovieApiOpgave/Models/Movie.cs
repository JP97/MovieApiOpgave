using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApiOpgave.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int NumSeats { get; set; }
    }
}
