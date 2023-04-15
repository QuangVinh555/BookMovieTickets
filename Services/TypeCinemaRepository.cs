using BookMovieTickets.Data;
using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class TypeCinemaRepository : ITypeCinemaRepository
    {
        private readonly BookMovieTicketsContext _context;

        public TypeCinemaRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }

        public MessageVM CreateTypeCinema(TypeCinemaDTO dto)
        {
            var _typeCinema = new CinemaType
            {
                Name = dto.Name
            };
            _context.Add(_typeCinema);
            _context.SaveChanges();
            return new MessageVM
            {
                Message = "Thêm thành công",
                Data = new TypeCinemaVM
                {
                    Id = _typeCinema.Id,
                    Name = _typeCinema.Name
                }
            };
        }

        public List<MessageVM> GetAll()
        {
            var _listTypeCinemas = _context.CinemaTypes.Select(x => new MessageVM
            {
                Message = "Lấy dữ liệu thành công",
                Data = new TypeCinemaVM
                {
                    Id = x.Id,
                    Name = x.Name
                }
            }).ToList();
            return _listTypeCinemas;
        }
    }
}
