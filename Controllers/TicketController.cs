using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tickets.Data;
using tickets.Entities;

namespace tickets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsDbContext _context;

        public TicketsController(TicketsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>>
        PurchaseTicket(TicketPurchaseDto ticketDto)
        {
            var user = await _context.Users.FindAsync(ticketDto.UserId);
            var schedule =
                await _context.Schedules.FindAsync(ticketDto.ScheduleId);

            if (user == null || schedule == null)
            {
                return BadRequest("Invalid user or schedule");
            }

            var ticket =
                new Ticket {
                    UserId = ticketDto.UserId,
                    ScheduleId = ticketDto.ScheduleId,
                    PurchaseDate = DateTime.UtcNow
                };

            _context.Tickets.Add (ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket),
            new { id = ticket.Id },
            ticket);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<object>>>
        GetUserTickets(int userId)
        {
            return await _context
                .Tickets
                .Where(t => t.UserId == userId)
                .Include(t => t.Schedule)
                .ThenInclude(s => s.Route)
                .Select(t =>
                    new {
                        t.Id,
                        t.ScheduleId,
                        RouteName = t.Schedule.Route.Name,
                        t.Schedule.DepartureTime,
                        t.PurchaseDate
                    })
                .ToListAsync();
        }
    }

    public class TicketPurchaseDto
    {
        public int UserId { get; set; }

        public int ScheduleId { get; set; }
    }
}
