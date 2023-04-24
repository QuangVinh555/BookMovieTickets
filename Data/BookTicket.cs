using System;
using System.Collections.Generic;

#nullable disable

namespace BookMovieTickets.Data
{
    public partial class BookTicket
    {
        public BookTicket()
        {
            BookTicketDetails = new HashSet<BookTicketDetail>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? MovieId { get; set; }
        public int? ShowTimeId { get; set; }
        public int? PaymentId { get; set; }
        public double? TicketPrice { get; set; }
        public int? TicketCount { get; set; }
        public int? RewardPoints { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ShowTime ShowTime { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BookTicketDetail> BookTicketDetails { get; set; }
    }
}
