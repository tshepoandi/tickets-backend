using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tickets.Data;
using tickets.Entities;

namespace tickets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {
        private readonly TicketsDbContext _context;

        public RoutesController(TicketsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Route>>> GetRoutes()
        {
            return await _context
                .Routes
                .Include(r => r.RouteStops)
                .ThenInclude(rs => rs.Stop)
                .ToListAsync();
        }
    }
}
