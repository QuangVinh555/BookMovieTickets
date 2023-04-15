using BookMovieTickets.Data;
using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class CinemaNameRepository : ICinemaNameRepository
    {
        private readonly BookMovieTicketsContext _context;

        public CinemaNameRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }
        public MessageVM CreateCinemaName(CinemaNameDTO dto)
        {
            var _cinemaName = new CinemaName();
            var _listCinemaNames = _context.CinemaNames.ToList();
            if(_listCinemaNames.Count > 0)
            {
                foreach(var cinemaName in _listCinemaNames)
                {
                    if (string.Compare(cinemaName.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        return new MessageVM
                        {
                            Message = "Tên đã tồn tại"
                        };
                    }
                }
                _cinemaName.CinemaTypeId = _context.CinemaTypes.Where(x => x.Id == dto.CinemaTypeId).SingleOrDefault().Id;
                _cinemaName.LocationId = _context.Locations.Where(x => x.Id == dto.LocationId).SingleOrDefault().Id;
                _cinemaName.Name = dto.Name;
                _cinemaName.LocationDetail = dto.LocationDetail;
                _context.Add(_cinemaName);
                _context.SaveChanges();
                return new MessageVM
                {
                    Message = "Thêm thành công",
                    Data = new CinemaNameVM
                    {
                        Id = _cinemaName.Id,
                        CinemaTypeId = _cinemaName.CinemaTypeId,
                        LocationId = _cinemaName.LocationId,
                        Name = _cinemaName.Name,
                        LocationDetail = _cinemaName.LocationDetail,
                    }
                };
            }
            else
            {
                _cinemaName.CinemaTypeId = _context.CinemaTypes.Where(x => x.Id == dto.CinemaTypeId).SingleOrDefault().Id;
                _cinemaName.LocationId = _context.Locations.Where(x => x.Id == dto.LocationId).SingleOrDefault().Id;
                _cinemaName.Name = dto.Name;
                _cinemaName.LocationDetail = dto.LocationDetail;
                _context.Add(_cinemaName);
                _context.SaveChanges();
                return new MessageVM
                {
                    Message = "Thêm thành công",
                    Data = new CinemaNameVM
                    {
                        Id = _cinemaName.Id,
                        CinemaTypeId = _cinemaName.CinemaTypeId,
                        LocationId = _cinemaName.LocationId,
                        Name = _cinemaName.Name,
                        LocationDetail = _cinemaName.LocationDetail,
                    }
                };
            }
        }

        public List<MessageVM> GetAll()
        {
            var _listCinemaNames = _context.CinemaNames.Select(x => new MessageVM
            {
                Message = "Lấy dữ liệu thành công",
                Data = new CinemaNameVM
                {
                    Id = x.Id,
                    CinemaTypeId = x.CinemaTypeId,
                    LocationId = x.Id,
                    Name = x.Name,
                    LocationDetail = x.LocationDetail
                }
            }).ToList();

            return _listCinemaNames;
        }
    }
}
