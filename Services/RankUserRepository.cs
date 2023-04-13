using BookMovieTickets.Data;
using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class RankUserRepository : IRankUserRepository
    {
        private readonly BookMovieTicketsContext _context;

        public RankUserRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }
        public MessageVM CreateRankUser(RankUserDTO dto)
        {
            var _rankUser = new UserRank();
            var _rankUsers = _context.LoginTypes.ToList();
            if (_rankUsers.Count > 0)
            {
                foreach (var rankUser in _rankUsers)
                {
                    if (string.Compare(rankUser.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        return new MessageVM
                        {
                            Message = "Tên rank User đã tồn tại"
                        };
                    }
                }
                _rankUser.Name = dto.Name;
                _rankUser.Benchmark = dto.Benchmark;
                _context.Add(_rankUser);
                _context.SaveChanges();
                return new MessageVM
                {
                    Message = "Đã thêm thành công",
                    Data = new TypeLoginVM
                    {
                        Id = _rankUser.Id,
                        Name = _rankUser.Name
                    }
                };
            }
            else
            {
                _rankUser.Name = dto.Name;
                _rankUser.Benchmark = dto.Benchmark;
                _context.Add(_rankUser);
                _context.SaveChanges();
                return new MessageVM
                {
                    Message = "Đã thêm thành công",
                    Data = new TypeLoginVM
                    {
                        Id = _rankUser.Id,
                        Name = _rankUser.Name
                    }
                };
            }
        }
    }
}
