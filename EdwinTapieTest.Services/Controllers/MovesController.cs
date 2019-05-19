using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EdwinTapieTest.Services.Models;
using Microsoft.AspNetCore.Cors;

namespace EdwinTapieTest.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class MovesController : ControllerBase
    {
        private readonly Context _context;

        public MovesController(Context context)
        {
            _context = context;
        }

        // GET: api/Moves
        [HttpGet]
        public IEnumerable<Move> GetMoves()
        {
            return _context.Moves;
        }
    }
}