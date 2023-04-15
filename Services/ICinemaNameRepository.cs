using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public interface ICinemaNameRepository
    {
        MessageVM CreateCinemaName(CinemaNameDTO dto);
        List<MessageVM> GetAll();
    }
}
