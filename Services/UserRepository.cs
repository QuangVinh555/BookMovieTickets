using BookMovieTickets.Data;
using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly BookMovieTicketsContext _context;

        public UserRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }
        public MessageVM CreateUser(UserDTO dto)
        {
            var _user = new User();
            var _listUsers = _context.Users.ToList();
            foreach(var user in _listUsers)
            {
                if (string.Compare(user.Email, dto.Email, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    return new MessageVM
                    {
                        Message = "Email đã tồn tại"
                    };
                }
            }
            var _role = _context.Roles.Where(x => x.Id == dto.RoleId).SingleOrDefault();
            if(_role == null)
            {
                return new MessageVM
                {
                    Message = "Lỗi khi tìm RoleId"
                };
            }
            var _loginType = _context.LoginTypes.Where(x => x.Id == dto.LoginTypeId).SingleOrDefault();
            if (_loginType == null)
            {
                return new MessageVM
                {
                    Message = "Lỗi khi tìm LoginTypeId"
                };
            }
            var _rankUser = _context.UserRanks.Where(x => x.Id == dto.UserRankId).SingleOrDefault();
            if (_rankUser == null)
            {
                return new MessageVM
                {
                    Message = "Lỗi khi tìm RankUserId"
                };
            }
            byte[] bytes = Encoding.UTF8.GetBytes(dto.Password);
            string passwordEncoding = Convert.ToBase64String(bytes);
            _user.RoleId = _role.Id;
            _user.LoginTypeId = _loginType.Id;
            _user.UserRankId = _rankUser.Id;
            _user.Avatar = dto.Avatar;
            _user.Fullname = dto.Fullname;
            _user.Email = dto.Email;
            _user.Date = dto.Date;
            _user.PhoneNumber = dto.PhoneNumber;
            _user.Address = dto.Address;
            _user.Password = passwordEncoding;
            _context.Add(_user);
            _context.SaveChanges();

            var _userPoint = new UserPoint();
            _userPoint.UserId = _user.Id;
            _userPoint.RewardPoints = 0;
            _userPoint.RewardPointsUsed = 0;
            _context.Add(_userPoint);
            _context.SaveChanges();

            return new MessageVM
            {
                Message = "Thêm user thành công",
                Data = new UserVM
                {
                    Id = _user.Id,
                    RoleId = _user.RoleId,
                    LoginTypeId = _user.LoginTypeId,
                    UserRankId = _user.UserRankId,
                    Avatar = _user.Avatar,
                    Fullname = _user.Fullname,
                    Email = _user.Email,
                    Date = _user.Date,
                    PhoneNumber = _user.PhoneNumber,
                    Address = _user.Address,
                    Password = _user.Password,
                }
            };
        }

        public MessageVM DeleteUser(int id)
        {
            var _user = _context.Users.Where(x => x.Id == id).SingleOrDefault();
            if(_user != null)
            {
                try
                {
                    return null;
                }
                catch(Exception e)
                {
                    return new MessageVM
                    {
                        Message = e.Message
                    };
                }
            }
            else
            {
                return new MessageVM
                {
                    Message = "Lỗi không tìm thấy id cần xóa"
                };
            }
        }

        public List<MessageVM> GetAll()
        {
            var _listUsers = _context.Users.Select(x => new MessageVM
            {
                Message = "Lấy dữ liệu thành công",
                Data = new UserVM
                {
                    Id = x.Id,
                    RoleId = x.RoleId,
                    LoginTypeId = x.LoginTypeId,
                    UserRankId = x.UserRankId,
                    Avatar = x.Avatar,
                    Fullname = x.Fullname,
                    Email = x.Email,
                    Date = x.Date,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    Password = x.Password,
                    CreatedAt = x.CreatedAt,

                }
            }).ToList();
            return _listUsers;
        }

        public MessageVM GetById(int id)
        {
            var _user = _context.Users.Where(x => x.Id == id).SingleOrDefault();
            if(_user != null)
            {
                return new MessageVM
                {
                    Message = "Lấy dữ liệu thành công",
                    Data = new UserVM
                    {
                        Id = _user.Id,
                        RoleId = _user.RoleId,
                        LoginTypeId = _user.LoginTypeId,
                        UserRankId = _user.UserRankId,
                        Avatar = _user.Avatar,
                        Fullname = _user.Fullname,
                        Email = _user.Email,
                        Date = _user.Date,
                        PhoneNumber = _user.PhoneNumber,
                        Address = _user.Address,
                        Password = _user.Password,
                    }
                };
            }
            else
            {
                return new MessageVM{
                    Message = "Không tìm thấy thông tin Id này"
                };
            }
        }

        public MessageVM UpdateUser(UserDTO dto, int id)
        {
            var _role = _context.Roles.Where(x => x.Id == dto.RoleId).SingleOrDefault();
            if (_role == null)
            {
                return new MessageVM
                {
                    Message = "Lỗi khi tìm RoleId"
                };
            }

            var _loginType = _context.LoginTypes.Where(x => x.Id == dto.LoginTypeId).SingleOrDefault();
            if (_loginType == null)
            {
                return new MessageVM
                {
                    Message = "Lỗi khi tìm LoginTypeId"
                };
            }

            var _rankUser = _context.UserRanks.Where(x => x.Id == dto.UserRankId).SingleOrDefault();
            if (_rankUser == null)
            {
                return new MessageVM
                {
                    Message = "Lỗi khi tìm RankUserId"
                };
            }

            byte[] bytes = Encoding.UTF8.GetBytes(dto.Password);
            string passwordEncoding = Convert.ToBase64String(bytes);

            var _user = _context.Users.Where(x => x.Id == id).SingleOrDefault();
            if(_user != null)
            {
                _user.LoginTypeId = _loginType.Id;
                _user.UserRankId = _rankUser.Id;
                _user.Avatar = dto.Avatar;
                _user.Fullname = dto.Fullname;
                _user.Email = dto.Email;
                _user.PhoneNumber = dto.PhoneNumber;
                _user.Address = dto.Address;
                _user.Password = passwordEncoding;
                _context.SaveChanges();
                return new MessageVM
                {
                    Message = "Cập nhật thông tin thành công",
                    Data = new UserVM
                    {
                        Id = _user.Id,
                        RoleId = _user.RoleId,
                        LoginTypeId = _user.LoginTypeId,
                        UserRankId = _user.UserRankId,
                        Avatar = _user.Avatar,
                        Fullname = _user.Fullname,
                        Email = _user.Email,
                        Date = _user.Date,
                        PhoneNumber = _user.PhoneNumber,
                        Address = _user.Address,
                        Password = _user.Password,
                    }
                };
            }
            else
            {
                return new MessageVM
                {
                    Message = "Không tìm thấy thông tin của Id này"
                };
            }
        }
    }
}
