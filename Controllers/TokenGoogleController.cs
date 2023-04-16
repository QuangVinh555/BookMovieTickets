using BookMovieTickets.Data;
using BookMovieTickets.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenGoogleController : ControllerBase
    {
        private readonly BookMovieTicketsContext _context;

        public TokenGoogleController(BookMovieTicketsContext context)
        {
            _context = context;
        }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [HttpPost]
        public IActionResult TokenGoogle(TokenDTO dto)
        {
            return Ok("Ok00");
        }

        [HttpGet]
        public IActionResult GetTokenGoogleAsync()
        {
            return null;
        }
    }
}
