using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tickets.Data;
using tickets.Entities;

namespace tickets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController : ControllerBase
    {
        private readonly TicketsDbContext _context;

        public SchedulesController(TicketsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetSchedules()
        {
            var schedules =
                await _context
                    .Schedules
                    .Include(s => s.Route)
                    .Include(s => s.Bus)
                    .Select(s =>
                        new {
                            s.Id,
                            s.RouteId,
                            RouteName = s.Route.Name,
                            s.BusId,
                            BusNumber = s.Bus.BusNumber,
                            s.DepartureTime
                        })
                    .ToListAsync();

            return Ok(schedules);
        }

        [HttpGet("{id}/available-seats")]
        public async Task<ActionResult<object>> GetAvailableSeats(int id)
        {
            var schedule =
                await _context
                    .Schedules
                    .Include(s => s.Bus)
                    .FirstOrDefaultAsync(s => s.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            var soldTickets =
                await _context.Tickets.CountAsync(t => t.ScheduleId == id);
            var availableSeats = schedule.Bus.Capacity - soldTickets;

            return new { scheduleId = id, availableSeats };
        }
    }
}
