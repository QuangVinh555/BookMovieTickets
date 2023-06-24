using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Views
{
    public class ResultRaitingVM
    {
        public int? MovieId { get; set; }
        public int? UserId { get; set; }
        public int? CountStars { get; set; }
    }
}
