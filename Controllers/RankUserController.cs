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
    public class RankUserController : ControllerBase
    {
        private readonly IRankUserRepository _rankUserRepository;

        public RankUserController(IRankUserRepository rankUserRepository)
        {
            _rankUserRepository = rankUserRepository;
        }

        [HttpPost]
        public IActionResult CreateTypeLogin(RankUserDTO dto)
        {
            try
            {
                return Ok(_rankUserRepository.CreateRankUser(dto));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
