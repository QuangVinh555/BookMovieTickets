using System;
using System.Collections.Generic;

#nullable disable

namespace BookMovieTickets.Data
{
    public partial class Combo
    {
        public Combo()
        {
            BookTicketDetails = new HashSet<BookTicketDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Count { get; set; }
        public int? Price { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? Deleted { get; set; }

        public virtual ICollection<BookTicketDetail> BookTicketDetails { get; set; }
    }
}
