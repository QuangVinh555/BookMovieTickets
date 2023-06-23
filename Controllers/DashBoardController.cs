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
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardRepository _dashBoardRepository;

        public DashBoardController(IDashBoardRepository dashBoardRepository)
        {
            _dashBoardRepository = dashBoardRepository;
        }
        [HttpGet]
        public IActionResult GetDashBoardMovie()
        {
            try
            {
                return Ok(_dashBoardRepository.GetAllMovie());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("/api/dashboard/cinemaNameId")]
        public IActionResult GetDashBoardMovie(int cinemaNameId)
        {
            try
            {
                return Ok(_dashBoardRepository.GetAllMonthByCinemaName(cinemaNameId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
