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
    public class ResultRaitingController : ControllerBase
    {
        private readonly IResultRaitingRepository _resultRaitingRepository;

        public ResultRaitingController(IResultRaitingRepository resultRaitingRepository)
        {
            _resultRaitingRepository = resultRaitingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_resultRaitingRepository.GetAllRaitings());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
