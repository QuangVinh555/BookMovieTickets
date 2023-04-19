﻿using BookMovieTickets.Data;
using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly BookMovieTicketsContext _context;

        public MovieRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }
        public MessageVM CreateMovie(MovieDTO dto)
        {
            var _movie = new Movie();
            var _listMovies = _context.Movies.ToList();
            var _user = _context.Users.Where(x => x.Id == dto.UserId).SingleOrDefault();
            if(_user.RoleId != 1)
            {
                return new MessageVM
                {
                    Message = "Không có quyền ở chức năng này!"
                };
            }
            foreach(var movie in _listMovies)
            {
                if (string.Compare(movie.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    return new MessageVM
                    {
                        Message = "Tên đã tồn tại"
                    };
                }
            }
            _movie.UserId = _user.Id;
            _movie.Name = dto.Name;
            _movie.Description = dto.Description;
            _movie.Content = dto.Content;
            _movie.Stamp = dto.Stamp;
            _movie.Nation = dto.Nation;
            _movie.MovieDuration = dto.MovieDuration;
            _movie.PremiereDate = dto.PremiereDate;
            _movie.PremiereYear = dto.PremiereYear;
            _movie.Author = dto.Author;
            _movie.Actor = dto.Actor;
            _movie.Producer = dto.Producer;
            _context.Add(_movie);
            _context.SaveChanges();
            return new MessageVM { 
                Message = "Thêm thành công",
                Data = new MovieVM
                {
                    Id = _movie.Id,
                    UserId =  _movie.UserId,
                    Name = _movie.Name ,
                    Description = _movie.Description,
                    Content = _movie.Content ,
                    Stamp = _movie.Stamp,
                    Nation = _movie.Nation ,
                    MovieDuration = _movie.MovieDuration,
                    PremiereDate = _movie.PremiereDate,
                    PremiereYear = _movie.PremiereYear,
                    Author = _movie.Author,
                    Actor = _movie.Actor,
                    Producer = _movie.Producer
                }
            };
        }

        public MessageVM DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public List<MessageVM> GetAll()
        {
            var _listMovies = _context.Movies.Select(x => new MessageVM
            {
                Message = "Lấy dữ liệu thành công",
                Data = new MovieVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.Name,
                    Description = x.Description,
                    Content = x.Content,
                    Stamp = x.Stamp,
                    Nation = x.Nation,
                    MovieDuration = x.MovieDuration,
                    PremiereDate = x.PremiereDate,
                    PremiereYear = x.PremiereYear,
                    Author = x.Author,
                    Actor = x.Actor,
                    Producer = x.Producer
                }
            }).ToList();
            return _listMovies;
        }

        public MessageVM GetById(int id)
        {
            var _movie = _context.Movies.Where(x => x.Id == id).SingleOrDefault();
            if(_movie != null)
            {
                return new MessageVM
                {
                    Message = "Lấy dữ liệu thành công",
                    Data = new MovieVM
                    {
                        Id = _movie.Id,
                        UserId = _movie.UserId,
                        Name = _movie.Name,
                        Description = _movie.Description,
                        Content = _movie.Content,
                        Stamp = _movie.Stamp,
                        Nation = _movie.Nation,
                        MovieDuration = _movie.MovieDuration,
                        PremiereDate = _movie.PremiereDate,
                        PremiereYear = _movie.PremiereYear,
                        Author = _movie.Author,
                        Actor = _movie.Actor,
                        Producer = _movie.Producer
                    }
                };
            }
            else
            {
                return new MessageVM
                {
                    Message = "Không lấy được thông tin của Id này"
                };
            }
        }

        public MessageVM UpdateMovie(MovieDTO dto, int id)
        {
            var _movie = _context.Movies.Where(x => x.Id == id).SingleOrDefault();
            var _listMovies = _context.Movies.ToList();
            var _user = _context.Users.Where(x => x.Id == dto.UserId).SingleOrDefault();
            if (_user.RoleId != 1)
            {
                return new MessageVM
                {
                    Message = "Không có quyền ở chức năng này!"
                };
            }
            foreach (var movie in _listMovies)
            {
                if (string.Compare(movie.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    return new MessageVM
                    {
                        Message = "Tên đã tồn tại"
                    };
                }
            }
            if(_movie != null)
            {
                _movie.UserId = _user.Id;
                _movie.Name = dto.Name;
                _movie.Description = dto.Description;
                _movie.Content = dto.Content;
                _movie.Stamp = dto.Stamp;
                _movie.Nation = dto.Nation;
                _movie.MovieDuration = dto.MovieDuration;
                _movie.PremiereDate = dto.PremiereDate;
                _movie.PremiereYear = dto.PremiereYear;
                _movie.Author = dto.Author;
                _movie.Actor = dto.Actor;
                _movie.Producer = dto.Producer;

                _context.SaveChanges();
                return new MessageVM
                {
                    Message = "Cập nhật thành công",
                    Data = new MovieVM
                    {
                        Id = _movie.Id,
                        UserId = _movie.UserId,
                        Name = _movie.Name,
                        Description = _movie.Description,
                        Content = _movie.Content,
                        Stamp = _movie.Stamp,
                        Nation = _movie.Nation,
                        MovieDuration = _movie.MovieDuration,
                        PremiereDate = _movie.PremiereDate,
                        PremiereYear = _movie.PremiereYear,
                        Author = _movie.Author,
                        Actor = _movie.Actor,
                        Producer = _movie.Producer
                    }
                };
            }
            else
            {
                return new MessageVM
                {
                    Message = "Không tìm thấy thông tin Id này"
                };
            }
        }
    }
}
