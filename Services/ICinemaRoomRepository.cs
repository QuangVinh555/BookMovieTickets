using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public interface ICinemaRoomRepository
    {
        List<MessageVM> GetAll();
        MessageVM CreateCinemaRoom(CinemaRoomDTO dto);
        MessageVM GetById(int id);
        MessageVM UpdateCinemaRoom(CinemaRoomDTO dto, int id);
        MessageVM DeleteCinemaRoom(int id);
    }
}
