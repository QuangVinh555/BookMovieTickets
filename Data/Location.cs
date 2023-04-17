using System;
using System.Collections.Generic;

#nullable disable

namespace BookMovieTickets.Data
{
    public partial class Location
    {
        public Location()
        {
            CinemaNames = new HashSet<CinemaName>();
            ShowTimes = new HashSet<ShowTime>();
        }

        public int Id { get; set; }
        public string Province { get; set; }

        public virtual ICollection<CinemaName> CinemaNames { get; set; }
        public virtual ICollection<ShowTime> ShowTimes { get; set; }
    }
}
