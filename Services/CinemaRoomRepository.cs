using BookMovieTickets.Data;
using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class CinemaRoomRepository : ICinemaRoomRepository
    {
        private readonly BookMovieTicketsContext _context;

        public CinemaRoomRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }
        public MessageVM CreateCinemaRoom(CinemaRoomDTO dto)
        {
            var _cinemaRoom = new CinemaRoom();
            var _listCinemaRooms = _context.CinemaRooms.ToList();
            var _cinemaName = _context.CinemaNames.Where(x => x.Id == dto.CinemaNameId).SingleOrDefault();
            if(_cinemaName == null)
            {
                return new MessageVM
                {
                    Message = "Phải điền thông tin rạp cho phòng chiếu này"
                };
            }
            else
            {
                foreach (var cinemaRoom in _listCinemaRooms)
                {
                    if(_cinemaName.Id == cinemaRoom.CinemaNameId)
                    {
                        if (string.Compare(cinemaRoom.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase) == 0)
                        {
                            return new MessageVM
                            {
                                Message = "Tên đã tồn tại"
                            };
                        }
                    }
                }
                _cinemaRoom.CinemaNameId = _cinemaName.Id;
                _cinemaRoom.Name = dto.Name;
                _context.Add(_cinemaRoom);
                _context.SaveChanges();
                return new MessageVM
                {
                    Message = "Thêm phòng chiếu thành công",
                    Data = new CinemaRoomVM
                    {
                        Id = _cinemaRoom.Id,
                        Name = _cinemaRoom.Name,
                        CinemaNameId = _cinemaRoom.CinemaNameId
                    }
                };
            }
            
        }

        public MessageVM DeleteCinemaRoom(int id)
        {
            throw new NotImplementedException();
        }

        public List<MessageVM> GetAll()
        {
            var _listCinemaRooms = _context.CinemaRooms.Select(x => new MessageVM
            {
                Message = "Lấy dữ liệu thành công",
                Data = new CinemaRoomVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    CinemaNameId = x.CinemaNameId
                }
            }).ToList();
            return _listCinemaRooms;
        }

        public MessageVM GetById(int id)
        {
            var _cinemaRoom = _context.CinemaRooms.Where(x => x.Id == id).SingleOrDefault();
            if(_cinemaRoom != null)
            {
                return new MessageVM
                {
                    Message = "Lấy dữ liệu thành công",
                    Data = new CinemaRoomVM
                    {
                        Id = _cinemaRoom.Id,
                        Name = _cinemaRoom.Name,
                        CinemaNameId = _cinemaRoom.CinemaNameId
                    }
                };
            }
            else
            {
                return new MessageVM
                {
                    Message = "Không tìm thấy Id này"
                };
            }
        }

        public MessageVM UpdateCinemaRoom(CinemaRoomDTO dto, int id)
        {
            var _cinemaRoom = _context.CinemaRooms.Where(x => x.Id == id).SingleOrDefault();
            var _cinemaName = _context.CinemaNames.Where(x => x.Id == dto.CinemaNameId).SingleOrDefault();
            var _listCinemaRooms = _context.CinemaRooms.ToList();
            if (_cinemaRoom != null)
            {
                foreach (var cinemaRoom in _listCinemaRooms)
                {
                    if (cinemaRoom.CinemaNameId == _cinemaName.Id)
                    {
                        if (string.Compare(cinemaRoom.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase) == 0)
                        {
                            return new MessageVM
                            {
                                Message = "Tên đã tồn tại"
                            };
                        }
                    }
                }
            
                _cinemaRoom.Name = dto.Name;
                _cinemaRoom.CinemaNameId = _cinemaName.Id;
                _context.SaveChanges();
                return new MessageVM
                {
                    Message = "Tên này đã tồn tại",
                    Data = new CinemaRoomVM
                    {
                        Id = _cinemaRoom.Id,
                        Name = _cinemaRoom.Name,
                        CinemaNameId = _cinemaRoom.CinemaNameId
                    }
                };
            }
            else
            {
                return new MessageVM
                {
                    Message = "Không tìm thấy Id này",                
                };
            }
        }
    }
}
