using BookMovieTickets.Models;
using BookMovieTickets.Services;
using BookMovieTickets.Views;
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
    public class ShowTimeController : ControllerBase
    {
        private readonly IShowTimeRepository _showTimeRepository;

        public ShowTimeController(IShowTimeRepository showTimeRepository)
        {
            _showTimeRepository = showTimeRepository;
        }

        [HttpPost]
        public IActionResult CreateShowTime(ShowTimeDTO dto)
        {
            try
            {
                return Ok(_showTimeRepository.CreateShowTime(dto));
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
                return Ok(_showTimeRepository.GetAll());
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
                return Ok(_showTimeRepository.GetById(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("movie/{movie_id}")]
        public IActionResult GetByMovieId(int movie_id)
        {
            try
            {
                return Ok(_showTimeRepository.GetByMovieId(movie_id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShowTime(ShowTimeDTO dto, int id)
        {
            try
            {
                return Ok(_showTimeRepository.UpdateShowTime(dto,id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                return Ok(_showTimeRepository.DeleteShowTime(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
