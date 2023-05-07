using BookMovieTickets.Models;
using BookMovieTickets.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTicketDetailController : ControllerBase
    {
        private readonly IBookTicketDetailRepository _bookTicketDetailRepository;

        public BookTicketDetailController(IBookTicketDetailRepository bookTicketDetailRepository)
        {
            _bookTicketDetailRepository = bookTicketDetailRepository;
        }
        [HttpPost]
        public IActionResult CreateBookTicketDetail(BookTicketDetailDTO dto)
        {
            try
            {
                return Ok(_bookTicketDetailRepository.CreateBookTicketDetail(dto));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_bookTicketDetailRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_bookTicketDetailRepository.GetById(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("bookticket/{bookTicketId}")]
        public IActionResult GetByTicketId(int bookTicketId)
        {
            try
            {
                return Ok(_bookTicketDetailRepository.GetByTicketId(bookTicketId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookTicketDetail(BookTicketDetailDTO dto, int id)
        {
            try
            {
                return Ok(_bookTicketDetailRepository.UpdateBookTicketDetail(dto, id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCombo(int id)
        {
            try
            {
                return Ok(_bookTicketDetailRepository.DeleteBookTicketDetail(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
