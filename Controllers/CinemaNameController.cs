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
    public class CinemaNameController : ControllerBase
    {
        private readonly ICinemaNameRepository _cinemaNameRepository;

        public CinemaNameController(ICinemaNameRepository cinemaNameRepository)
        {
            _cinemaNameRepository = cinemaNameRepository;
        }

        [HttpPost]
        public IActionResult CreateCinemaName(CinemaNameDTO dto)
        {
            try
            {
                return Ok(_cinemaNameRepository.CreateCinemaName(dto));
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
                return Ok(_cinemaNameRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
