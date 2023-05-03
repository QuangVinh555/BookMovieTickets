using BookMovieTickets.Data;
using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class ShowTimeRepository : IShowTimeRepository
    {
        private readonly BookMovieTicketsContext _context;

        public ShowTimeRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }

        public MessageVM CreateShowTime(ShowTimeDTO dto)
        {
            try
            {
                var _showTime = new ShowTime();
                var _listShowTimes = _context.ShowTimes.ToList();
                foreach (var showtime in _listShowTimes)
                {
                    if (showtime.State == false)
                    {
                        _showTime.Id = _context.ShowTimes.Where(x => x.Id == showtime.Id).SingleOrDefault().Id;
                        _showTime.CinemaRoomId = null;
                        _showTime.MovieId = null;
                        _showTime.ShowDate = null;
                        _showTime.TicketPrice = null;
                        _showTime.NumTicket = null;
                        _showTime.Role = null;
                        _showTime.State = false;
                        _context.SaveChanges();
                        return new MessageVM
                        {
                            Message = "Tạo thành công suất chiếu có sẵn"
                        };
                    }
                }
                _showTime.CinemaRoomId = null;
                _showTime.MovieId = null;
                _showTime.ShowDate = null;
                _showTime.TicketPrice = null;
                _showTime.NumTicket = null;
                _showTime.Role = null;
                _showTime.State = false;
                _context.Add(_showTime);
                _context.SaveChanges();

                return new MessageVM
                {
                    Message = "Tạo thành công suất chiếu"
                };
            }
            catch(Exception e)
            {
                return new MessageVM
                {
                    Message = e.Message
                };
            }
        }

        public MessageVM DeleteShowTime(int id)
        {
            var _showtime = _context.ShowTimes.Where(x => x.Id == id).SingleOrDefault();
            if(_showtime != null) {
                if(_showtime.State == false)
                {
                    return new MessageVM
                    {
                        Message = "Suất chiếu chưa được tạo xong"
                    };
                }
                else
                {
                    var _bookTicketDetail = _context.BookTicketDetails.Where(x => x.ShowTimeId == _showtime.Id).ToList();
                    var _hourTime = _context.HourTimes.Where(x => x.ShowTimeId == _showtime.Id).ToList();
                    _context.RemoveRange(_bookTicketDetail);
                    _context.RemoveRange(_hourTime);

                    _context.Remove(_showtime);
                    _context.SaveChanges();
                    return new MessageVM
                    {
                        Message = "Đã xóa suất chiếu thành công"
                    };
                }
            }
            else
            {
                return new MessageVM
                {
                    Message = "Không tìm thấy thông tin id này!",
                };
            }
        }

        public List<MessageVM> GetAll()
        {
            var _listShowTimes = _context.ShowTimes.Select(x => new MessageVM
            {
                Message = "Lấy dữ liệu thành công",
                Data = new ShowTimeVM
                {
                    Id = x.Id,
                    CinemaRoomId = x.CinemaRoomId,
                    MovieId = x.MovieId,
                    ShowDate = x.ShowDate,
                    TicketPrice = x.TicketPrice,
                    NumTicket = x.NumTicket,
                    Role = x.Role,
                    State = x.State
                }
            }).ToList();
            return _listShowTimes;
        }

        public MessageVM GetById(int id)
        {
            var _showTime = _context.ShowTimes.Where(x => x.Id == id).SingleOrDefault();
            if(_showTime != null)
            {
                return new MessageVM
                {
                    Message = "Lấy dữ liệu thành công",
                    Data = new ShowTimeVM
                    {
                        Id = _showTime.Id,
                        CinemaRoomId = _showTime.CinemaRoomId,
                        MovieId = _showTime.MovieId,
                        ShowDate = _showTime.ShowDate,
                        TicketPrice = _showTime.TicketPrice,
                        NumTicket = _showTime.NumTicket,
                        Role = _showTime.Role,
                        State = _showTime.State
                    }
                };
            }
            else
            {
                return new MessageVM
                {
                    Message = "Không tìm thấy thông tin id này!",
                };
            }
        }

        public MessageVM GetByMovieId(int movie_id)
        {
            var _showTime = _context.ShowTimes.Where(x => x.MovieId == movie_id).ToList();
            if (_showTime.Count > 0)
            {
                var listShowTimes = _showTime.Select(x => new ShowTimeVM
                {
                        Id = x.Id,
                        CinemaRoomId = x.CinemaRoomId,
                        MovieId = x.MovieId,
                        ShowDate = x.ShowDate,
                        TicketPrice = x.TicketPrice,
                        NumTicket = x.NumTicket,
                        Role = x.Role,
                        State = x.State
                });
                return new MessageVM
                {
                    Message = "Lấy dữ liệu thành công",
                    Data = listShowTimes
                };
            }
            else
            {
                return new MessageVM
                {
                    Message = "Không tìm thấy thông tin id này!",
                };
            }
        }

        public MessageVM UpdateShowTime(ShowTimeDTO dto, int id)
        {
            try
            {
                var _showTime = _context.ShowTimes.Where(x => x.Id == id).SingleOrDefault();
                var _movie = _context.Movies.Where(x => x.Id == dto.MovieId).SingleOrDefault();
                var _cinemaRoomId = _context.CinemaRooms.Where(x => x.Id == dto.CinemaRoomId).SingleOrDefault();
                //DateTime datetime = DateTime.ParseExact(dto.ShowDate.ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                if (_showTime != null)
                {
                    if (_showTime.State == false)
                    {

                        if (_movie == null)
                        {
                            return new MessageVM
                            {
                                Message = "Nhập sai thông tin id movie",
                            };
                        }
                        if (_cinemaRoomId == null)
                        {
                            return new MessageVM
                            {
                                Message = "Nhập sai thông tin id cinemaRoom",
                            };
                        }
                        _showTime.CinemaRoomId = _cinemaRoomId.Id;
                        _showTime.MovieId = _movie.Id;
                        _showTime.ShowDate = dto.ShowDate;
                        _showTime.TicketPrice = dto.TicketPrice;
                        _showTime.NumTicket = dto.NumTicket;
                        _showTime.Role = dto.Role;
                        _showTime.State = true;
                        _context.SaveChanges();
                        return new MessageVM
                        {
                            Message = "Cập nhật đầy đủ thông tin suất chiếu thành công",
                            Data = new ShowTimeVM
                            {
                                Id = _showTime.Id,
                                CinemaRoomId = _showTime.CinemaRoomId,
                                MovieId = _showTime.MovieId,
                                ShowDate = _showTime.ShowDate,
                                TicketPrice = _showTime.TicketPrice,
                                NumTicket = _showTime.NumTicket,
                                Role = _showTime.Role,
                                State = _showTime.State
                            }
                        };
                    }
                    else
                    {
                        _showTime.CinemaRoomId = _cinemaRoomId.Id;
                        _showTime.MovieId = _movie.Id;
                        _showTime.ShowDate = dto.ShowDate;
                        _showTime.TicketPrice = dto.TicketPrice;
                        _showTime.NumTicket = dto.NumTicket;
                        _showTime.Role = dto.Role;
                        _showTime.State = true;
                        _context.SaveChanges();
                        return new MessageVM
                        {
                            Message = "Cập nhật suất chiếu thành công",
                            Data = new ShowTimeVM
                            {
                                Id = _showTime.Id,
                                CinemaRoomId = _showTime.CinemaRoomId,
                                MovieId = _showTime.MovieId,
                                ShowDate = _showTime.ShowDate,
                                TicketPrice = _showTime.TicketPrice,
                                NumTicket = _showTime.NumTicket,
                                Role = _showTime.Role,
                                State = _showTime.State
                            }
                        };
                    }
                }
                else
                {
                    return new MessageVM
                    {
                        Message = "Không tìm thấy thông tin của suất chiếu này"
                    };
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return new MessageVM
                {
                    Message = e.Message
                };
            }
        }
    }
}
