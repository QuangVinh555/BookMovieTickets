using BookMovieTickets.Data;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class ChairStatusRepository : IChairStatusRepository
    {
        private readonly BookMovieTicketsContext _context;

        public ChairStatusRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }
        public List<MessageVM> GetAllChairByHourTimeId(int HourTimeId)
        {
            List<MessageVM> list = new List<MessageVM>();
            var _listChairs = _context.ChairStatuses.Where(x => x.HourTimeId == HourTimeId).ToList();
            foreach (var item in _listChairs)
            {
                var _chair = new MessageVM
                {
                    Message = "Lấy dữ liệu thành công",
                    Data = new ChairStatusVM
                    {
                        Id = item.Id,
                        Chair = _context.Chairs.Where(x => x.Id == item.ChairId).SingleOrDefault().Name
                    }
                };
                list.Add(_chair);
            }
            return list;
        }
    }
}
