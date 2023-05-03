using System;
using System.Collections.Generic;

#nullable disable

namespace BookMovieTickets.Data
{
    public partial class ShowTime
    {
        public ShowTime()
        {
            BookTicketDetails = new HashSet<BookTicketDetail>();
            HourTimes = new HashSet<HourTime>();
        }

        public int Id { get; set; }
        public int? CinemaRoomId { get; set; }
        public int? MovieId { get; set; }
        public DateTime? ShowDate { get; set; }
        public double? TicketPrice { get; set; }
        public int? NumTicket { get; set; }
        public bool? Deleted { get; set; }
        public string Role { get; set; }
        public bool? State { get; set; }

        public virtual CinemaRoom CinemaRoom { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ICollection<BookTicketDetail> BookTicketDetails { get; set; }
        public virtual ICollection<HourTime> HourTimes { get; set; }
    }
}
