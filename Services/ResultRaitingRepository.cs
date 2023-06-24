using BookMovieTickets.Data;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class ResultRaitingRepository : IResultRaitingRepository
    {
        private readonly BookMovieTicketsContext _context;

        public ResultRaitingRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }
        public List<MessageVM> GetAllRaitings()
        {
            var _listRaitings = _context.Comments.Select(x => new MessageVM
            {
                Message = "Lấy dữ liệu thành công",
                Data = new ResultRaitingVM
                {
                    MovieId = x.MovieId,
                    UserId = x.UserId,
                    CountStars = x.CountStars
                }

            });
            return _listRaitings.ToList();
        }
    }
}
