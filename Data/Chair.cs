using System;
using System.Collections.Generic;

#nullable disable

namespace BookMovieTickets.Data
{
    public partial class Chair
    {
        public Chair()
        {
            BookTickets = new HashSet<BookTicket>();
        }

        public int Id { get; set; }
        public int? CinemaRoomId { get; set; }
        public int? ChairTypeId { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }

        public virtual ChairType ChairType { get; set; }
        public virtual CinemaRoom CinemaRoom { get; set; }
        public virtual ICollection<BookTicket> BookTickets { get; set; }
    }
}
