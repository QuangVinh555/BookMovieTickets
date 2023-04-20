using System;
using System.Collections.Generic;

#nullable disable

namespace BookMovieTickets.Data
{
    public partial class HourTime
    {
        public int Id { get; set; }
        public int? ShowTimeId { get; set; }
        public string Time { get; set; }
        public string EndTime { get; set; }

        public virtual ShowTime ShowTime { get; set; }
    }
}
