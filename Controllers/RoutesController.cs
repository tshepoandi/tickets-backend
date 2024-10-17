using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tickets.Data;
using tickets.Entities;

using Route = tickets.Entities.Route;

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
        public async Task<ActionResult<IEnumerable<RouteDto>>> GetRoutes()
        {
            var routes =
                await _context
                    .Routes
                    .Include(r => r.RouteStops)
                    .ThenInclude(rs => rs.Stop)
                    .ToListAsync();

            var routeDtos =
                routes
                    .Select(r =>
                        new RouteDto {
                            Id = r.Id,
                            Name = r.Name,
                            RouteStops =
                                r
                                    .RouteStops
                                    .Select(rs =>
                                        new RouteStopDto {
                                            Id = rs.Id,
                                            StopId = rs.StopId,
                                            Stop =
                                                new StopDto {
                                                    Id = rs.Stop.Id,
                                                    Name = rs.Stop.Name
                                                },
                                            Order = rs.Order
                                        })
                                    .ToList()
                        })
                    .ToList();

            return routeDtos;
        }
    }
}
