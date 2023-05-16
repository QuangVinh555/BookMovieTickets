using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public interface IUserRepository
    {
        List<MessageVM> GetAll(int page);
        MessageVM GetById(int id);
        MessageVM CreateUser(UserDTO dto);
        MessageVM UpdateUser(UserDTO dto, int id);
        MessageVM DeleteUser(int id);
    }
}
