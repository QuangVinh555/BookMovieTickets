using BookMovieTickets.Data;
using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class BookTicketRepository : IBookTicketRepository
    {
        private readonly BookMovieTicketsContext _context;

        public BookTicketRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }
        public List<MessageVM> GetAll()
        {

            var _listBookTickets = _context.BookTickets.ToList();
            List<MessageVM> list = new List<MessageVM>();
            Combo _combo = null;
            ShowTime _showTime = null;
            HourTime _hourTime = null;
            CinemaRoom _roomCinema = null;
            CinemaName _nameCinema = null;
            foreach (var item in _listBookTickets)
            {
                try
                {
                    _showTime = _context.ShowTimes.Where(x => x.Id == item.ShowTimeId).SingleOrDefault();
                    _hourTime = _context.HourTimes.Where(x => x.Id == item.HourTimeId).SingleOrDefault();
                    _roomCinema = _context.CinemaRooms.Where(x => x.Id == _showTime.CinemaRoomId).SingleOrDefault();
                    _nameCinema = _context.CinemaNames.Where(x => x.Id == _roomCinema.CinemaNameId).SingleOrDefault();
                    _combo = _context.Combos.Where(x => x.Id == item.ComboId).SingleOrDefault();
                    var _bookTicket = new MessageVM
                    {
                        Message = "Lấy dữ liệu thành công",
                        Data = new BookTicketVM
                        {
                            Id = item.Id,
                            UserName = item.UserId == null ? "" : _context.Users.Where(y => y.Id == item.UserId).FirstOrDefault().Fullname,
                            Movie = item.MovieId == null ? "" : _context.Movies.Where(y => y.Id == item.MovieId).SingleOrDefault().Name,
                            Stamp = item.MovieId == null ? "" : _context.Movies.Where(y => y.Id == item.MovieId).SingleOrDefault().Stamp,
                            RoleMovie = _showTime == null ? "" : _showTime.Role,
                            ShowTime = _showTime == null ? DateTime.Now : _showTime.ShowDate,
                            HourTime = _hourTime == null ? "" : _hourTime.Time,
                            Payment = item.PaymentId == null ? "" : _context.Payments.Where(y => y.Id == item.PaymentId).SingleOrDefault().PaymentType,
                            CinemaName = _nameCinema == null ? "" : _nameCinema.Name,
                            Location = _nameCinema == null ? "" : _nameCinema.LocationDetail,
                            CinemaRoom = _roomCinema == null ? "" : _roomCinema.Name,
                            TotalTicket = item.TotalTickets == null ? 0 : item.TotalTickets,
                            NameCombo = _combo == null ? "" : _combo.Name,
                            CountCombo = item.CountCombo == null ? 0 : item.CountCombo,
                            TotalCombo = item.TotalCombo == null ? 0 : item.TotalCombo,
                            Total = item.TotalPrice == null ? 0 : item.TotalPrice,
                            RewardPoints = item.RewardPoints == null ? 0 : item.RewardPoints,
                            RewardPoints_Used = item.MoneyPoints == null ? 0 : item.MoneyPoints,
                            TotalPercent = item.TotalPercent == null ? 0 : item.TotalPercent,
                            CreatedAt = item.CreatedAt == null ? DateTime.Now : item.CreatedAt
                        }
                    };
                    list.Add(_bookTicket);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return null;
                }
            }
            return list;
        }

        public MessageVM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public MessageVM CreateBookTicket(BookTicketDTO dto)
        {
            try
            {
                var _bookTicket = new BookTicket();

                var _user = _context.Users.Where(x => x.Id == dto.UserId).SingleOrDefault();
                var _showTime = _context.ShowTimes.Where(x => x.Id == dto.ShowTimeId).SingleOrDefault();
                var _hourTime = _context.HourTimes.Where(x => x.Id == dto.HourTimeId).SingleOrDefault();

                var _listBookTickets = _context.BookTickets.ToList();
                foreach (var item in _listBookTickets)
                {
                    if (item.State == false && item.UserId == _user.Id)
                    {
                        _bookTicket.UserId = _user.Id;
                        _bookTicket.MovieId = null;
                        _bookTicket.ShowTimeId = _showTime.Id;
                        _bookTicket.HourTimeId = _hourTime.Id;
                        _bookTicket.ComboId = null;
                        _bookTicket.PaymentId = null;
                        _bookTicket.RewardPoints = null;
                        _bookTicket.MoneyPoints = null;
                        _bookTicket.CountCombo = null;
                        _bookTicket.TotalCombo = null;
                        _bookTicket.TotalTickets = null;
                        _bookTicket.TotalPrice = null;
                        _bookTicket.State = false;
                        _context.SaveChanges();
                        return new MessageVM
                        {
                            Message = "Tạo vé có sẵn thành công",
                        };
                    }
                }


                if (_user == null)
                {
                    return new MessageVM
                    {
                        Message = "Không tìm được thông tin người dùng này!"
                    };
                }
                if (_showTime == null)
                {
                    return new MessageVM
                    {
                        Message = "Không tìm được thông tin suất chiếu này!"
                    };
                }
                else
                {
                    if (_hourTime == null)
                    {
                        return new MessageVM
                        {
                            Message = "Không tìm được thông tin giờ chiếu này!"
                        };
                    }
                    else
                    {
                        if (_hourTime.ShowTimeId != _showTime.Id)
                        {
                            return new MessageVM
                            {
                                Message = "Không tìm được thông tin giờ chiếu này trong suất chiếu này!"
                            };
                        }
                    }
                }

                _bookTicket.UserId = _user.Id;
                _bookTicket.MovieId = null;
                _bookTicket.ShowTimeId = _showTime.Id;
                _bookTicket.HourTimeId = _hourTime.Id;
                _bookTicket.ComboId = null;
                _bookTicket.PaymentId = null;
                _bookTicket.RewardPoints = null;
                _bookTicket.MoneyPoints = null;
                _bookTicket.CountCombo = null;
                _bookTicket.TotalCombo = null;
                _bookTicket.TotalTickets = null;
                _bookTicket.TotalPrice = null;
                _bookTicket.State = false;
                _context.Add(_bookTicket);
                _context.SaveChanges();
                return new MessageVM
                {
                    Message = "Tạo vé thành công",
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return new MessageVM
                {
                    Message = e.Message
                };
            }
        }

        public MessageVM UpdateBookTicket(BookTicketDTO dto, int id)
        {
            try
            {
                var _bookTicket = _context.BookTickets.Where(x => x.State == false && x.Id == id).SingleOrDefault();
                if (_bookTicket != null)
                {
                    decimal TICKET_MEMBER = 5 / 100M;
                    decimal COMBO_MEMBER = 3 / 100M;
                    decimal TICKET_VIP = 7 / 100M;
                    decimal COMBO_VIP = 3 / 100M;
                    decimal TICKET_VVIP = 10 / 100M;
                    decimal COMBO_VVIP = 5 / 100M;

                    // giá vé
                    decimal priceTicket = 0;

                    // giá combo
                    decimal priceCombo = 0;

                    // điểm tích lũy vé
                    decimal accumulated_points_Ticket = 0;

                    // điểm tích lũy combo
                    decimal accumulated_points_Combo = 0;

                    // tổng điểm tích lũy
                    decimal total_accumulated_points = 0;

                    var _user = _context.Users.Where(x => x.Id == _bookTicket.UserId).SingleOrDefault();
                    if (_user == null)
                    {
                        return new MessageVM
                        {
                            Message = "Không tìm thấy thông tin user"
                        };
                    }
                    else
                    {
                        // Hạng member
                        if (_user.UserRankId == 1)
                        {
                            var _listBookTicketDetails = _context.BookTicketDetails.Where(x => x.State == false && x.BookTicketId == _bookTicket.Id).ToList();
                            if (_listBookTicketDetails.Count == 0)
                            {
                                return new MessageVM
                                {
                                    Message = "Bạn chưa chọn ghế!"
                                };
                            }
                            foreach (var item in _listBookTicketDetails)
                            {
                                if (item.ChairId != null)
                                {
                                    var _listChairStatus = _context.ChairStatuses.Where(x => x.ChairId == item.ChairId && x.HourTimeId == _bookTicket.HourTimeId).ToList();
                                    foreach (var _chair in _listChairStatus)
                                    {
                                        if (_chair.Status == 2)
                                        {
                                            return new MessageVM
                                            {
                                                Message = "Ghế bạn chọn đã có người khác đặt!"
                                            };
                                        }                                     
                                        _chair.Status = 2;
                                        _context.SaveChanges();
                                    }
                                    priceTicket = priceTicket + (decimal)(item.TicketPrice);
                                    accumulated_points_Ticket = Math.Round((TICKET_MEMBER * (decimal)(priceTicket) / 1000));
                                }
                            }
                            var _combo = _context.Combos.Where(x => x.Id == dto.ComboId).SingleOrDefault();
                            if (_combo != null)
                            {
                                priceCombo = (decimal)_combo.Price * (decimal)dto.CountCombo;
                                accumulated_points_Combo = Math.Round((COMBO_MEMBER * priceCombo) / 1000);
                            }

                            total_accumulated_points = accumulated_points_Ticket + accumulated_points_Combo;
                        }
                        // Hạng VIP
                        if (_user.UserRankId == 2)
                        {
                            var _listBookTicketDetails = _context.BookTicketDetails.Where(x => x.State == false && x.BookTicketId == _bookTicket.Id).ToList();
                            if (_listBookTicketDetails.Count == 0)
                            {
                                return new MessageVM
                                {
                                    Message = "Bạn chưa chọn ghế!"
                                };
                            }
                            foreach (var item in _listBookTicketDetails)
                            {
                                if (item.ChairId != null)
                                {
                                    var _listChairStatus = _context.ChairStatuses.Where(x => x.ChairId == item.ChairId && x.HourTimeId == _bookTicket.HourTimeId).ToList();
                                    foreach (var _chair in _listChairStatus)
                                    {
                                        if (_chair.Status == 2)
                                        {
                                            return new MessageVM
                                            {
                                                Message = "Ghế bạn chọn đã có người khác đặt!"
                                            };
                                        }
                                     
                                        _chair.Status = 2;
                                        _context.SaveChanges();
                                    }
                                    priceTicket = priceTicket + (decimal)(item.TicketPrice);
                                    accumulated_points_Ticket = Math.Round((TICKET_VIP * (decimal)(priceTicket) / 1000));
                                }
                            }
                            var _combo = _context.Combos.Where(x => x.Id == dto.ComboId).SingleOrDefault();
                            if (_combo != null)
                            {
                                priceCombo = (decimal)_combo.Price * (decimal)dto.CountCombo;
                                accumulated_points_Combo = Math.Round((COMBO_VIP * priceCombo) / 1000);
                            }

                            total_accumulated_points = accumulated_points_Ticket + accumulated_points_Combo;
                        }
                        // Hạng VVIP
                        if (_user.UserRankId == 3)
                        {
                            var _listBookTicketDetails = _context.BookTicketDetails.Where(x => x.State == false && x.BookTicketId == _bookTicket.Id).ToList();
                            if (_listBookTicketDetails.Count == 0)
                            {
                                return new MessageVM
                                {
                                    Message = "Bạn chưa chọn ghế!"
                                };
                            }
                            foreach (var item in _listBookTicketDetails)
                            {
                                if (item.ChairId != null)
                                {
                                    var _listChairStatus = _context.ChairStatuses.Where(x => x.ChairId == item.ChairId && x.HourTimeId == _bookTicket.HourTimeId).ToList();
                                    foreach (var _chair in _listChairStatus)
                                    {
                                        if (_chair.Status == 2)
                                        {
                                            return new MessageVM
                                            {
                                                Message = "Ghế bạn chọn đã có người khác đặt!"
                                            };
                                        }
                                      
                                        _chair.Status = 2;
                                        _context.SaveChanges();
                                    }
                                    priceTicket = priceTicket + (decimal)(item.TicketPrice);
                                    accumulated_points_Ticket = Math.Round((TICKET_VVIP * (decimal)(priceTicket) / 1000));
                                }
                            }
                            var _combo = _context.Combos.Where(x => x.Id == dto.ComboId).SingleOrDefault();
                            if (_combo != null)
                            {
                                priceCombo = (decimal)_combo.Price * (decimal)dto.CountCombo;
                                accumulated_points_Combo = Math.Round((COMBO_VVIP * priceCombo) / 1000);
                            }

                            total_accumulated_points = accumulated_points_Ticket + accumulated_points_Combo;
                        }

                        var _listCombos = _context.Combos.ToList();
                        foreach (var combo in _listCombos)
                        {
                            if (combo.Id == dto.ComboId)
                            {
                                if (combo.Count - dto.CountCombo < 0)
                                {
                                    return new MessageVM
                                    {
                                        Message = "Combo đã hết số lượng!"
                                    };
                                }
                                else
                                {
                                    combo.Count = combo.Count - dto.CountCombo;
                                }
                                _context.SaveChanges();
                            }
                        }

                        var _userPoint = _context.UserPoints.Where(x => x.UserId == _user.Id).SingleOrDefault();
                        if (_userPoint != null)
                        {
                            if (dto.MoneyPoints.HasValue)
                            {
                                if (_userPoint.RewardPoints - dto.MoneyPoints < 0)
                                {
                                    return new MessageVM
                                    {
                                        Message = "Số điểm tích lũy của bạn không đủ!"
                                    };
                                }
                                else
                                {
                                    _userPoint.RewardPoints = (_userPoint.RewardPoints - dto.MoneyPoints) + (int)total_accumulated_points;
                                    _userPoint.RewardPointsUsed = _userPoint.RewardPointsUsed + dto.MoneyPoints;
                                    _context.SaveChanges();
                                }
                            }
                            else
                            {
                                _userPoint.RewardPoints = _userPoint.RewardPoints + (int)total_accumulated_points;
                                _context.SaveChanges();
                            }
                        }

                        var _showTime = _context.ShowTimes.Where(x => x.Id == _bookTicket.ShowTimeId).SingleOrDefault();
                        var _payment = _context.Payments.Where(x => x.Id == dto.PaymentId).SingleOrDefault();
                        if (_payment == null)
                        {
                            return new MessageVM
                            {
                                Message = "Không tìm thấy thông tin payment"
                            };
                        }

                        _bookTicket.UserId = _user.Id;
                        _bookTicket.MovieId = _showTime.MovieId;
                        _bookTicket.ComboId = dto.ComboId == null ? null : dto.ComboId;
                        _bookTicket.PaymentId = _payment.Id;
                        _bookTicket.RewardPoints = (int)total_accumulated_points;
                        _bookTicket.MoneyPoints = dto.MoneyPoints == null ? 0 : dto.MoneyPoints * 1000;
                        _bookTicket.CountCombo = dto.CountCombo == null ? 0 : dto.CountCombo;
                        _bookTicket.TotalCombo = (int)priceCombo;
                        _bookTicket.TotalTickets = (int)priceTicket;
                        _bookTicket.TotalPrice = dto.MoneyPoints == null ? (double)(priceCombo + priceTicket) : (double)((priceCombo + priceTicket) - dto.MoneyPoints * 1000);
                        _bookTicket.State = true;
                        _bookTicket.UpdatedAt = DateTime.Now;
                        _context.SaveChanges();


                        var _listDetails = _context.BookTicketDetails.Where(x => x.BookTicketId == _bookTicket.Id && x.State == false).ToList();
                        foreach (var item in _listDetails)
                        {
                            item.State = true;
                            _context.SaveChanges();
                        }

                        var _roomCinema = _context.CinemaRooms.Where(x => x.Id == _showTime.CinemaRoomId).SingleOrDefault();
                        var _nameCinema = _context.CinemaNames.Where(x => x.Id == _roomCinema.CinemaNameId).SingleOrDefault();
                        return new MessageVM
                        {
                            Message = "Đặt vé thành công",
                            Data = new BookTicketVM
                            {
                                Id = _bookTicket.Id,
                                UserName = _context.Users.Where(y => y.Id == _bookTicket.UserId).FirstOrDefault().Fullname,
                                Movie = _context.Movies.Where(y => y.Id == _showTime.MovieId).SingleOrDefault().Name,
                                Stamp = _context.Movies.Where(y => y.Id == _showTime.MovieId).SingleOrDefault().Stamp,
                                RoleMovie = _context.ShowTimes.Where(y => y.Id == _bookTicket.ShowTimeId).SingleOrDefault().Role,
                                ShowTime = _context.ShowTimes.Where(y => y.Id == _bookTicket.ShowTimeId).SingleOrDefault().ShowDate,
                                HourTime = _context.HourTimes.Where(y => y.Id == _bookTicket.HourTimeId).FirstOrDefault().Time,
                                Payment = _context.Payments.Where(y => y.Id == dto.PaymentId).SingleOrDefault().PaymentType,
                                CinemaName = _nameCinema.Name,
                                Location = _nameCinema.LocationDetail,
                                CinemaRoom = _roomCinema.Name,
                                TotalTicket = _bookTicket.TotalTickets,
                                NameCombo = dto.ComboId == null ? "" : _context.Combos.Where(x => x.Id == dto.ComboId).SingleOrDefault().Name,
                                CountCombo = dto.CountCombo == null ? 0 : dto.CountCombo,
                                TotalCombo = _bookTicket.TotalCombo,
                                Total = _bookTicket.TotalPrice,
                                RewardPoints_Used = _bookTicket.MoneyPoints,
                                RewardPoints = _bookTicket.RewardPoints,
                                TotalPercent = _bookTicket.TotalPercent,
                                CreatedAt = _bookTicket.UpdatedAt
                            }
                        };
                    }
                }
                else
                {
                    return new MessageVM
                    {
                        Message = "Không tìm thấy vé đã được tạo"
                    };
                }
            }

            catch (Exception e)
            {
                return new MessageVM
                {
                    Message = e.Message
                };
            }
        }

        public MessageVM DeleteBookTicket(int id)
        {
            throw new NotImplementedException();
        }

    }
}
