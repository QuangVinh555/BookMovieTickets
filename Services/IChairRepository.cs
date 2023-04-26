using BookMovieTickets.Models;
using BookMovieTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public interface IChairRepository
    {
        List<MessageVM> GetAll();
        MessageVM GetById(int id);
        MessageVM CreateChair(ChairDTO dto);
        MessageVM UpdateChair(ChairDTO dto, int id);
        MessageVM DeleteChair(int id);
    }
}
